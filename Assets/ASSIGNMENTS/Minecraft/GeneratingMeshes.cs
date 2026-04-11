using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public enum BlockType {
    Air,
    Grass,
    Dirt,
    Stone,
    Wood,
    Leaves
}

public class GeneratingMeshes : MonoBehaviour
{
    Mesh mesh;
    public Material material;
    Vector3[] vertices;
    private Vector2[] uvs;
    int[] triangles;

    public int xSize = 5;
    public int ySize = 10;
    public int zSize = 5;

    public int seed;

    private float perlinNoice;

    public float MaxHeight;
    public float MinHeight;

    public float offset = 0.1f;

    public float terrainVariation = 2f;

    Mesh grassMesh;
    Mesh stoneMesh;
    Mesh dirtMesh;
    Mesh woodMesh;
    Mesh leavesMesh;
    
    Dictionary<Vector3Int, BlockType> blocks = new Dictionary<Vector3Int, BlockType>();

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


        float RandomYValue = Random.Range(0, MaxHeight + 1);
        
        
        CreateMesh();
        CreateDirt();
        CreateGrass();
        CreateCobble();
        CreateWood();
        CreateLeaves();

        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                perlinNoice = Mathf.PerlinNoise((x + seed) * offset,(z + seed) * offset);
                float terrainHeight = MinHeight + Mathf.FloorToInt(perlinNoice * terrainVariation);
                
                for (int y = 0; y <= terrainHeight; y++)
                {
                    if (y == terrainHeight)
                    {
                        SpawnBlocK(BlockType.Grass, new Vector3(x, y, z));
                    }
                    else if (y >= terrainHeight - 2)
                    {
                        SpawnBlocK(BlockType.Dirt, new Vector3(x, y, z));
                    }
                    else
                    { 
                        SpawnBlocK(BlockType.Stone, new Vector3(x, y, z));
                    }
                }
            }
        }
    }

    public void AppyUVS(int x, int y)
    {
        float left_corner = x / 16f;
        float right_corner = (x + 1) / 16f;
        float top_corner = (y + 1) / 16f;
        float bottom_corner = y / 16f;
        
        uvs = new Vector2[]
        {
            // Front
            new Vector2(left_corner, bottom_corner), // bottom-left
            new Vector2(right_corner, bottom_corner), // bottom-right
            new Vector2(right_corner, top_corner), // top-right
            new Vector2(left_corner, top_corner), // top-left
            
            // Back
            new Vector2(left_corner, bottom_corner),
            new Vector2(right_corner, bottom_corner),
            new Vector2(right_corner, top_corner),
            new Vector2(left_corner, top_corner),
            
            // Left
            new Vector2(left_corner, bottom_corner),
            new Vector2(right_corner, bottom_corner),
            new Vector2(right_corner, top_corner),
            new Vector2(left_corner, top_corner),

            // Right
            new Vector2(left_corner, bottom_corner),
            new Vector2(right_corner, bottom_corner),
            new Vector2(right_corner, top_corner),
            new Vector2(left_corner, top_corner),

            // Top
            new Vector2(left_corner, bottom_corner),
            new Vector2(right_corner, bottom_corner),
            new Vector2(right_corner, top_corner),
            new Vector2(left_corner, top_corner),

            // Bottom
            new Vector2(left_corner, bottom_corner),
            new Vector2(right_corner, bottom_corner),
            new Vector2(right_corner, top_corner),
            new Vector2(left_corner, top_corner)
        };
        
    }

    void CreateCobble()
    {
        AppyUVS(0, 14);
        
        stoneMesh = new Mesh();
        stoneMesh.vertices = vertices;
        stoneMesh.triangles = triangles;
        stoneMesh.uv = uvs;
        stoneMesh.RecalculateNormals();
    }

    void CreateDirt()
    {
        AppyUVS(2, 15);
        
        dirtMesh = new Mesh();
        dirtMesh.vertices = vertices;
        dirtMesh.triangles = triangles;
        dirtMesh.uv = uvs;
        dirtMesh.RecalculateNormals();
    }

    void CreateGrass()
    {
        AppyUVS(0, 15);

        grassMesh = new Mesh();
        grassMesh.vertices = vertices;
        grassMesh.triangles = triangles;
        grassMesh.uv = uvs;
        grassMesh.RecalculateNormals();
        
    }

    void CreateWood()
    {
        AppyUVS(4, 14);
        
        woodMesh = new Mesh();
        woodMesh.vertices = vertices;
        woodMesh.triangles = triangles;
        woodMesh.uv = uvs;
        woodMesh.RecalculateNormals();
    }

    void CreateLeaves()
    {
        AppyUVS(4, 12);
        
        leavesMesh = new Mesh();
        
        leavesMesh.vertices = vertices;
        leavesMesh.triangles = triangles;
        leavesMesh.uv = uvs;
        leavesMesh.RecalculateNormals();
        
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