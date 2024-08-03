using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider musicSlider;
    public Slider SfxSlider;
    public AudioMixer myMixer;
    // Start is called before the first frame update
    public void SetMusicVolume(){
        float volume = musicSlider.value;
        myMixer.SetFloat("Music",Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(){
        float volume = SfxSlider.value;
        myMixer.SetFloat("SFX",Mathf.Log10(volume) * 20);
    }
}
