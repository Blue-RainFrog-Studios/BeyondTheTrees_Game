using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void pantallaCompleta(bool esCompleta)
    {
        Screen.fullScreen = esCompleta;
    }
    public void cambiarVolumen(float volume)
    {
        audioMixer.SetFloat("Volumen", volume);
    }

    public void cambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index+1); 
    }
}
