using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    
    private PlayerMovement playerMovement;
    public CurrentState state = CurrentState.Stationary;
    public static PlayerManager Instance { get; private set;}
    private bool invisible = false;
    private String prev_tag = "";
    public int base_exp = 10;
    private String saveFilePath;

    // Powerups
    public bool timeSlowDownUnlocked = false;
    public bool disableSpikesUnlocked = false;


    // Attributes
    public int level = 1;
    public int stars = 6;
    public int experience = 0;
    


    // Methods for progression
    public void addStars(int s){ stars += s;}
    public void addXP(int xp){
        experience += xp;
        if (experience > base_exp * Math.Pow(2, level - 1)){
            level += 1;
            stars += 1;
            experience -= (int)(base_exp * Math.Pow(2, level - 1));
        }
    }


    // Methods to unlock powerups from the character screen
    public void unlockTimeSlow(){
        if (stars >= 6) {
            stars -= 6;
            timeSlowDownUnlocked = true;
            //return "Successfully unlocked Time Slow Down ability!";
        }
        //return "Insufficient stars :(";
    }

    public void unlockDisableSpikes(){
        if (stars >= 9) {
            stars -= 9;
            disableSpikesUnlocked = true;
            //return "Successfully unlocked Disabling Spikes ability!";
        }
        //return "Insufficient stars :(";
    }


    // Methods to load and save data and initialize the object

    private void  Awake(){
        prev_tag = this.transform.gameObject.tag;
         if (Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        saveFilePath = Application.persistentDataPath + "/KatanaMasterSave.json";
        LoadPlayerData();
    }

    private void OnApplicationQuit(){
        SavePlayerData();
    }



    public void killed(){
        SceneManager.LoadScene("LevelFailed");
        //Invoke("LevelFailed", soundManager.clip.length);
    }

    private void OnDestroy() {
        Time.timeScale = 1f;    
    }

    private void EnableInput(bool input){
        playerMovement.input = input;
    }



    // Methods to enable invisibility
    public void EnableInvisibility(float duration){
        invisible = true;
        
        this.transform.gameObject.tag = "Undetected";
        Invoke("DisableInvisibility", duration);
    }

    private void DisableInvisibility(){
         this.transform.gameObject.tag = prev_tag;
    }


    //Methods to save and load player progress
    private void SavePlayerData()
    {
        PlayerData data = new PlayerData
        {
            level = level,
            stars = stars,
            experience = experience,
            timeSlowDownUnlocked = timeSlowDownUnlocked,
            disableSpikesUnlocked = disableSpikesUnlocked
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
    }

    private void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            level = data.level;
            stars = data.stars;
            Debug.Log(stars);
            experience = data.experience;
            timeSlowDownUnlocked = data.timeSlowDownUnlocked;
            disableSpikesUnlocked = data.disableSpikesUnlocked;
        }
    }
}

public enum CurrentState{
        Stationary,
        Moving,
        Dead,
}
