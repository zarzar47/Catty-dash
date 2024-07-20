using System.Collections;
using UnityEngine;

public class TimeManager: MonoBehaviour
{
    public static float targetTime;
    public bool flag = false;
    public GameObject player; 
    
    void Start(){
        targetTime = 0.0f;
    }
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