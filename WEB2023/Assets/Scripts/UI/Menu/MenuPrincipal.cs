using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class MenuPrincipal : MonoBehaviour
{

    [SerializeField] All_Items_Database laDB;

    public void Jugar()
    {
        Debug.Log("Entrando a la siguiente Pantalla");

    }

    public void Salir()
    {
        Application.Quit();
    }
}
