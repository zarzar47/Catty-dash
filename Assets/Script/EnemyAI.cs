using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public float speed = 0.05f;
    public float TimeToEnrage = 1f;
    public float viewDistance = 0;
    public float FOV = 90f;
    private bool enraged = false;
    public LayerMask PlayerLayer;
    [SerializeField] private GameObject pfFieldOfView;
    private GameObject objFieldOfView;
    private FieldOfView fieldOfView;
    private AudioSource soundManager;
    private Animator animator;
    private PlayerMovement playerObj;
    private bool playerNotfound = true;
    private new ParticleSystem particleSystem;
    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
        playerObj = FindObjectOfType<PlayerMovement>();
        objFieldOfView = Instantiate(pfFieldOfView, null);
        fieldOfView = objFieldOfView.GetComponent<FieldOfView>();
        
        fieldOfView.setOriginPoint(transform.position);
        fieldOfView.SetAimDirection(transform.forward);
        fieldOfView.SetDistance(viewDistance);
        fieldOfView.SetFov(FOV);

        soundManager = GetComponent<AudioSource>();
    }

    void Update(){
        fieldOfView.setOriginPoint(transform.position);
        fieldOfView.SetAimDirection(transform.forward);
        DetectPlayer();
        
    }


    private void PlayerFound(){
        Debug.Log("Player found");
        soundManager.Play();
        playerNotfound = false;
        animator.SetTrigger("PlayerCaught");
        Invoke("LevelFailLoad", soundManager.clip.length);
    }
    private void LevelFailLoad(){
        SceneManager.LoadScene("LevelFailed");
    }


    public bool isEnraged(){
        return enraged;
    }

    public void playDeath() {
        if (!playerNotfound){
            return;
        }
        Destroy(objFieldOfView);
        particleSystem.Play();
        animator.SetTrigger("dead");
        dead = true;
    }

    public void DetectPlayer(){
        if (!playerNotfound || dead)
            return;
        if (PlayerInArea()){
            RaycastHit hit;
            Vector3 pos = playerObj.transform.position - transform.position;
            
            if (Vector3.Angle(-1 * transform.forward,pos) < FOV/2){
                if (Physics.Raycast(transform.position, pos.normalized , out hit, viewDistance)){
                    if (hit.collider.tag == "Player"){
                        // If player not invisible, then continue, otherwise dont
                        if (!(hit.collider.gameObject.GetComponent<PlayerMovement>().invisible)) {
                            float meter = Time.deltaTime / TimeToEnrage;
                            fieldOfView.addAggro(meter);
                            if (fieldOfView.getAggro() >= 1){
                                PlayerFound();
                            }
                        }
                    }
                }
            }
        } else {
            if (fieldOfView.getAggro() > 0){
                fieldOfView.decreaseAggro(0.001f);
            }
        }
    }

    private bool PlayerInArea(){
        if (Vector3.Distance(transform.position, playerObj.center.position) < viewDistance){
            return true;
        }
        return false;
    }

}
