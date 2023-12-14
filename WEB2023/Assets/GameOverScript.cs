using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }
    public void Return()
    {
        SceneManager.LoadScene("Campamento Base");
        player.GetComponent<ReactionNPCs>().comingBack = true;
        player.GetComponent<ReactionNPCs>().expedicionFallida = true;
    } 
}
