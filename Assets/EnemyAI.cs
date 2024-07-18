using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 0.05f;
    public float TimeToEnrage = 1f;
    private bool enraged = false;
    private float meter = 0;
    private Material Enemy_material;

    // Start is called before the first frame update
    void Start()
    {
        Enemy_material = GetComponentInParent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && enraged)
        {
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!enraged)
            {
                meter += Time.deltaTime * 2;
                Enemy_material.color = Color.Lerp(Color.white, Color.red, meter);
                if (meter > TimeToEnrage)
                {
                    enraged = true;
                }
            }
            else if (enraged)
            {
                Debug.Log("Player found");
                //LookTowards(other.transform.position);
                //transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Enemy_material.color = Color.white;
            meter = 0;
            enraged = false;
        }
    }

    void LookTowards(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0;
        Debug.DrawLine(transform.position, target, Color.red); // Draw a line towards the target
        if (direction != Vector3.zero)
        {
            transform.LookAt(direction);
        }
    }
}
