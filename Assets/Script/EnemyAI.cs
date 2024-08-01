using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    public Transform center;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
        playerObj = FindObjectOfType<PlayerMovement>();
        objFieldOfView = Instantiate(pfFieldOfView, null);
        fieldOfView = objFieldOfView.GetComponent<FieldOfView>();
        
        fieldOfView.SetOriginPoint(transform.position);
        fieldOfView.SetAimDirection(transform.forward);
        fieldOfView.SetDistance(viewDistance);
        fieldOfView.SetFov(FOV);

        soundManager = GetComponent<AudioSource>();
    }

    void Update(){
        fieldOfView.SetOriginPoint(transform.position);
        fieldOfView.SetAimDirection(transform.forward);
        if (!playerObj.invisible){
            DetectPlayer();
        }
        
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
        Invoke("DestroyObj",2f);
        animator.SetTrigger("dead");
        dead = true;
    }

    private void DestroyObj(){
        particleSystem.Play();
        Destroy(this.transform.GetChild(0).gameObject);
        Destroy(this.gameObject, 3f);

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
                        Debug.Log("Player being found");
                        // If player not invisible, then continue, otherwise dont (Lets try to not make this coupled)
                        //if (!(hit.collider.gameObject.GetComponent<PlayerMovement>().invisible)) {
                        float meter = Time.deltaTime / TimeToEnrage;
                        fieldOfView.AddAggro(meter);
                        if (fieldOfView.GetAggro() >= fieldOfView.MaxValue){
                            PlayerFound();
                        }
                    }
                }
            }
        } else {
                fieldOfView.DecreaseAggro(1f * Time.deltaTime);
        }
    }

    private bool PlayerInArea(){
        if (Vector3.Distance(transform.position, playerObj.center.position) < viewDistance){
            return true;
        }
        return false;
    }

}
