using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }
    public static int levelComp = 0;
    public static int currentLevel = 0;
    public static int max_levels = 9;
    public float loading_time = 3f;

    private void Awake()
    {
        // Implement the Singleton pattern
        if (Instance != null && Instance != this)
        {
            // Destroy the old instance
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator CoLoadLevel(int lvlNum)
    {
        // Load the loading screen
        SceneManager.LoadScene("Loading");
        yield return new WaitForSeconds(loading_time);

        // Load the specified level if it's available, else load the "LevelNotAvail" scene
        if (lvlNum <= max_levels)
        {
            SceneManager.LoadScene("Level " + lvlNum);
            currentLevel = lvlNum;
            Debug.Log("Current Level: " + lvlNum);
        }
        else
        {
            SceneManager.LoadScene("LevelNotAvail");
            Debug.Log("Level Not Available: " + lvlNum);
        }
    }

    public void LoadLevel(int lvlNum)
    {
        StartCoroutine(CoLoadLevel(lvlNum));
    }

    public void LoadNext()
    {
        LoadLevel(currentLevel + 1);
    }

    public void RestartLevel()
    {
        if (currentLevel == 0)
        {
            currentLevel = 1; // Default to the first level if currentLevel is not set
        }
        SceneManager.LoadScene("Level " + currentLevel);
        Debug.Log("Loaded Level: " + currentLevel);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("Home Screen");
    }
}
