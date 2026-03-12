using UnityEngine;

public class GeneratingMeshes : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 5;
    public int ySize = 3;
    public int zSize = 5;

    public Material cubeMaterial;

    // Start is called once before the first execution of Update
    void Start()
    {
        CreateMesh();

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int z = 0; z < zSize; z++)
                {
                    GameObject cube = new GameObject("Cube");

                    cube.transform.position = new Vector3(x, y, z);
                    
                    cube.layer = LayerMask.NameToLayer("Block");

                    cube.AddComponent<BoxCollider>();

                    MeshFilter mf = cube.AddComponent<MeshFilter>();
                    mf.mesh = mesh;

                    MeshRenderer mr = cube.AddComponent<MeshRenderer>();
                    mr.material = cubeMaterial;
                }
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void CreateMesh()
    {
        mesh = new Mesh();

        vertices = new Vector3[]
        {
            // Front
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(1,1,0),
            new Vector3(0,1,0),

            // Back
            new Vector3(0,0,1),
            new Vector3(1,0,1),
            new Vector3(1,1,1),
            new Vector3(0,1,1)
        };

        triangles = new int[]
        {
            // Front
            0,2,1,
            0,3,2,

            // Back
            4,5,6,
            4,6,7,

            // Left
            0,7,3,
            0,4,7,

            // Right
            1,2,6,
            1,6,5,

            // Top
            3,7,6,
            3,6,2,

            // Bottom
            0,1,5,
            0,5,4
        };

        UpdateMesh();
    }

    void Update()
    {

    }
}