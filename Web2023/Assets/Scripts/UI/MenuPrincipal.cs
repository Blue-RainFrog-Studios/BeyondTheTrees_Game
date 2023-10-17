using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void jugar()
    {
        //No usamos la escena
        //Usamos los cambios de escena activando y desactivando (mas rapido)
        SceneManager.LoadScene("LoginScene");
        Debug.Log("Entrando a la siguiente Pantalla");
    }
}
