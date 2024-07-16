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
    private float Score = 0f;
    public TextMeshProUGUI timerGUI;
    public TextMeshProUGUI scoreGUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer(){
        accum_timer += Time.deltaTime;
        Timer = (int) Math.Floor(accum_timer);
        timerGUI.text = "Timer "+Timer;
    }

    void UpdateScore(int score){
        Score += score;
        scoreGUI.text = "Score "+Score;
    }
}

