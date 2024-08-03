using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float cooldown = 2f;
    private float timer = 0f;
    public GameObject Spikes;
    public Material activeMat;
    private new MeshRenderer renderer;
    private Material normal_mat;
    private bool activated = false;
    private Animator animator;

    private void Start() {
        renderer = Spikes.GetComponent<MeshRenderer>();
        normal_mat = renderer.sharedMaterial;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            other.GetComponent<PlayerManager>().killed();
        }
    }
    
    private void ActivateTrap(){
        activated = !activated;
        renderer.material = activated? activeMat : normal_mat;
        animator.SetTrigger("Spike");
    }
    void Update()
    {
            timer += Time.deltaTime;
            if (timer > cooldown){
                timer = 0f;
                ActivateTrap();
            }
    }
}

