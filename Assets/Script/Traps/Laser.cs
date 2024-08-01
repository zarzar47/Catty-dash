using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
   
   void OnCollisionEnter(Collision other)
   {
    if (other.gameObject.tag == "Player"){
        PlayerManager.Instance.killed();
    }
   }
}
