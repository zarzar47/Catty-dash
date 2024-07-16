using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float maxDistance = 5f;
    public float speed = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player!=null){
            Vector3 difference = player.transform.position - this.transform.position;

            float dot = difference.x * this.transform.position.x + difference.z * this.transform.position.z;
            if (dot < 0) return;

            this.transform.LookAt(player.transform.position);

            if (difference.sqrMagnitude > Mathf.Pow(maxDistance, 2)){
                this.transform.position += new Vector3(this.transform.forward.x, 0, this.transform.forward.z) * speed;
                //Debug.DrawRay(this.transform.position, difference, Color.blue, 5);
                Debug.DrawRay(this.transform.position, this.transform.forward * 3, Color.green, 5);
            }
        }
        
    }
}
