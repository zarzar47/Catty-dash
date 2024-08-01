using UnityEngine;
using System.Collections.Generic;

public class TrailLine : MonoBehaviour
{
    private LineRenderer lineRenderer;  // Reference to the LineRenderer component
    public float lineLifeTime = 5f;    // Time in seconds for the line to disappear
    private Queue<Vector3> positions;  // Queue to store line positions with timestamps
    private Queue<float> timestamps;   // Queue to store time stamps of each position
    private float elapsedTime = 0f;    // Accumulated time

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;

        // Initialize position and timestamp queues
        positions = new Queue<Vector3>();
        timestamps = new Queue<float>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        Vector3 currentPosition = transform.position;

        // Add the current position and timestamp to the queues
        positions.Enqueue(currentPosition);
        timestamps.Enqueue(elapsedTime);

        // Update the LineRenderer with the new position
        UpdateLineRenderer();

        // Remove old positions based on the lifetime
        while (timestamps.Count > 0 && (elapsedTime - timestamps.Peek()) > lineLifeTime)
        {
            timestamps.Dequeue();
            positions.Dequeue();
        }
    }

    private void UpdateLineRenderer()
    {
        // Update LineRenderer with the current positions in the queue
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }
}
