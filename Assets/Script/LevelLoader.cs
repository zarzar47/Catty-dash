using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static int levelComp = 0;
    public static int currentLevel = 0;
    public static int max_levels = 9;
    public float loading_time = 3f;
    private IEnumerator ColoadLevel(int lvlNum){

        SceneManager.LoadScene("Loading");
        yield return new WaitForSeconds(loading_time);

        if (lvlNum <=max_levels){
            SceneManager.LoadScene("Level "+lvlNum);
            currentLevel = lvlNum;
            Debug.Log("current Level " +lvlNum);
        } else {
            SceneManager.LoadScene("LevelNotAvail");
            Debug.Log("Level Not Available " +lvlNum);
        }
    }

    public void loadLevel(int lvlNum){
        StartCoroutine(ColoadLevel(lvlNum));
    }

    private void Awake() {
        DontDestroyOnLoad(this);
    }
    
    public void LoadNext(){
        loadLevel(currentLevel + 1);
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
