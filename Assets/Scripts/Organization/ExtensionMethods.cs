using UnityEngine;
using System;

public static class Vector2Extension
{
    public static Vector2 RotatePointAroundPivotByZAxis(this Vector2 point, Vector2 pivot, float angles)
    {
        return (Vector2)(Quaternion.Euler(0, 0, angles) * (point - pivot) + (Vector3)pivot);
    }

}

public static class TransformExtension
{
    public static bool IsTargetBehind(this Transform self, Transform target)
    {
        Vector3 forward = new Vector3(self.localScale.x, 0, 0);
        Vector3 toTarget = (target.position - self.position);

        return Vector3.Dot(forward, toTarget) < 0;
    }

    /// <summary>
    /// Finds first child with given function.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="query">Function to find object ( ex. x => x.name == "ObjectName")</param>
    /// <returns>Found transform or null if not found.</returns>
    public static Transform FirstOrDefault(this Transform transform, Func<Transform, bool> query)
    {
        if (query(transform)) {
            return transform;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var result = FirstOrDefault(transform.GetChild(i), query);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}