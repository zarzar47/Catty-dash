using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
