using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private GameObject player;

    private Vector3[] testPath;

    private float playerRadius = 0.2f;

    private float speed = 2f;

    private float distance = 0.2f;
    
    int currentPathIndex = 0;

    private float pointReachDistance = 0.1f;

    public float Offset = 0.5f;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
        player =  GameObject.FindGameObjectWithTag("Player");

        testPath = new Vector3[]
        {
            new Vector3(7, 3, 6),
            new Vector3(6, 3, 6),
            new Vector3(5, 3, 6),
            new Vector3(4, 3, 6),
            new Vector3(3, 3, 5),
            new Vector3(2, 3, 4),
        };

    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        if (currentPathIndex < testPath.Length)
        {
            Vector3 targetPosition = testPath[currentPathIndex];
            targetPosition.y = targetPosition.y + Offset;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            
            float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);

            if (distanceToPlayer <= pointReachDistance)
            {
                currentPathIndex++;
            }
        }

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= playerRadius)
        {
            Debug.Log("Zombie Hit Player");
        }
        
        
        
    }
    
    void OnDrawGizmos()
    {
        if (testPath == null || testPath.Length == 0) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < testPath.Length; i++)
        {
            Vector3 pos = testPath[i]; 
            if(currentPathIndex < testPath.Length && pos == testPath[currentPathIndex])
                Gizmos.color = Color.blue;
            Gizmos.DrawCube(pos, new Vector3(0.9f, 0.2f, 0.9f));          
            Gizmos.color = Color.green;
        
            if (i > 0)
            {
                Vector3 prevPos = testPath[i - 1];
                Gizmos.DrawLine(prevPos, pos);             
            }
        }
    }
}
