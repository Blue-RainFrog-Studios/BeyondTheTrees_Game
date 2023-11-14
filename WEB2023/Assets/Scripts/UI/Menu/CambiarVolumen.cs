using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CambiarVolumen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;


    public void SetMusicValue()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("MusicVolume",Mathf.Log10(volume)*20);
    }
    public void SetSFXValue()
    {
        float SFX = sfxSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFX) * 20);
    }
}
