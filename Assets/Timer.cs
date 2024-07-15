using System.Collections;
using UnityEngine;

public class Timer: MonoBehaviour
{
    public float targetTime = 0.0f;
    public bool flag = false;
    public GameObject player; 
    void Update(){
        if (!flag){
            targetTime += Time.deltaTime;
        }
        if (player == null && !flag)
        {
            flag = true;
            Debug.Log("Level completed in " + targetTime + " seconds.");
        }
    }


}