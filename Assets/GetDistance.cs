using UnityEngine;

public static class GetDistance
{
    public static float Distance(Vector3 a, Vector3 b, Vector3 c)
    {
        return Mathf.Sqrt(((a.x - b.x) * (a.x - b.x)) + ((a.y - b.y) * (a.y - b.y)));
    }
}
