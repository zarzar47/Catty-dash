using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    
    private PlayerMovement playerMovement;
    public CurrentState state = CurrentState.Stationary;
    public bool timeSlowDownUnlocked = true;
    public static PlayerManager Instance { get; private set;}
    private bool invisible = false;
    private String prev_tag = "";
    
    public static bool input = true;

    private void  Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        prev_tag = this.transform.gameObject.tag;
        Instance = this;
    }

    public void killed(){
        SceneManager.LoadScene("LevelFailed");
        //Invoke("LevelFailed", soundManager.clip.length);
    }

    private void OnDestroy() {
        Time.timeScale = 1f;
    }


    public void PauseGame(){
        input = false;
        Time.timeScale = 0f;
    }

    public void UnPauseGame(){
        input = true;
        Time.timeScale = 1f;
    }


    public void EnableInvisibility(float duration){
        invisible = true;
        
        this.transform.gameObject.tag = "Undetected";
        Invoke("DisableInvisibility", duration);
    }

    private void DisableInvisibility(){
         this.transform.gameObject.tag = prev_tag;
    }

}

public enum CurrentState{
        Stationary,
        Moving,
        Dead,
}
