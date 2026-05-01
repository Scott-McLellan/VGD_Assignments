using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private GameObject player;

    private List<Vector3Int> path = new List<Vector3Int>();

    private AStarPathfinding pathfinding;

    private float playerRadius = 0.2f;

    private float speed = 2f;

    private float distance = 0.2f;
    
    int currentPathIndex = 0;

    private float pointReachDistance = 0.1f;

    public float Offset = 0.5f;

    public float pathUpdateTime = 0.3f;
    private float pathUpdateTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        pathfinding = FindObjectOfType<AStarPathfinding>();

        FindNewPath();
    }

    // Update is called once per frame
    void Update()
    {
        pathUpdateTimer -= Time.deltaTime;

        if (pathUpdateTimer <= 0)
        {
            FindNewPath();
            pathUpdateTimer = pathUpdateTime;
        }

        FollowPath();

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= playerRadius)
        {
            Debug.Log("Zombie Hit Player");
        }
    }

    void FindNewPath()
    {
        if (player == null || pathfinding == null)
        {
            return;
        }

        Vector3Int zombiePosition = Vector3Int.RoundToInt(transform.position);
        Vector3Int playerPosition = Vector3Int.RoundToInt(player.transform.position);

        path = pathfinding.FindPath(zombiePosition, playerPosition);
        
        Debug.Log("Path count: " + path.Count);
        Debug.Log("Zombie position: " + zombiePosition);
        Debug.Log("Player position: " + playerPosition);

        if (path.Count > 1)
        {
            currentPathIndex = 1;
        }
        else
        {
            currentPathIndex = 0;
        }
    }

    void FollowPath()
    {
        if (path == null || path.Count == 0)
        {
            return;
        }

        if (currentPathIndex < path.Count)
        {
            Vector3 targetPosition = path[currentPathIndex];
            targetPosition.y = targetPosition.y + Offset;

            float distanceToPoint = Vector3.Distance(transform.position, targetPosition);

            if (distanceToPoint <= pointReachDistance)
            {
                currentPathIndex++;
                return;
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
        }
    }

    void OnDrawGizmos()
    {
        if (path == null || path.Count == 0) return;

        Gizmos.color = Color.green;

        for (int i = 0; i < path.Count; i++)
        {
            Vector3 pos = path[i];

            if (currentPathIndex < path.Count && pos == path[currentPathIndex])
            {
                Gizmos.color = Color.blue;
            }

            Gizmos.DrawCube(pos, new Vector3(0.9f, 0.2f, 0.9f));

            Gizmos.color = Color.green;

            if (i > 0)
            {
                Vector3 prevPos = path[i - 1];
                Gizmos.DrawLine(prevPos, pos);
            }
        }
    }
}