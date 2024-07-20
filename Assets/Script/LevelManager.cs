using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public float Max_Time = 0;
    private int Timer = 0;
    private float accum_timer = 0f;
    private int Score = 0;
    public TextMeshProUGUI timerGUI;
    public TextMeshProUGUI scoreGUI;
    private DataGatherer dataGatherer;
    // Start is called before the first frame update
    void Start()
    {
        dataGatherer = DataGatherer.getInstance();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void UpdateTimer(){
        accum_timer += Time.deltaTime;
        Timer = (int) Math.Floor(accum_timer);
        timerGUI.text = "Timer "+Timer;
    }

    public void UpdateScore(int score){
        Score+=score;
        scoreGUI.text = "Score "+Score;
    }

    public void UpdateData(){
        dataGatherer.UpdateTime(Timer);
        dataGatherer.UpdateScore(Score);
        LevelLoader.levelComp+=1;
    }

}

