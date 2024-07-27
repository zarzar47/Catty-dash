using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public GameObject laser;
    private float cooldownTimer = 0f;
    public float cooldown = 2f;
    public float activateTime = 2f;
    private float activeTimer = 0f;
    public bool active = false;


    // Update is called once per frame
    void Update()
    {
        if (!active)
            cooldownTimer += Time.deltaTime;
        else
            activeTimer += Time.deltaTime;

        if (active && activeTimer >= activateTime){
            activeTimer = 0f;
            laser.SetActive(false);
            active = false;
        }

        if (!active && cooldownTimer >= cooldown){
            cooldownTimer = 0f;
            laser.SetActive(true);
            active = true;
        }
    }
}
