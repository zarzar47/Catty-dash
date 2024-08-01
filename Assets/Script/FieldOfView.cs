using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float fov = 90f;
    private UnityEngine.Vector3 origin_point = Vector3.zero;
    private float startingAngle = 0f;
    public int rayCount = 20;
    public float viewDistance = 50f;
    private Mesh mesh;
    public LayerMask layerMask;
    private Transform aggroHead;
    // Start is called before the first frame update
    void Start()
    {
        mesh  = new Mesh();
        aggroHead = transform.GetChild(0).transform;
        aggroHead.GetComponentInChildren<MeshFilter>().mesh = mesh;
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        float angle = startingAngle;
        float angleIncrease = fov/ rayCount;;
        UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[rayCount + 1 + 1];
        UnityEngine.Vector2[] uv = new UnityEngine.Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin_point;
        int vertexIndex = 1;
        int trianglesIndex= 0;
        for (int i = 0; i<= rayCount; i++){
            UnityEngine.Vector3 vertex;
            RaycastHit hit;
            UnityEngine.Vector3 angleVector = GetVectorFromAngle(angle);
            
            if (Physics.Raycast(origin_point, angleVector, out hit, viewDistance, layerMask))
            {
                vertex = hit.point;
            } else {
                vertex = origin_point + angleVector * viewDistance;
            }
            
            vertices[vertexIndex] = vertex;

            if (i>0){
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

    Vector3 GetVectorFromAngle(float angle){
        //returns Eulers angle
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
    }

    float GetAngleFromVectorFloat(Vector3 dir){
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        if (n<0) n+=360;
        return n;
    }

    public void SetOriginPoint(Vector3 Origin){
        transform.position = Origin;
        origin_point = Origin;
    }

    public void SetAimDirection(Vector3 aimDirection){
        startingAngle = GetAngleFromVectorFloat(aimDirection) - fov/2;
    }

    public void SetFov(float infov){
        this.fov = infov;
    }

    public void SetDistance(float distance){
        this.viewDistance = distance;
    }

    public void SetLayer(LayerMask layerMask){
        this.layerMask = layerMask;
    }

    public void SetAggro(float scale){
        if (aggroHead == null) return;
        Vector3 newscale = aggroHead.localScale;
        newscale.x = scale;
        newscale.z = scale;
        aggroHead.localScale = newscale;
    }

    public float GetAggro(){
        if (aggroHead == null) return 0.0f;
        return aggroHead.localScale.x;
    }

    public void DecreaseAggro(float amount){
        float aggro = GetAggro();
        SetAggro(aggro - amount);
    }

    public void AddAggro(float amount){
        DecreaseAggro(-1*amount);
    }

}
