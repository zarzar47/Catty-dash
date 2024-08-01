using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet_Destruction : MonoBehaviour
{
    float timePassed = 0f;

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 3f){
            Debug.Log("timed out");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player"){
            if (!other.gameObject.GetComponent<PlayerMovement>().invisible){
                Destroy(gameObject);
                PlayerManager.Instance.killed();
            }
        }
        else{
            Destroy(gameObject);
        }
    }
}
