using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        player.GetComponent<PlayerMovementInputSystem>().enabled = true;
        //player.GetComponentInChildren<Canvas>().gameObject.SetActive(true);
        //GameObject.Find("CanvasInv").gameObject.transform.Find("Menu").gameObject.SetActive(true);
        player.GetComponentInChildren<Canvas>().enabled = true;
    }
}
