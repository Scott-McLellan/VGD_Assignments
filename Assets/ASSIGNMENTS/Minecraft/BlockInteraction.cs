using System.Net;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    
    public Camera playerCamera;

    public float reachDistance = 6f;
    
    public GameObject blockPrefab;
    
    public LayerMask BlockLayer;
    
    public LayerMask GroundLayer;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new  Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * reachDistance, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDistance, BlockLayer))
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 placePosition = hit.transform.position + hit.normal;
                
                placePosition = new Vector3(Mathf.Round(placePosition.x), Mathf.Round(placePosition.y), Mathf.Round(placePosition.z));

                if (!Physics.CheckBox(placePosition, Vector3.one * 0.45f))
                {
                    Instantiate(blockPrefab, placePosition, Quaternion.identity);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
