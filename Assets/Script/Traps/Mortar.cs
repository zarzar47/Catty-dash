using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    public float waitTime = 1f;
    public float passedTime = 0f;
    public GameObject shell;
    public GameObject player;
    public Transform shoot;
    public float speed = 25f;
    public float rotSpeed = 15f;

    void Update()
    {
        passedTime += Time.deltaTime;

        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        float? angle = RotateTurret();

        if (passedTime >= waitTime){
            if (angle != null){
                //Debug.Log("Not 0");
                Shoot();
                passedTime = 0;
            }/*
            else{
                this.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
            }*/
        }
    }

    void Shoot(){
        //Debug.Log("hit");
        GameObject shot = Instantiate(shell, shoot.position, shoot.rotation);
        shot.GetComponent<Rigidbody>().velocity = speed * shoot.forward;
    }

    float? RotateTurret(){
        float? angle = CalculateAngle(false);
        if (angle != null){
            shoot.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }
        return angle;
    }

    float? CalculateAngle(bool low){
        Vector3 targetDir = player.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0;
        float x = targetDir.magnitude;
        float gravity = 9.8f;
        float sSqr = speed * speed;
        float underSqrRoot = sSqr * sSqr - gravity * (gravity * x * x + 2 * y * sSqr);
        //Debug.Log("sSqr: " + sSqr);
        //Debug.Log("x: " + x);
        //Debug.Log("y: " + y);
        //Debug.Log("underroot: " + underSqrRoot);

        if (underSqrRoot >= 0f){
            float root = Mathf.Sqrt(underSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
                return Mathf.Atan2(lowAngle, gravity*x) * Mathf.Rad2Deg;
            else
                return Mathf.Atan2(highAngle, gravity*x) * Mathf.Rad2Deg;
        }
        else
            return null;
    }
}
