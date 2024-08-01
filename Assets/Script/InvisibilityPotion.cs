using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityPotion : MonoBehaviour
{
    //we can simply cahnge the TAG of the player to make them invisible
    public float invisibilityDuration = 2f;
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Player"){
    //         other.GetComponent<PlayerMovement>().invisible = true;
    //         other.GetComponent<PlayerMovement>().invisibility_Duration = invisibilityDuration;
    //         Destroy(gameObject);
    //     }
    // }
}
