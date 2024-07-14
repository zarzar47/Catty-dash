using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_follow : MonoBehaviour
{
    [SerializeField]
    private Transform obj_to_follow;
    private GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(obj_to_follow.position.x, transform.position.y, obj_to_follow.position.z);
    }
}
