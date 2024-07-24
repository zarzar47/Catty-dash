using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitManager : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start() {
        GameObject levelManagerObj = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager = levelManagerObj.GetComponent<LevelManager>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            levelManager.UpdateData();
            SceneManager.LoadScene("LevelEnd");
        }
    }
}
