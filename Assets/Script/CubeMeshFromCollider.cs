using UnityEngine;

public class CubeMeshFromCollider : MonoBehaviour
{
    public Material MeshMaterial;
    void Start()
    {
        // Retrieve the BoxCollider component from the GameObject
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        // Create a new GameObject to hold the box mesh
        GameObject cubeMeshObject = new GameObject("CubeMesh");
        cubeMeshObject.transform.SetParent(transform);

        // Set the local position to match the center of the BoxCollider
        cubeMeshObject.transform.localPosition = boxCollider.center;
        cubeMeshObject.transform.localRotation = Quaternion.identity;

        // Add a MeshFilter and MeshRenderer to the new GameObject
        MeshFilter meshFilter = cubeMeshObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = cubeMeshObject.AddComponent<MeshRenderer>();

        // Create a new cube mesh
        meshFilter.mesh = CreateCubeMesh(boxCollider.size);

        // Optionally, set a material to the MeshRenderer
        meshRenderer.material = MeshMaterial;
    }

    private Mesh CreateCubeMesh(Vector3 size)
    {
        Mesh mesh = new Mesh();

        // Define vertices for a cube
        Vector3[] vertices = new Vector3[8]
        {
            new Vector3(-size.x, -size.y, -size.z), // Bottom-left-back
            new Vector3( size.x, -size.y, -size.z), // Bottom-right-back
            new Vector3( size.x,  size.y, -size.z), // Top-right-back
            new Vector3(-size.x,  size.y, -size.z), // Top-left-back
            new Vector3(-size.x, -size.y,  size.z), // Bottom-left-front
            new Vector3( size.x, -size.y,  size.z), // Bottom-right-front
            new Vector3( size.x,  size.y,  size.z), // Top-right-front
            new Vector3(-size.x,  size.y,  size.z)  // Top-left-front
        };

        // Define triangles for each face of the cube
        int[] triangles = new int[36]
        {
            // Back
            0, 2, 1, 0, 3, 2,
            // Front
            4, 5, 6, 4, 6, 7,
            // Left
            0, 7, 3, 0, 4, 7,
            // Right
            1, 2, 6, 1, 6, 5,
            // Top
            2, 3, 7, 2, 7, 6,
            // Bottom
            0, 1, 5, 0, 5, 4
        };

        // Assign the vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals for proper lighting
        mesh.RecalculateNormals();

        return mesh;
    }
}
