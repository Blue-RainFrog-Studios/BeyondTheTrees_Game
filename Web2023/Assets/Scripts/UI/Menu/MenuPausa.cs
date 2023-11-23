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
    private GameObject cajaPausa;

    [SerializeField]
    private GameObject menuConfirmacion;

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
        cajaPausa.SetActive(true);
        menuConfirmacion.SetActive(false);
        menuPausa.SetActive(false);
    }
    public void VolverAlCampamento()
    {
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name == "Campamento Base") return;
        SceneManager.LoadScene("Campamento Base");
        player.GetComponent<KnightScript>().ResetStats();
        player.GetComponent<KnightScript>().MoneyDealer(0.5f,player.GetComponent<CoinCounter>().expeditionMoney);
        player.GetComponent<PlayerMovementInputSystem>().nivel = 0;
        player.GetComponent<KnightScript>().ResetMoneyCanvas();
        player.transform.position = new Vector2(0, -4);
    }
    public void ConfirmarMenu()
    {
        menuConfirmacion.SetActive(true);
        cajaPausa.SetActive(false);
    }

    public void NoSalir()
    {
        menuConfirmacion.SetActive(false);
        cajaPausa.SetActive(true);
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
