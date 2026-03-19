using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GeneratingMeshes : MonoBehaviour
{
    Mesh mesh;
    public Material material;
    Vector3[] vertices;
    private Vector2[] uvs;
    int[] triangles;

    public int xSize = 5;
    public int ySize = 3;
    public int zSize = 5;

    Mesh grassMesh;
    Mesh stoneMesh;
    Mesh dirtMesh;
    
    

    public Material cubeMaterial;

    // Start is called once before the first execution of Update
    void Start()
    {
        
        
        
        
        CreateMesh();
        CreateDirt();
        CreateGrass();
        CreateCobble();

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int z = 0; z < zSize; z++)
                {
                    GameObject cube = new GameObject("Cube");

                    cube.AddComponent<BoxCollider>();
                    
                    cube.transform.position = new Vector3(x, y, z);
                    
                    cube.layer = LayerMask.NameToLayer("Block");
                    
                    MeshFilter mf = cube.AddComponent<MeshFilter>();

                    if (y == 0)
                    {
                        mf.mesh = stoneMesh;
                    }
                    else if (y == 1)
                    {
                        mf.mesh = dirtMesh;
                    }
                    else if (y == 2)
                    {
                        mf.mesh = grassMesh;
                    }
                    
                    MeshRenderer mr = cube.AddComponent<MeshRenderer>();
                    mr.material = cubeMaterial;
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