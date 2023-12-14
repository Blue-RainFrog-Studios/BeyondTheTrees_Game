using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Name : MonoBehaviour
{
    public string nombre;
    public TMP_Text texto;
    public bool sexo;
    public string sex;
    public int edad;
    private void Awake()
    {
        if(sexo) 
            sex = "M";
        else
            sex = "F";
    }
    public void CambiarDatos()
    {
        texto.text = nombre;
    }
}
