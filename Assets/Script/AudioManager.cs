using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips = new AudioClip[1];

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void playClip(int i){
        audioSource.clip = audioClips[i];
        audioSource.Play();
    }
}
