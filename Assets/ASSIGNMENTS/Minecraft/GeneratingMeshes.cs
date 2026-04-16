using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering.Universal;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public enum BlockType {
    Air,
    Grass,
    Dirt,
    Stone,
    Wood,
    Leaves,
    Coal,
    Iron,
    Diamond
}

public class GeneratingMeshes : MonoBehaviour
{
    Mesh mesh;
    public Material material;
    Vector3[] vertices;
    private Vector2[] uvs;
    int[] triangles;

    public int xSize = 20;
    public int ySize = 5;
    public int zSize = 20;

    public int seed;

    private float perlinNoise;

    public float MaxHeight;
    public float MinHeight;

    public float offset = 0.1f;

    public float terrainVariation = 2f;

    Mesh grassMesh;
    Mesh stoneMesh;
    Mesh dirtMesh;
    Mesh woodMesh;
    Mesh leavesMesh;
    Mesh coalMesh;
    Mesh ironMesh;
    Mesh diamondMesh;
    
    Dictionary<Vector3Int, BlockType> blocks = new Dictionary<Vector3Int, BlockType>();
    
    List<Vector3Int> stonePositions = new List<Vector3Int>();

    public float coalChance = 0.55f;
    public float ironChance = 0.35f;
    public float diamondChance = 0.10f;

    public Material cubeMaterial;
    
    

    public void SpawnBlocK(BlockType blockType, Vector3 position)
    {
        Vector3Int intPosition = Vector3Int.RoundToInt(position);

        if (blocks.ContainsKey(intPosition) && blocks[intPosition] != BlockType.Air)
        {
            Debug.LogWarning("Can't spawn a block when there is already a block at this position.");
            return;
        }
        
        blocks.Add(intPosition, blockType);
        
        if (blockType == BlockType.Air)
        {
            Debug.LogWarning("Trying to spawn an Air block! That doesn't make sense...");
            return;
        }
        
        GameObject cube = new GameObject("Block");

        cube.AddComponent<BoxCollider>();

        cube.transform.position = position;
                    
        cube.layer = LayerMask.NameToLayer("Block");
                    
        MeshFilter mf = cube.AddComponent<MeshFilter>();

        switch (blockType)
        {
            case BlockType.Grass:
                mf.sharedMesh = grassMesh;
                break;
            case BlockType.Dirt:
                mf.sharedMesh = dirtMesh;
                break;
            case BlockType.Stone:
                mf.sharedMesh = stoneMesh;
                break;
            case BlockType.Wood:
                mf.sharedMesh = woodMesh;
                break;
            case BlockType.Leaves:
                mf.sharedMesh = leavesMesh;
                break;
            case BlockType.Coal:
                mf.sharedMesh = coalMesh;
                break;
            case BlockType.Iron:
                mf.sharedMesh = ironMesh;
                break;
            case BlockType.Diamond:
                mf.sharedMesh = diamondMesh;
                break;
        }
                    
        MeshRenderer mr = cube.AddComponent<MeshRenderer>();
        mr.material = cubeMaterial;
    }

    public void RemoveBlock(GameObject block)
    {
        Vector3Int position = Vector3Int.RoundToInt(block.transform.position);
        if (!blocks.ContainsKey(position))
        {
            Debug.LogWarning("Trying to remove a block that doesn't exist!");
            return;
        }       
        
        blocks.Remove(position);
        GameObject.Destroy(block);
    }
    
    // Start is called once before the first execution of Update
    void Start()
    {

        Random.InitState(seed);

        float RandomYValue = Random.Range(0, MaxHeight + 1);
        
        
        CreateMesh();
        CreateDirt();
        CreateGrass();
        CreateCobble();
        CreateWood();
        CreateLeaves();
        CreateCoal();
        CreateIron();
        CreateDiamond();

        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                perlinNoise = Mathf.PerlinNoise((x + seed) * offset,(z + seed) * offset);
                
                // Perlin Noise for the Tree Spawner and uses the seed to add more randomness
                float treeNoise = Mathf.PerlinNoise((x + seed) * offset,(z + seed) * offset);
                
                float terrainHeight = MinHeight + Mathf.FloorToInt(perlinNoise * terrainVariation);
                
                for (int y = 0; y <= terrainHeight; y++)
                {
                    // Spawns Trees
                    if (y == terrainHeight)
                    {
                        SpawnBlocK(BlockType.Grass, new Vector3(x, y, z));

                        if (treeNoise < 0.5f && !IsTooCloseToTree(x, z))
                        {
                            TrySpawnTree(x, y , z);
                        }
                    }
                    else if (y >= terrainHeight - 2)
                    {
                        SpawnBlocK(BlockType.Dirt, new Vector3(x, y, z));
                    }
                    else
                    { 
                        Vector3Int stonePos = new Vector3Int(x, y, z);
                        SpawnBlocK(BlockType.Stone, stonePos);
                        stonePositions.Add(stonePos);
                    }
                }
            }
        }
        Debug.Log("Stone positions count: " + stonePositions.Count);
        Random.InitState(seed + 100);
        GenerateOres();
    }

    public void AppyUVS(int sideX, int sideY, int topX, int topY, int  bottomX, int bottomY)
    {
        float sideLeft = sideX / 16f;
        float sideRight = (sideX + 1) / 16f;
        float sideTop = (sideY + 1) / 16f;
        float sideBottom = sideY / 16f;
        
        float topLeft = topX / 16f;
        float topRight = (topX + 1) / 16f;
        float topTop = (topY + 1) / 16f;
        float topBottom = topY / 16f;
        
        float bottomLeft = bottomX / 16f;
        float bottomRight = (bottomX + 1) / 16f;
        float bottomTop = (bottomY + 1) / 16f;
        float bottomBottom = bottomY / 16f;
        
        uvs = new Vector2[]
        {
            // Front
            new Vector2(sideLeft, sideBottom),
            new Vector2(sideRight, sideBottom),
            new Vector2(sideRight, sideTop),
            new Vector2(sideLeft, sideTop),

            // Back
            new Vector2(sideLeft, sideBottom),
            new Vector2(sideRight, sideBottom),
            new Vector2(sideRight, sideTop),
            new Vector2(sideLeft, sideTop),

            // Left
            new Vector2(sideLeft, sideBottom),
            new Vector2(sideRight, sideBottom),
            new Vector2(sideRight, sideTop),
            new Vector2(sideLeft, sideTop),

            // Right
            new Vector2(sideLeft, sideBottom),
            new Vector2(sideRight, sideBottom),
            new Vector2(sideRight, sideTop),
            new Vector2(sideLeft, sideTop),

            // Top
            new Vector2(topLeft, topBottom),
            new Vector2(topRight, topBottom),
            new Vector2(topRight, topTop),
            new Vector2(topLeft, topTop),

            // Bottom
            new Vector2(bottomLeft, bottomBottom),
            new Vector2(bottomRight, bottomBottom),
            new Vector2(bottomRight, bottomTop),
            new Vector2(bottomLeft, bottomTop)
        };
        
    }

    void TrySpawnTree(int x, int y, int z)
    {
        // Trunk Height
        int treeHeight = Random.Range(4, 10);

        for (int i = 1; i <= treeHeight; i++)
        {
            SpawnBlocK(BlockType.Wood, new Vector3(x, y + i, z));
        }
        
        
        // Creating the leaves
        int leafY = y +  treeHeight;

        // Creating a Cube shape with the leaves
        // Going from -2 to 2 so that the leaves spawn
        // on the left and right side of the trunk
        for (int lx = -2; lx <= 2; lx++)
        {
            for (int lz = -2; lz <= 2; lz++)
            {
                for (int ly = -1; ly <= 1; ly++)
                {
                    Vector3Int leafPosition = new Vector3Int(x + lx, leafY + ly, z +  lz);

                    if (lx == 0 && lz == 0 && ly <= 0)
                    {
                        continue;
                    }
                    if (!blocks.ContainsKey(Vector3Int.RoundToInt(leafPosition)))
                    {
                        SpawnBlocK(BlockType.Leaves, leafPosition);
                    }
                    
                    
                }
            }
        }
    }
    
    // Checks to see if the tress are too close to each other
    bool IsTooCloseToTree(int x, int z)
    {
        int radius = 4;

        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dz = -radius; dz <= radius; dz++)
            {
                Vector3Int checkPos = new Vector3Int(x + dx, 0, z + dz);

                for (int y = 0; y < ySize + 10; y++)
                {
                    Vector3Int pos = new Vector3Int(x + dx, y, z + dz);

                    if (blocks.ContainsKey(pos) && blocks[pos] == BlockType.Leaves)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
    
    
    
    void ReplaceStoneWithOre(Vector3Int position, BlockType oreType)
    {
        if (!blocks.ContainsKey(position))
        {
            return;
        }

        if (blocks[position] != BlockType.Stone)
        {
            return;
        }

        // Remove the stone from the dictionary first
        blocks.Remove(position);

        // Find and destroy the old block GameObject
        Collider[] hits = Physics.OverlapBox(position, Vector3.one * 0.45f);

        foreach (Collider hit in hits)
        {
            if (Vector3Int.RoundToInt(hit.transform.position) == position)
            {
                Destroy(hit.gameObject);
                break;
            }
        }
        // Spawn the ore
        SpawnBlocK(oreType, position);
    }
    
    BlockType GetRandomOreType()
    {
        float roll = Random.value;

        if (roll < coalChance)
        {
            return BlockType.Coal;
        }
        else if (roll < (coalChance + ironChance))
        {
            return BlockType.Iron;
        }
        else
        {
            return BlockType.Diamond;
        }
    }
    
    int GetVeinSize(BlockType oreType)
    {
        if (oreType == BlockType.Coal)
        {
            return Random.Range(6, 13);
        }
        else if (oreType == BlockType.Iron)
        {
            return Random.Range(4, 9);
        }
        else
        {
            return Random.Range(2, 5);
        }
    }
    
    void GenerateOres()
    {
        int numberOfVeins = 35;

        for (int i = 0; i < numberOfVeins; i++)
        {
            if (stonePositions.Count == 0)
            {
                return;
            }

            Vector3Int startPos = Vector3Int.zero;
            bool foundValidStart = false;
            int tries = 0;

            while (!foundValidStart && tries < 30)
            {
                Vector3Int randomPos = stonePositions[Random.Range(0, stonePositions.Count)];

                if (blocks.ContainsKey(randomPos) && blocks[randomPos] == BlockType.Stone)
                {
                    startPos = randomPos;
                    foundValidStart = true;
                }

                tries++;
            }

            if (!foundValidStart)
            {
                continue;
            }
            
            Debug.Log("Valid ore vein start found at: " + startPos);

            BlockType oreType = GetRandomOreType();
            int veinSize = GetVeinSize(oreType);

            GenerateOreVein(startPos, oreType, veinSize);
        }
    }
    
    void GenerateOreVein(Vector3Int startPos, BlockType oreType, int veinSize)
    {
        List<Vector3Int> veinBlocks = new List<Vector3Int>();

        if (blocks.ContainsKey(startPos) && blocks[startPos] == BlockType.Stone)
        {
            ReplaceStoneWithOre(startPos, oreType);
            veinBlocks.Add(startPos);
        }
        
        if (veinBlocks.Count == 0)
        {
            return;
        }

        int attempts = 0;

        while (veinBlocks.Count < veinSize && attempts < veinSize * 10)
        {
            Vector3Int basePos = veinBlocks[Random.Range(0, veinBlocks.Count)];

            Vector3Int[] neighbors = new Vector3Int[]
            {
                basePos + Vector3Int.right,
                basePos + Vector3Int.left,
                basePos + Vector3Int.forward,
                basePos + Vector3Int.back,
                basePos + Vector3Int.up,
                basePos + Vector3Int.down,
                basePos + new Vector3Int(1, 0, 1),
                basePos + new Vector3Int(-1, 0, 1),
                basePos + new Vector3Int(1, 0, -1),
                basePos + new Vector3Int(-1, 0, -1)
            };

            Vector3Int nextPos = neighbors[Random.Range(0, neighbors.Length)];

            if (blocks.ContainsKey(nextPos) && blocks[nextPos] == BlockType.Stone)
            {
                ReplaceStoneWithOre(nextPos, oreType);
                veinBlocks.Add(nextPos);
            }

            attempts++;
        }
    }

    void CreateCobble()
    {
        AppyUVS(0, 14, 0, 14, 0, 14);
        
        stoneMesh = new Mesh();
        stoneMesh.vertices = vertices;
        stoneMesh.triangles = triangles;
        stoneMesh.uv = uvs;
        stoneMesh.RecalculateNormals();
    }

    void CreateDirt()
    {
        AppyUVS(2, 15, 2, 15, 2, 15);
        
        dirtMesh = new Mesh();
        dirtMesh.vertices = vertices;
        dirtMesh.triangles = triangles;
        dirtMesh.uv = uvs;
        dirtMesh.RecalculateNormals();
    }

    void CreateGrass()
    {
        AppyUVS(3, 15, 0, 15, 2, 15);

        grassMesh = new Mesh();
        grassMesh.vertices = vertices;
        grassMesh.triangles = triangles;
        grassMesh.uv = uvs;
        grassMesh.RecalculateNormals();
        
    }

    void CreateWood()
    {
        AppyUVS(4, 14, 5, 14, 5, 14);
        
        woodMesh = new Mesh();
        woodMesh.vertices = vertices;
        woodMesh.triangles = triangles;
        woodMesh.uv = uvs;
        woodMesh.RecalculateNormals();
    }

    void CreateLeaves()
    {
        AppyUVS(4, 12, 4, 12, 4, 12);
        
        leavesMesh = new Mesh();
        
        leavesMesh.vertices = vertices;
        leavesMesh.triangles = triangles;
        leavesMesh.uv = uvs;
        leavesMesh.RecalculateNormals();
        
    }

    void CreateCoal()
    {
        AppyUVS(2, 13, 2, 13, 2, 13);
        
        coalMesh = new Mesh();
        
        coalMesh.vertices = vertices;
        coalMesh.triangles = triangles;
        coalMesh.uv = uvs;
        coalMesh.RecalculateNormals();
    }

    void CreateIron()
    {
        AppyUVS(1, 13, 1, 13, 1, 13);
        
        ironMesh = new Mesh();
        ironMesh.vertices = vertices;
        ironMesh.triangles = triangles;
        ironMesh.uv = uvs;
        ironMesh.RecalculateNormals();
    }

    void CreateDiamond()
    {
        AppyUVS(2, 12, 2, 12, 2, 12);

        diamondMesh = new Mesh();
        diamondMesh.vertices = vertices;
        diamondMesh.triangles = triangles;
        diamondMesh.uv = uvs;
        diamondMesh.RecalculateNormals();
    }

    void CreateMesh()
    {
        vertices = new Vector3[]
        {
            // Front
            new Vector3(-0.5f,-0.5f,-0.5f),
            new Vector3(0.5f,-0.5f,-0.5f),
            new Vector3(0.5f,0.5f,-0.5f),
            new Vector3(-0.5f,0.5f,-0.5f),

            // Back
            new Vector3(-0.5f,-0.5f,0.5f),
            new Vector3(0.5f,-0.5f,0.5f),
            new Vector3(0.5f,0.5f,0.5f),
            new Vector3(-0.5f,0.5f,0.5f),
            
            // Left
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),

            // Right
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f), 

            // Top
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f),
            new Vector3( 0.5f,  0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),

            // Bottom
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f)
        };

        triangles = new int[]
        {
            // Front
            0, 2, 1,
            0, 3, 2,

            // Back
            4, 5, 6,
            4, 6, 7,

            // Left
            9, 11, 10,
            9, 8, 11,

            // Right
            12, 15, 14,
            12, 14, 13,

            // Top
            16, 19, 18,
            16, 18, 17,

            // Bottom
            23, 22, 21,
            23, 21, 20
        };
    }

    void Update()
    {

    }
}