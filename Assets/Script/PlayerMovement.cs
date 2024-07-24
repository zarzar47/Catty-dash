using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    [SerializeField, Range(0,1)]private float timeSlowDown = 1;
    public float maxSpeed = 10f;
    public float minSpeedToKill = 7f;
    public float moveMultipler = 5f;
    public float maxDragLength = 6.5f;
    private bool aiming = false;
    private bool moving = false;
    private UnityEngine.Vector3 direction = new UnityEngine.Vector3(0,0,0);
    private UnityEngine.Vector3 original_mouse_Posi = new UnityEngine.Vector3(0,0,0);
    public Transform center;
    public GameObject cursor;
    private new Rigidbody rigidbody;
    private Vector3 old_velo = new Vector3(0,0,0);
    private Material ArrowMat;
    private Animator arrowAnim;
    private ParticleController _particleController;
    private float length = 0;
    private LevelManager levelManager;
    private AudioManager audioSource;
    void Start()
    {
        audioSource = GetComponentInChildren<AudioManager>();
        GameObject levelManagerObj = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager = levelManagerObj.GetComponent<LevelManager>();
        Arrow.SetActive(false);
        rigidbody = GetComponent<Rigidbody>();
        ArrowMat = Arrow.GetComponentInChildren<MeshRenderer>().materials[0];
        arrowAnim = Arrow.gameObject.GetComponentInChildren<Animator>();
        _particleController = GetComponentInChildren<ParticleController>();
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
        cursor.SetActive(true);
        cursor.transform.position = original_mouse_Posi;
    }

    private void SlingShotTrajectory() {
        UnityEngine.Vector3 current_mousePos = GetMousePos();
        UnityEngine.Vector3 original_Posi = original_mouse_Posi; // Get the player's position
        // Calculate the direction vector from the player to the click position
        //BIG due to how the game is set up Z = Y in the code (OK)
        //dont worry about whats happening here (ask rafay)
        direction = current_mousePos - original_Posi;
        direction.z = direction.y;
        direction.y = 0.5f;
        // The length of the vector is the distance between the player and the click position
        length = direction.magnitude;
        if (length > maxDragLength){
            length = maxDragLength;
        } else if (length <0.25){
            length = 0;
            return;
        }
        // Normalize the direction vector to get a unit vector and scale it by the distance

        if (Arrow.activeSelf){
            float var = length / maxDragLength;
            if (var >= 0f && var < 0.33f && ArrowMat.color != Color.yellow){
                ArrowMat.color = Color.yellow;
                TweenArrow();
            } else if (var >= 0.33f && var < 0.66f && ArrowMat.color != Color.blue){
                ArrowMat.color = Color.blue;
                TweenArrow();
            } else if (var >= 0.66f && var <=1f && ArrowMat.color != Color.red){
                ArrowMat.color = Color.red;
                TweenArrow();
            }
        }

        LookTowards(direction * -1);
        AimArrow();

    }

    private void SlingShotAction() {
        float var = length / maxDragLength;

        if (var >= 0f && var < 0.33f)
            var = 0.33f;
        else if (var >= 0.33f && var < 0.66f)
            var = 0.66f;
        else if (var >= 0.66f)
            var = 1f;
        
        rigidbody.AddForce(direction.normalized * (moveMultipler * var) * -1, ForceMode.Impulse);
        moving = false;
        Arrow.SetActive(false);
        Time.timeScale = 1;
        ArrowMat.color = Color.white;
        _particleController.TriggerParticle(direction.normalized, center.position, 0, 1f);
        cursor.SetActive(false);


        audioSource.playClip(0);//0 is woosh
    }

    private void OnCollisionEnter(Collision other) {
         if (other.gameObject.tag == "Bouceable"){
            ApplyReflection(other);
         } else if (other.gameObject.tag == "Enemy"){
            if (rigidbody.velocity.magnitude >= minSpeedToKill){
                if (!other.gameObject.GetComponent<EnemyAI>().isEnraged()) {
                    levelManager.UpdateScore(10);
                    audioSource.playClip(1); // 1 is slash
                    Destroy(other.gameObject);
                }
            } else {
                ApplyReflection(other);
            }
         }
    }

    void ApplyReflection(Collision collider) {
        //getting the normal vector from the contact on the object (not the player's point of contact)
        Vector3 collisionNormal = collider.GetContact(0).normal;
        Vector3 incomingVelocity = old_velo;
        Vector3 reflectDir = Vector3.Reflect (incomingVelocity , collisionNormal); // reflecting the velocity of the player along the normal vector's plane
        transform.rotation.SetFromToRotation(rigidbody.velocity.normalized, reflectDir); // rotating the player to the new vector's direction
        reflectDir *= 0.8f;
        _particleController.TriggerParticle(reflectDir * -1, collider.GetContact(0).point, 1, 0.25f);
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
        UnityEngine.Vector3 mousePosition = Input.mousePosition;

        return mousePosition;
    }

    void AimArrow() {
        if (Arrow.activeSelf == false){
            Arrow.SetActive(true);
        }
    }

    void TweenArrow(){
        arrowAnim.Play("ArrowShake",0);
    }

    void LookTowards(Vector3 target){
        Vector3 direction = target - transform.position;
        direction.y = this.transform.position.y;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
