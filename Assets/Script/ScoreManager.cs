using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI Score_Text;
    public TextMeshProUGUI Time_Text;
    private int Score = 0;
    private int Time = 0;
    // Start is called before the first frame update
    public void Awake()
    {
        DataGatherer dataGatherer = DataGatherer.getInstance();
        Score =  dataGatherer.getScore();
        Time = dataGatherer.getTime();
        Score_Text.text = "Score: "+Score.ToString();
        Time_Text.text = "Time: "+Time.ToString();
    }


}
