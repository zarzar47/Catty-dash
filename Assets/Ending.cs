using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ending : MonoBehaviour
{
    public TextMeshProUGUI score;
    public int num = 0;
    // Start is called before the first frame update
    public void Awake()
    {
        num = (int)(TimeManager.targetTime);
        score.text = num.ToString();
    }


}
