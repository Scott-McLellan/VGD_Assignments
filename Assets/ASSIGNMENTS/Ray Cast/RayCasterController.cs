using UnityEngine;

public class RayCasterController : MonoBehaviour
{
    private float maxDistance = 100;
    
    public LayerMask layerMask;

    public float heightAboveHitPoint;
    
    bool isHolding = false;
    
    SimplePickupSystem simplePickupSystem;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        simplePickupSystem = GetComponent<SimplePickupSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        bool didHit = Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance, layerMask);

        if (isHolding)
        {
            Vector3 targetPosition;

            if (didHit)
            {
                targetPosition = hitInfo.point + Vector3.up * heightAboveHitPoint;
            }
            else
            {
                targetPosition = ray.origin + ray.direction * heightAboveHitPoint;
            }
            
            simplePickupSystem.UpdatePickupPosition(targetPosition);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding && didHit)
            {
                simplePickupSystem.Pickup(hitInfo.collider.gameObject);
                isHolding = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (isHolding)
            {
                simplePickupSystem.Drop();
                isHolding = false;
            }
        }
    }
}
