using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityPotion : MonoBehaviour
{
    //we can simply cahnge the TAG of the player to make them invisible
    public float invisibility_Duration = 2f;
     void OnTriggerEnter(Collider other)
     {
         if (other.tag == "Player")
         {
            
             Destroy(gameObject);
         }
         
     }
}
