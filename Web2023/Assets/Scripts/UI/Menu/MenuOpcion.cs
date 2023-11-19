using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOpcion : MonoBehaviour
{ private bool act = true;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(player.GetComponent<PlayerMovementInputSystem>().nivel > 0 && player.GetComponent<PlayerMovementInputSystem>().nivel < 3 && RoomController.boosDoor && act==false)
        {
            act = true;
            //this.transform.GetChild(0).gameObject.SetActive(true);
            SceneManager.LoadScene("LoandingBoss");
            
        }
        else if(player.GetComponent<PlayerMovementInputSystem>().nivel >= 3)
        {
            SceneManager.LoadScene("Victory");
            //this.transform.GetChild(1).gameObject.SetActive(true);
            player.GetComponent<PlayerMovementInputSystem>().nivel = 0;
            //this.transform.GetChild(1).gameObject.transform.Find("VictoriaESP").gameObject.SetActive(true);
            //StartCoroutine(Wait(10));
        }
    }
    public void VolverAlCampamento()
    {
        SceneManager.LoadScene("Campamento Base");
        //player.GetComponent<KnightScript>().ResetMoneyCanvas();
        player.transform.position = new Vector2(0, -4);
        player.GetComponent<PlayerMovementInputSystem>().nivel = 0;
    }
    public void ContinuarLaRun()
    {
        SceneManager.LoadScene("Basement");
        //player.GetComponent<KnightScript>().ResetMoneyCanvas();
        player.transform.position = new Vector2(0, 0);
    }

    IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        VolverAlCampamento();
    }
}
