using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private GameObject cursor;
    private void Start(){
        cursor = transform.GetChild(2).gameObject;
        cursor.SetActive(false);
    }

    private void Update(){
        if (Input.GetMouseButtonDown(0)){
            cursor.SetActive(true);
            cursor.transform.position = Input.mousePosition;
        } else if (Input.GetMouseButton(0)){
        } else if (Input.GetMouseButtonUp(0)){
            cursor.SetActive(false);
        }
    }
}
