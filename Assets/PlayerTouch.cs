using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerTouch : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 10f;
    public float maxDragLength = 6.5f;
    private bool pathTacing = false;
    public float pathDelay = 1f;
    private float pathTimer = 0f;
    private UnityEngine.Vector3 mouse_Posi = new UnityEngine.Vector3(0,0,0);
    public Transform center;
    private new Rigidbody rigidbody;
    private GameObject sphereContainer;
    public GameObject trail;
    private List<UnityEngine.Vector3> pathTrace = new List<UnityEngine.Vector3>();
    private 
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        sphereContainer = new GameObject("sphereContainer");
    }

    // Update is called once per frame
    void Update(){

    }

    void FixedUpdate(){
        if (pathTacing)
            MovePath();
    }

    private void OnMouseDrag() {
        if (pathTacing == true)
            return;

        UnityEngine.Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen coordinates
        Ray ray = Camera.main.ScreenPointToRay(mousePosition); // Convert screen coordinates to a ray
        float length = 0;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            mouse_Posi = hit.point; // Get the world position of the mouse click
            mouse_Posi.y = center.position.y;

            pathTimer+=Time.deltaTime;
            if (pathTimer > pathDelay)
            {
                pathTimer = 0f;
                GameObject sphere = Instantiate(trail, mouse_Posi, UnityEngine.Quaternion.identity);
                sphere.transform.SetParent(sphereContainer.transform);
                pathTrace.Add(sphere.transform.position);
            }
            //newObj.transform.SetParent(sphereContainer.transform);
        }

        if (length > 1.2f)
            transform.LookAt(mouse_Posi);
    }

    private void OnMouseUp() {
        if (pathTacing == true)
            return;
        
        CleanUp();
        pathTacing = true;
    }

    private void MovePath(){
        if (pathTrace.Count != 0){
            UnityEngine.Vector3 latestpath = pathTrace[0];
            transform.position = new UnityEngine.Vector3(Mathf.MoveTowards(transform.position.x, latestpath.x, maxSpeed), transform.position.y,
             Mathf.MoveTowards(transform.position.z, latestpath.z, maxSpeed));
            if (transform.position.x == latestpath.x && transform.position.z == latestpath.z){
                pathTrace.RemoveAt(0);
            }
        } else if (pathTrace.Count == 0){
            pathTacing = false;
        }
    }

    private void CleanUp(){
        if (sphereContainer != null){
            Destroy(sphereContainer);
            sphereContainer = new GameObject("sphereContainer");
        }
    }

    private void OnCollisionEnter(Collision other) {
    }


}
