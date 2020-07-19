using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector2[] points = {};
    [Range(0, 100f)][SerializeField] float speed = 10f;

    int nextPoint = 0;
    Vector2 startPosition;
    // public Vector2 velocity { get; private set; }

    private void Start() {
        if(points == null || points.Length < 2) {
            Debug.LogError("Moving platform: need at least 2 points");
            return;
        }
        startPosition = transform.position;

        transform.position = currentPoint;
    }

    Vector2 currentPoint {
        get {
            if (points == null || points.Length == 0) {
                return transform.position;
            }
            return points[nextPoint] + startPosition;
        }
    }

    private void FixedUpdate() {
        var newPosition = Vector2.MoveTowards(transform.position, currentPoint, speed * Time.deltaTime);

        if(Vector2.Distance(newPosition, currentPoint)<.001){
            newPosition = currentPoint;
            nextPoint += 1;
            nextPoint %= points.Length;
        }

        // velocity = (newPosition - new Vector2(transform.position.x, transform.position.y)) / Time.deltaTime;

        transform.position = newPosition;
    }

    private void OnDrawGizmos()
    {
        if (points == null || points.Length < 2) {
            return;
        }
        Vector2 offsetPosition = transform.position;
        if (Application.isPlaying) {
            offsetPosition = startPosition;
        }
        Gizmos.color = Color.blue;
        for (int p = 0; p < points.Length; p++) {
            var p1 = points[p];
            var p2 = points[(p + 1) % points.Length];
            Gizmos.DrawSphere(offsetPosition + p1, 0.1f);// Draw the line between the points
            Gizmos.DrawLine(offsetPosition + p1, offsetPosition + p2);
        }
    }
}
