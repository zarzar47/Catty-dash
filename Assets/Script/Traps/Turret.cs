using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public GameObject player;
    public GameObject turret;
    public GameObject gun;
    public float rotateSpeed;
    public GameObject bullet;
    public float waitTime = 2f;
    private float passedTime = 0f;


    void OnTriggerStay(Collider other)
    {
        passedTime += Time.deltaTime;
        if(other.tag == "Player"){
            Debug.Log("hit");
            Vector3 difference = (player.transform.position - turret.transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(difference);
            Debug.DrawRay(gun.transform.position, difference, Color.yellow);
            turret.transform.LookAt(difference);
            //turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
            if (passedTime >= waitTime){
                CreateBullet();
                passedTime = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") passedTime = 0;
    }

    void CreateBullet(){
        GameObject shell = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = gun.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
