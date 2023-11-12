using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Name : MonoBehaviour
{
    public string nombre;
    public TMP_Text texto;

    private void Awake()
    {
        texto.text = nombre;
    }
}
