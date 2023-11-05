using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    private PlayerMovementInputSystem playerMovementInputSystem;

    private void Awake()
    {
        playerMovementInputSystem = FindObjectOfType<PlayerMovementInputSystem>().GetComponent<PlayerMovementInputSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            playerMovementInputSystem.gameObject.SetActive(false);
            SceneManager.LoadScene("Basement");
            playerMovementInputSystem.gameObject.transform.position = new Vector2(0.0f, -2.0f);
        }
    }

}

