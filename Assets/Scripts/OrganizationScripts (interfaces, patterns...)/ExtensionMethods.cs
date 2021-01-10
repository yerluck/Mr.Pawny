using UnityEngine;

public static class Vector2Extension
{
    public static Vector2 RotatePointAroundPivotByZAxis(this Vector2 point, Vector2 pivot, float angles)
    {
        return (Vector2)(Quaternion.Euler(0, 0, angles) * (point - pivot) + (Vector3)pivot);
    }
}