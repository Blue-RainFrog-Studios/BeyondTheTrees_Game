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
    public void VolverAlCampamento()
    {
        SceneManager.LoadScene("Campamento Base");
        //player.GetComponent<KnightScript>().ResetMoneyCanvas();
        player.transform.position = new Vector2(0, -4);
    }
    public void ContinuarLaRun()
    {
        SceneManager.LoadScene("Basement");
        //player.GetComponent<KnightScript>().ResetMoneyCanvas();
        player.transform.position = new Vector2(0, 0);
    }
}
