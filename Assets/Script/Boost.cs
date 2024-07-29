using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostSpeedMultiplier = 2f;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            other.GetComponent<Rigidbody>().velocity *= boostSpeedMultiplier;
        }
    }
}
