using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    [SerializeField, Range(0,1)]private float timeSlowDown = 1;
    public float maxSpeed = 10f;
    public float moveMultipler = 5f;
    public float maxDragLength = 6.5f;
    private bool aiming = false;
    private bool moving = false;
    private UnityEngine.Vector3 arrowVector = new UnityEngine.Vector3(0,0,0);
    private UnityEngine.Vector3 original_mouse_Posi = new UnityEngine.Vector3(0,0,0);
    public Transform center;
    private new Rigidbody rigidbody;
    private Vector3 old_velo = new Vector3(0,0,0);
    private Material ArrowMat;
    void Start()
    {
        Arrow.SetActive(false);
        rigidbody = GetComponent<Rigidbody>();
        ArrowMat = Arrow.GetComponentInChildren<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.touchCount > 0){
            //Touch touch = Input.GetTouch(0);
        if (Input.GetMouseButtonDown(0)){
            SlingShotInitialPos();
        } else if ( /*(touch.phase == TouchPhase.Moved) ||*/ Input.GetMouseButton(0)){
            Debug.Log("aiming");
            aiming = true;
            moving = false;
        } else if ( /*touch.phase == TouchPhase.Ended ||*/ Input.GetMouseButtonUp(0)){
            Debug.Log("Moving");
            aiming = false;
            moving = true;
        }
        //}

        if (aiming == true){
            SlingShotTrajectory();
        } else if (moving == true && aiming == false){
            SlingShotAction();
        }
    }

    void FixedUpdate(){
        old_velo = rigidbody.velocity;
        ClampVelocity();
    }

    private void SlingShotInitialPos(){
        original_mouse_Posi = GetMousePos();
        Time.timeScale = timeSlowDown;
    }
    private void SlingShotTrajectory() {
        UnityEngine.Vector3 current_mousePos = GetMousePos();
        UnityEngine.Vector3 original_Posi = original_mouse_Posi; // Get the player's position
        float length = 0;
        // Calculate the direction vector from the player to the click position
        UnityEngine.Vector3 direction = current_mousePos - original_Posi;

        // The length of the vector is the distance between the player and the click position
        length = direction.magnitude;
        if (length > maxDragLength)
            length = maxDragLength;
        else if (length<0.25){
            return;
        }
        // Normalize the direction vector to get a unit vector and scale it by the distance
        arrowVector = direction.normalized * length * -1;

        transform.LookAt(center.position + arrowVector);
        AimArrow();
        if (Arrow.activeSelf){
            float normalizedLength = Mathf.InverseLerp(0f, 7f, length);
            Color color = Color.Lerp(Color.white, Color.red, normalizedLength);
            ArrowMat.color = color;
        }

    }

    private void SlingShotAction() {
        rigidbody.AddForce(arrowVector * moveMultipler, ForceMode.Impulse);
        moving = false;
        Arrow.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnCollisionEnter(Collision other) {
         if (other.gameObject.tag == "Bouceable"){
            ApplyReflection(other);
         }
    }

    void ApplyReflection(Collision collider) {
        //getting the normal vector from the contact on the object (not the player's point of contact)
        Vector3 collisionNormal = collider.GetContact(0).normal;
        Vector3 incomingVelocity = old_velo;
        Vector3 reflectDir = Vector3.Reflect (incomingVelocity , collisionNormal); // reflecting the velocity of the player along the normal vector's plane
        transform.rotation.SetFromToRotation(rigidbody.velocity.normalized, reflectDir); // rotating the player to the new vector's direction
        reflectDir *= 0.8f;

        //application of the force
        rigidbody.AddForce(reflectDir, ForceMode.Impulse);
    }

    void ClampVelocity()
    {
        // Calculate the current speed of the Rigidbody
        float currentSpeed = rigidbody.velocity.magnitude;

        // If the current speed exceeds the maximum speed, clamp it
        if (currentSpeed > maxSpeed) {
            // Normalize the velocity vector and scale it to the maximum speed
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }
    }

    private UnityEngine.Vector3 GetMousePos() {
        UnityEngine.Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen coordinates
        Ray ray = Camera.main.ScreenPointToRay(mousePosition); // Convert screen coordinates to a ray
        UnityEngine.Vector3 mouse_Posi = new Vector3(0,0,0);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            mouse_Posi = hit.point; // Get the world position of the mouse click
            mouse_Posi.y = center.position.y;
        }

        return mouse_Posi;
    }

    void AimArrow() {
        if (Arrow.activeSelf == false){
            Arrow.SetActive(true);
        }
    }
}
