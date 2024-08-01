using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeKill : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player"){
            PlayerManager.Instance.killed();
        }
    }
}
