using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private bool active = false;
    private float cooldown = 2f;
    private float timer = 0f;
    public GameObject trap;

    private void Start() {
        trap = transform.GetChild(0).gameObject;
        trap.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            if (!other.GetComponent<PlayerMovement>().invisible){
                active = true;
                trap.SetActive(true);
            }
        }
    }
    
    void Update()
    {
        if (active){
            timer += Time.deltaTime;
            if (timer > cooldown){
                active = false;
                timer = 0;
                trap.SetActive(false);
            }
        }
    }
}

