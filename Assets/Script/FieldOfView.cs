using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float fov = 90f;
    private Vector3 origin_point = Vector3.zero;
    private float startingAngle = 0f;
    public int rayCount = 20;
    public float viewDistance = 50f;
    private Mesh mesh;
    public LayerMask layerMask;
    public float MaxValue = 360f; // Maximum value for transparency, corresponding to fully opaque
    private float MinValue = 0f; // Minimum value for transparency, corresponding to mostly transparent
    private Material aggroMaterial;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        aggroMaterial = GetComponent<Renderer>().material;
        MinValue = aggroMaterial.color.a;
    }

    void LateUpdate()
    {
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin_point;
        int vertexIndex = 1;
        int trianglesIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit hit;
            Vector3 angleVector = GetVectorFromAngle(angle);

            if (Physics.Raycast(origin_point, angleVector, out hit, viewDistance, layerMask))
            {
                vertex = hit.point;
            }
            else
            {
                vertex = origin_point + angleVector * viewDistance;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[trianglesIndex + 0] = 0;
                triangles[trianglesIndex + 1] = vertexIndex - 1;
                triangles[trianglesIndex + 2] = vertexIndex;

                trianglesIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        // Returns a direction vector from an angle in degrees
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
    }

    float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public void SetOriginPoint(Vector3 Origin)
    {
        origin_point = Origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(-1 * aimDirection) + fov / 2;
    }

    public void SetFov(float infov)
    {
        this.fov = infov;
    }

    public void SetDistance(float distance)
    {
        this.viewDistance = distance;
    }

    public void SetLayer(LayerMask layerMask)
    {
        this.layerMask = layerMask;
    }

    public void SetAggro(float transparency)
    {
        // Map the scale to the transparency range
        Color color = aggroMaterial.color;
        color.a = transparency; // Convert to [0,1] range for alpha
        aggroMaterial.color = color;
    }

    public float GetAggro()
    {
        if (aggroMaterial == null) return MinValue;
        return aggroMaterial.color.a; // Convert back from [0,1] range
    }

    public void DecreaseAggro(float amount)
    {
        if (GetAggro() - amount < MinValue) {
            SetAggro(MinValue);
            return;}
        float transparency = GetAggro();
        SetAggro(transparency - amount);
    }

    public void AddAggro(float amount)
    {
        DecreaseAggro(-1 * amount);
    }
}
