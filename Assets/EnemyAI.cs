using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 0.05f;
    public float TimeToEnrage = 1f;
    private bool enraged = false;
    private bool follow = false;
    private float meter = 0;
    private Material Enemy_material;
    // Start is called before the first frame update
    void Start()
    {
        Enemy_material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void LateUpdate()
    {
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && enraged){
            follow = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player"){
            if (enraged == false){
                meter+=Time.deltaTime * 2;
                Enemy_material.color = Color.Lerp(Color.white, Color.red, meter);
                if (meter>TimeToEnrage)
                   enraged = true;
            } else if (enraged){
                this.transform.position = Vector3.MoveTowards(this.transform.position, other.transform.position, speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player"){
            Enemy_material.color = Color.white;
            meter = 0;
            enraged = false;
        }    
    }
}