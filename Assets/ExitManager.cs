using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            Debug.Log("Player destroyed");
            Destroy(other.gameObject);
        }
    }
}
