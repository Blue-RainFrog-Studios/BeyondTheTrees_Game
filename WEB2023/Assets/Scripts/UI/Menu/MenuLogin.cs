using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLogin : MonoBehaviour
{
    private bool genero;
    private int edad;
    [SerializeField] private Button maleButton;
    [SerializeField] private Button femaleButton;
    private string nombreUsuario = "Invitado";
    [SerializeField]
    private GameObject playerPrefab;

    public void volverMenuPrincipal()
    {
        //No usamos la escena
        //Usamos los cambios de escena activando y desactivando (mas rapido)
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void esMasculino()
    {
        genero = true;
        maleButton.GetComponent<Image>().color = Color.black;
        femaleButton.GetComponent<Image>().color = Color.white;
        Debug.Log("Es Hombre"+genero);
    }
    public void esFemenino()
    {
        genero=false;
        femaleButton.GetComponent<Image>().color = Color.black;
        maleButton.GetComponent<Image>().color = Color.white;
        Debug.Log("Es Mujer"+genero);
    }
    public void nuevoNombre(string s)
    {
        //Se modifica cuando pulsamos cualquier cosa
        //El texto se modifica al utilizar la funcion de string dinamica
        //No hace falta coger el texto del inputField
        if (s != "")
            nombreUsuario = s;
        Debug.Log("El nombre de usuario es: " + nombreUsuario);
    }
    public void empezarJuego()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        SceneManager.LoadScene("Campamento Base");
        if (player == null)
        {
            playerPrefab.GetComponent<Name>().nombre = nombreUsuario;
            playerPrefab.GetComponent<Name>().sexo = genero;
            playerPrefab.GetComponent<Name>().CambiarDatos();
            Instantiate(playerPrefab);
        }
        else
        {
            player.GetComponent<Name>().nombre = nombreUsuario;
            player.GetComponent<Name>().sexo = genero;
            player.GetComponent<Name>().CambiarDatos();
            player.transform.position = new Vector2(0, -4);
            player.GetComponent<PlayerMovementInputSystem>().enabled = true;
            player.GetComponentInChildren<Canvas>().enabled = true;
        }
    }
}
