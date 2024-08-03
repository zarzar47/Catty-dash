using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterScreenUI : MonoBehaviour
{
    public Button timeSlowDownButton;
    public TextMeshProUGUI timeSlowDownButtonText;
    public Button disableSpikesButton;
    public TextMeshProUGUI disableSpikesButtonText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI starText;
    public Button timeSlowDownError;
    public Button timeSlowDownSuccess;
    public Button disableSpikesError;
    public Button disableSpikesSuccess;

    PlayerManager playerManager;

    void Awake()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();

        starText.text = "Stars: " + playerManager.stars;
        levelText.text = "Level " + playerManager.level;

        if (playerManager.timeSlowDownUnlocked){
            timeSlowDownButton.interactable = false;
            timeSlowDownButtonText.text = "Already Unlocked";
        }

        if (playerManager.disableSpikesUnlocked){
            disableSpikesButton.interactable = false;
            disableSpikesButtonText.text = "Already Unlocked";
        }
    }

    void Update(){
        starText.text = "Stars: " + playerManager.stars;
        levelText.text = "Level " + playerManager.level;

        if (playerManager.timeSlowDownUnlocked){
            timeSlowDownButton.interactable = false;
            timeSlowDownButtonText.text = "Already Unlocked";
        }

        if (playerManager.disableSpikesUnlocked){
            disableSpikesButton.interactable = false;
            disableSpikesButtonText.text = "Already Unlocked";
        }
    }


    public void disableSpikesClicked(){
        if (playerManager.stars >= 9){
            playerManager.unlockDisableSpikes();

            starText.text = "Stars: " + playerManager.stars;
            disableSpikesButton.interactable = false;
            disableSpikesButtonText.text = "Already Unlocked";

            disableSpikesSuccess.gameObject.SetActive(true);
            Invoke("disableTimeSlowDownSuccess", 2);
        }
        else {
            disableSpikesError.gameObject.SetActive(true);
            Invoke("disableTimeSlowDownError", 2);
        }
    }

    private void disableDisableSpikesSuccess(){
        disableSpikesSuccess.gameObject.SetActive(false);
    }

    private void disableDisableSpikesError(){
        disableSpikesError.gameObject.SetActive(false);
    }

    public void timeSlowDownClicked(){
        Debug.Log(playerManager.stars);
        if (playerManager.stars >= 6){
            playerManager.unlockTimeSlow();

            starText.text = "Stars: " + playerManager.stars;
            timeSlowDownButton.interactable = false;
            timeSlowDownButtonText.text = "Already Unlocked";

            timeSlowDownSuccess.gameObject.SetActive(true);
            Invoke("disableTimeSlowDownSuccess", 2);
        }
        else {
            timeSlowDownError.gameObject.SetActive(true);
            Invoke("disableTimeSlowDownError", 2);
        }
    }

    private void disableTimeSlowDownSuccess(){
        timeSlowDownSuccess.gameObject.SetActive(false);
    }

    private void disableTimeSlowDownError(){
        timeSlowDownError.gameObject.SetActive(false);
    }

    public void returnToMainMenu(){
        SceneManager.LoadScene("Home Screen");
    }

}
