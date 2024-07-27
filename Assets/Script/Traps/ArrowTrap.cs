using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public Transform[] arrows;
    public GameObject arrow;
    public float arrowSpeed = 5f;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            shootArrows();
        }
    }

    public void shootArrows(){
        for (int i = 0; i < arrows.Length; i++){
            float random = Random.Range(0, 10);
            if(random >= 2){
                GameObject arro = Instantiate(arrow, arrows[i].position, arrows[i].rotation);
                arro.GetComponent<Rigidbody>().velocity = arrows[i].transform.forward * arrowSpeed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

