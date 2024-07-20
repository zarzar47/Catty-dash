using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DataGatherer{
    private static DataGatherer dataGatherer = null;
    private int lastScore;
    private int lastTime;
    private DataGatherer(){
        lastScore = 0;
        lastTime = 0;
    }

    public static DataGatherer getInstance(){
        if (dataGatherer == null){
            dataGatherer = new DataGatherer();
        }
        return dataGatherer;
    }

    public void UpdateScore(int score){
        lastScore = score;
    }

    public void UpdateTime(int time){
        lastTime = time;
    }

    public int getScore(){
        return lastScore;
    }

    public int getTime(){
        return lastTime;
    }
}
