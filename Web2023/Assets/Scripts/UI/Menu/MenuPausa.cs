using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPausa;
    [SerializeField]
    private GameObject botonPausa;
    private GameObject player;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Pausa()
    {
        Time.timeScale = 0F;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }
    public void Reanudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }
    public void VolverAlCampamento()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Campamento Base");
        player.GetComponent<KnightScript>().resetStats();
        player.GetComponent<KnightScript>().MoneyDealer(0.5f,player.GetComponent<CoinCounter>().expeditionMoney);
        player.GetComponent<PlayerMovementInputSystem>().nivel = 0;
        player.GetComponent<KnightScript>().ResetMoneyCanvas();
        player.transform.position = new Vector2(0, -4);
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);

    }
    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
        //player.GetComponent<PlayerMovementInputSystem>().enabled = false;
        player.GetComponent<KnightScript>().ResetMoneyCanvas();
        player.GetComponent<PlayerMovementInputSystem>().nivel = 0;
        player.GetComponent<PlayerMovementInputSystem>().enabled = false;
        player.GetComponentInChildren<Canvas>().enabled = false;
        //player.gameObject.SetActive(false);
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);

    }
}
