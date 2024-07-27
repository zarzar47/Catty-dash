using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public Transform[] arrows;
    public GameObject arrow;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            shootArrows();
        }
    }

    public void shootArrows(){
        for (int i = 0; i < arrows.Length; i++){
            float random = Random.Range(0, 10);
            if(random >= 2){
                Instantiate(arrow, arrows[i].position, arrows[i].rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

