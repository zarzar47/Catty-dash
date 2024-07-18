using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenReturn : MonoBehaviour
{
    void OnMouseUp()
    {
        string name = gameObject.name;

        switch(name)
        {
            case "Level1":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Level2":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Level3":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Level4":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Level5":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Level6":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Level7":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Level8":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Level9":
                SceneManager.LoadScene("SampleScene");
                break;
        
        }
    }
}
