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
        GetComponentInChildren<Canvas>().gameObject.SetActive(true);
        player.GetComponentInChildren<Canvas>().enabled = true;
    }
}
