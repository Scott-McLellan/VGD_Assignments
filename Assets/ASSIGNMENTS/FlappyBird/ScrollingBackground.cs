using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Vector2 textureOffset =  Vector2.zero;
    public float speed;
    
    

    void Start()
    {
        meshRenderer =  GetComponent<MeshRenderer>();
    }

    void Update()
    {
        textureOffset.x += speed * Time.deltaTime;
        meshRenderer.material.mainTextureOffset = textureOffset;
    }
}

