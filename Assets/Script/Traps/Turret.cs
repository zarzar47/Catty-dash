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
    public float bulletSpeed = 5f;


    void OnTriggerStay(Collider other)
    {
        if (!player.GetComponent<PlayerMovement>().invisible)
        {
            //Debug.Log("hit");
            if (other.tag == "Player")
            {
                passedTime += Time.deltaTime;
                Vector3 difference = player.transform.position - turret.transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(difference);
                //turret.transform.LookAt(difference);
                turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
                if (passedTime >= waitTime)
                {
                    CreateBullet();
                    passedTime = 0;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") passedTime = 0;
    }

    void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = gun.transform.forward * bulletSpeed;
    }
}
