using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public float speed = 0.05f;
    public float TimeToEnrage = 1f;
    public float viewDistance = 0;
    public float FOV = 90f;
    private bool enraged = false;
    private float meter = 0;
    private Material Enemy_material;
    public LayerMask PlayerLayer;
    [SerializeField] private GameObject pfFieldOfView;
    private GameObject objFieldOfView;
    private FieldOfView fieldOfView;
    private AudioSource soundManager;
    private AudioClip huh;
    private PlayerMovement playerObj;
    private bool playerNotfound = true;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = FindObjectOfType<PlayerMovement>();
        Enemy_material = GetComponentInParent<MeshRenderer>().material;
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

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player") && enraged)
    //     {
    //     }
    // }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         if (!enraged)
    //         {
    //             meter += Time.deltaTime * 2;
    //             Enemy_material.color = Color.Lerp(Color.white, Color.red, meter);
    //             if (meter > TimeToEnrage)
    //             {
    //                 enraged = true;
    //             }
    //         }
    //         else if (enraged)
    //         {
                
    //             //LookTowards(other.transform.position);
    //             //transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed * Time.deltaTime);
    //         }
    //     }
    // }

    private void PlayerFound(){
        Debug.Log("Player found");
        soundManager.Play();
        playerNotfound = false;
        Invoke("LevelFailLoad", soundManager.clip.length);
    }
    private void LevelFailLoad(){
        SceneManager.LoadScene("LevelFailed");
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         Enemy_material.color = Color.white;
    //         meter = 0;
    //         enraged = false;
    //     }
    // }


    public bool isEnraged(){
        return enraged;
    }

    private void OnDestroy() {
        Destroy(objFieldOfView);
    }

    public void DetectPlayer(){
        if (!playerNotfound)
            return;
        if (PlayerInArea()){
            Debug.Log("Player Detected");
            RaycastHit hit;
            Vector3 pos = playerObj.transform.position - transform.position;
            
            if (Vector3.Angle(-1 * transform.forward,pos) < FOV/2){
                if (Physics.Raycast(transform.position, pos.normalized , out hit, viewDistance)){
                    if (hit.collider.tag == "Player"){
                        Debug.Log("RayHit");
                        meter += Time.deltaTime * 2 / TimeToEnrage;
                        Enemy_material.color = Color.Lerp(Color.white, Color.red, meter);
                        if (Enemy_material.color == Color.red){
                            PlayerFound();
                        }
                    }
                }
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
