using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    public GeneratingMeshes world;

    private Vector3Int[] directions =
    {
        Vector3Int.forward,
        Vector3Int.back,
        Vector3Int.left,
        Vector3Int.right
    };

    void Start()
    {
        if (world == null)
        {
            world = FindObjectOfType<GeneratingMeshes>();
        }
    }

    public List<Vector3Int> FindPath(Vector3Int startPosition, Vector3Int targetPosition)
    {
        PriorityQueue<Node> openSet = new PriorityQueue<Node>();
        HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();
        Dictionary<Vector3Int, Node> allNodes = new Dictionary<Vector3Int, Node>();

        Node startNode = new Node(
            startPosition,
            0,
            GetDistance(startPosition, targetPosition),
            null
        );

        openSet.Enqueue(startNode);
        allNodes[startPosition] = startNode;

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.Dequeue();

            if (closedSet.Contains(currentNode.position))
            {
                continue;
            }

            closedSet.Add(currentNode.position);

            if (currentNode.position == targetPosition)
            {
                return BuildPath(currentNode);
            }

            foreach (Vector3Int direction in directions)
            {
                Vector3Int nextPosition;

                if (!world.TryGetMovePosition(currentNode.position, direction, out nextPosition))
                {
                    continue;
                }

                if (closedSet.Contains(nextPosition))
                {
                    continue;
                }

                float newGCost = currentNode.gCost + 1;
                float hCost = GetDistance(nextPosition, targetPosition);

                if (!allNodes.ContainsKey(nextPosition) || newGCost < allNodes[nextPosition].gCost)
                {
                    Node nextNode = new Node(nextPosition, newGCost, hCost, currentNode);

                    allNodes[nextPosition] = nextNode;
                    openSet.Enqueue(nextNode);
                }
            }
        }

        return new List<Vector3Int>();
    }

    private float GetDistance(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
    }

    private List<Vector3Int> BuildPath(Node endNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Node currentNode = endNode;

        while (currentNode != null)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }
}