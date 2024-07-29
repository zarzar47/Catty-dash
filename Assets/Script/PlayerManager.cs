using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool timeSlowDownUnlocked = true;

    public static PlayerManager Instance { get; private set;}

    private void  Awake()
    {
        Instance = this;
    }
}
