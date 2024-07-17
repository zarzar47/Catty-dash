using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            Debug.Log("Player destroyed");
            Destroy(other.gameObject);
            SceneManager.LoadScene("LevelEnd");
        }
    }
}
