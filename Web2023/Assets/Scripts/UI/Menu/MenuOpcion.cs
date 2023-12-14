using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOpcion : MonoBehaviour
{
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if(player.GetComponent<PlayerMovementInputSystem>().nivel > 0 && player.GetComponent<PlayerMovementInputSystem>().nivel < 3 && RoomController.boosDoor)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(player.GetComponent<PlayerMovementInputSystem>().nivel == 3)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
            player.GetComponent<PlayerMovementInputSystem>().nivel = 0;
            this.transform.GetChild(1).gameObject.transform.Find("VictoriaESP").gameObject.SetActive(true);
            player.GetComponent<KnightScript>().ResetMoneyCanvas();
            StartCoroutine(Wait(10));
        }
    }
    public void VolverAlCampamento()
    {

        SceneManager.LoadScene("Campamento Base");
        //player.GetComponent<KnightScript>().ResetMoneyCanvas();
        player.transform.position = new Vector2(0, -4);
        player.GetComponent<KnightScript>().MoneyDealer(1f, player.GetComponent<CoinCounter>().expeditionMoney);
        player.GetComponent<KnightScript>().ResetMoneyCanvas();
        player.GetComponent <KnightScript>().ResetStats();
        player.GetComponent<PlayerMovementInputSystem>().nivel = 0;
        player.GetComponent<ReactionNPCs>().comingBack = true;
        player.GetComponent<ReactionNPCs>().expedicionExito = true;
    }
    public void ContinuarLaRun()
    {
        SceneManager.LoadScene("Basement");
        //player.GetComponent<KnightScript>().ResetMoneyCanvas();
        player.transform.position = new Vector2(0, 0);
        player.GetComponent<PlayerMovementInputSystem>().EmpezarContador();
    }

    IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        VolverAlCampamento();
    }
}
