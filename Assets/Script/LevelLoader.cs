using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static int levelComp = 0;
    private static int currentLevel = 0;
    private static int max_levels = 1;
    public void loadLevel(int lvlNum){
        if (lvlNum <=max_levels){
            SceneManager.LoadScene("Level "+lvlNum);
            currentLevel = lvlNum;
            Debug.Log("current Level " +lvlNum);
        } else {
            Debug.Log("Level Not Available " +lvlNum);
        }
    }

    public void LoadNext(){
        loadLevel(currentLevel+1);
    }

    public void RestartLevel(){
        if (currentLevel == 0){
            currentLevel = 1;
        }
        SceneManager.LoadScene("Level " +currentLevel);
        Debug.Log("Loaded Level " +currentLevel);
    }

    public void LoadHome(){
        SceneManager.LoadScene("Home Screen");
    }
}
