using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitManager : MonoBehaviour
{
    private LevelManager levelManager;
    public int enemies = 0;
    public bool exit_available;
    private Animator animator;

    private void Start() {
        enemies = FindObjectsOfType<EnemyAI>().Length;
        animator = GetComponent<Animator>();
        GameObject levelManagerObj = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager = levelManagerObj.GetComponent<LevelManager>();
    }

    private void Update() {
        if (levelManager.GetScore() == enemies * 10){
            animator.SetTrigger("Uncage");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            levelManager.UpdateData();
            SceneManager.LoadScene("LevelEnd");
        }
    }
}
