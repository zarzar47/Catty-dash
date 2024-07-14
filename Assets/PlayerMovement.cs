using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 aim_direction = new Vector3(0, 0, 0);
    public float maxSpeed = 10f;
    public float moveMultipler = 5f;
    public float bounceDampen = 0.8f;
    public float maxDragLength = 6.5f;
    private UnityEngine.Vector3 arrowVector = new UnityEngine.Vector3(0,0,0);
    private UnityEngine.Vector3 mouse_Posi = new UnityEngine.Vector3(0,0,0);
    public Transform center;
    private new Rigidbody rigidbody;
    private Vector3 old_velo = new Vector3(0,0,0);
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate(){
        old_velo = rigidbody.velocity;
        ClampVelocity();
    }
    private void OnMouseDrag() {
        UnityEngine.Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen coordinates
        Ray ray = Camera.main.ScreenPointToRay(mousePosition); // Convert screen coordinates to a ray
        float length = 0;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            mouse_Posi = hit.point; // Get the world position of the mouse click
            mouse_Posi.y = center.position.y;
            UnityEngine.Vector3 playerPosition = center.position; // Get the player's position

            // Calculate the direction vector from the player to the click position
            UnityEngine.Vector3 direction = mouse_Posi - playerPosition;

            // The length of the vector is the distance between the player and the click position
             length = direction.magnitude;
            if (length > maxDragLength)
                length = maxDragLength;
            // Normalize the direction vector to get a unit vector and scale it by the distance
            arrowVector = direction.normalized * length;
        }

        if (length > 1.2f)
            transform.LookAt(mouse_Posi);
    }

    private void OnMouseUp() {
        rigidbody.AddForce(arrowVector * moveMultipler, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other) {
         if (other.gameObject.tag == "Bouceable"){
            ApplyReflection(other);
         }
    }

     void OnDrawGizmos()
    {
        if (gameObject != null)
        {
            // Draw the arrow vector from the player to the click position
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(center.position, mouse_Posi);
        }
    }

    void ApplyReflection(Collision collider){
        Vector3 collisionNormal = collider.GetContact(0).normal;
        Vector3 incomingVelocity = old_velo;
        Debug.Log("incomingVelo "+incomingVelocity);
        Debug.Log("collisionNormal "+collisionNormal);
        Vector3 reflectDir = Vector3.Reflect (incomingVelocity , collisionNormal);
        Debug.Log("ReflectDir " +reflectDir);
        transform.rotation.SetFromToRotation(rigidbody.velocity.normalized, reflectDir);
        reflectDir *= 0.8f;
        rigidbody.AddForce(reflectDir, ForceMode.Impulse);
    }

    void ClampVelocity()
    {
        // Calculate the current speed of the Rigidbody
        float currentSpeed = rigidbody.velocity.magnitude;

        // If the current speed exceeds the maximum speed, clamp it
        if (currentSpeed > maxSpeed)
        {
            // Normalize the velocity vector and scale it to the maximum speed
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }
    }
}
