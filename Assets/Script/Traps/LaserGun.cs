using UnityEngine;

public class LaserGun : MonoBehaviour
{
    private Transform tip;
    private LineRenderer lineRenderer;
    private BoxCollider laserCollider;

    void Start()
    {
        // Initialize the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // Get the world positions of the start and end points
        Vector3 localStart = lineRenderer.GetPosition(0);
        Vector3 localEnd = lineRenderer.GetPosition(1);

        // Add a BoxCollider to represent the laser collision area
        laserCollider = transform.gameObject.AddComponent<BoxCollider>();
        laserCollider.isTrigger = true;

        // Calculate the direction, center, and size of the BoxCollider
        Vector3 laserDirection = localEnd - localStart;
        float laserLength = laserDirection.magnitude;
        Vector3 laserCenter = (localEnd + localStart) / 2;
        Vector3 laserSize = new Vector3(localEnd.x == 0? 0.1f : laserLength, localEnd.y == 0? 0.1f : laserLength, localEnd.z == 0? 0.1f : laserLength); // Adjust the size as needed

        // Set the BoxCollider's position and size
        laserCollider.center = laserCenter;
        laserCollider.size = laserSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the laser's collider
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a PlayerManager script attached
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.killed();
            }
        }
    }
}
