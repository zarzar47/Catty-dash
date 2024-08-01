using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    
    private AudioSource soundManager;

    public CurrentState state = CurrentState.Stationary;
    public bool timeSlowDownUnlocked = true;
    public static PlayerManager Instance { get; private set;}

    private void  Awake()
    {
        Instance = this;
    }

    public void killed(){
        SceneManager.LoadScene("LevelFailed");
        //Invoke("LevelFailed", soundManager.clip.length);
    }

    private void LevelFailed(){
        SceneManager.LoadScene("LevelFailed");
    }

    private void OnDestroy() {
        Time.timeScale = 1f;    
    }

}

public enum CurrentState{
        Stationary,
        Moving,
        Dead,
}
