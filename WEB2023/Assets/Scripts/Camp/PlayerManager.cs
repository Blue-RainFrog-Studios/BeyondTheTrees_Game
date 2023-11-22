using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private GameObject pausePrefab;

    GameObject pause;

    private void Awake()
    {
        pause = GameObject.FindGameObjectWithTag("Pause");
        player = GameObject.FindGameObjectWithTag("Player");
        if (pause == null)
        {
            Instantiate(pausePrefab);
        }
        player.GetComponent<KnightScript>().ResetStats();
    }

    private void Start()
    {
        player.GetComponent<PlayerMovementInputSystem>().enabled = true;
        player.GetComponentInChildren<Canvas>().enabled = true;
    }
}
