using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Arrow;
    [SerializeField, Range(0,1)]private float timeSlowDown = 1;
    public float maxSpeed = 10f;
    public GameObject arrowPrefab;
    public float minSpeedToKill = 7f;
    public float moveMultipler = 5f;
    public float maxDragLength = 6.5f;
    private UnityEngine.Vector3 direction = new UnityEngine.Vector3(0,0,0);
    [HideInInspector] public Transform center;
    private new Rigidbody rigidbody;
    private Vector3 old_velo = new Vector3(0,0,0);
    private Material ArrowMat;
    private ParticleController _particleController;
    private float length = 0;
    private LevelManager levelManager;
    private AudioManager audioSource;
    private Vector3 mousePos;
    private PlayerManager playerManager;
    public bool input = true;
    void Start()
    {
        center = this.transform;
        audioSource = GetComponentInChildren<AudioManager>();
        GameObject levelManagerObj = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager = levelManagerObj.GetComponent<LevelManager>();
        rigidbody = GetComponent<Rigidbody>();
        ArrowMat = arrowPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterial;
        _particleController = GetComponentInChildren<ParticleController>();
        playerManager = PlayerManager.Instance;
    }

    void Update()
    {

        if (input){
            if (Input.GetMouseButtonDown(0)){
                mousePos = GetMousePos();
                if (playerManager.timeSlowDownUnlocked) {
                    Debug.Log("Slowed");
                    Time.timeScale = timeSlowDown;
                }
            } else if (Input.GetMouseButton(0)){
                SlingShotTrajectory(mousePos);
            } else if (Input.GetMouseButtonUp(0)){
                SlingShotAction();
            }
        }
        
    }

    void FixedUpdate(){
        old_velo = rigidbody.velocity;
        ClampVelocity();
    }


    private void SlingShotTrajectory(Vector3 mousePos) {
        if (Arrow == null){
            Arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity, transform);
        } else if (Arrow.activeSelf == false){
            Arrow.SetActive(true);
        }
        UnityEngine.Vector3 current_mousePos = GetMousePos();
        UnityEngine.Vector3 original_Posi = mousePos;
        direction = current_mousePos - original_Posi;
        direction.z = direction.y;
        direction.y = 0.5f;
        length = direction.magnitude;
        if (length > maxDragLength){
            length = maxDragLength;
        } else if (length < maxDragLength * 0.1f){
            length = 0;
            return;
        }

        if (Arrow.activeSelf){
            float var = length / maxDragLength;
            if (var >= 0f && var < 0.33f && ArrowMat.color != Color.yellow){
                ArrowMat.color = Color.yellow;
            } else if (var >= 0.33f && var < 0.66f && ArrowMat.color != Color.blue){
                ArrowMat.color = Color.blue;
            } else if (var >= 0.66f && var <=1f && ArrowMat.color != Color.red){
                ArrowMat.color = Color.red;
            }
        }

        LookTowards(direction * -1);
        AimArrow();

    }

    private void SlingShotAction() {
        if (length == 0)
            return;
        
        playerManager.state = CurrentState.Moving;
        rigidbody.velocity = Vector3.zero;
        float var = length / maxDragLength;

        if (var >= 0f && var < 0.33f)
            var = 0.33f;
        else if (var >= 0.33f && var < 0.66f)
            var = 0.66f;
        else if (var >= 0.66f)
            var = 1f;
        
        rigidbody.AddForce(transform.forward * (moveMultipler * var), ForceMode.Impulse);
        Arrow.SetActive(false);
        Time.timeScale = 1;
        ArrowMat.color = Color.white;
        _particleController.TriggerParticle(direction.normalized, center.position, 0, 1f);

        audioSource.playClip(0);//0 is woosh
    }

    private void OnCollisionEnter(Collision other) {
         if (other.gameObject.tag == "Bouceable"){
            ApplyReflection(other);
         } else if (other.gameObject.tag == "Enemy"){
            if (rigidbody.velocity.magnitude >= minSpeedToKill){
                EnemyAI ai = other.gameObject.GetComponent<EnemyAI>();
                if (!ai.isEnraged()) {
                    levelManager.UpdateScore(10);
                    audioSource.playClip(1); // 1 is slash
                    ai.playDeath();
                    CameraShake.Shake(0.2f);
                    ApplyReflection(other);
                }
            } else {
                ApplyReflection(other);
            }
         }
    }

    
    private void ApplyReflection(Collision collider) {
        //getting the normal vector from the contact on the object (not the player's point of contact)
        Vector3 collisionNormal = collider.GetContact(0).normal;
        Vector3 incomingVelocity = old_velo;
        Vector3 reflectDir = Vector3.Reflect(incomingVelocity , collisionNormal); // reflecting the velocity of the player along the normal vector's plane
        transform.rotation.SetFromToRotation(rigidbody.velocity.normalized, reflectDir); // rotating the player to the new vector's direction
        reflectDir *= 0.8f;
        _particleController.TriggerParticle(reflectDir * -1, collider.GetContact(0).point, 1, 2f);
        //application of the force
        rigidbody.AddForce(reflectDir, ForceMode.Impulse);
        transform.LookAt(reflectDir);
    }

    void ClampVelocity()
    {
        float currentSpeed = rigidbody.velocity.magnitude;
        
        if (currentSpeed > maxSpeed) {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }

        if (rigidbody.velocity.magnitude < 0.1f){
            rigidbody.velocity = Vector3.zero;
            playerManager.state = CurrentState.Stationary;
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

    void LookTowards(Vector3 target){
        Vector3 direction = target - transform.position;
        direction.y = this.transform.position.y;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
