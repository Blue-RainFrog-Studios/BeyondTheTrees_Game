using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    [SerializeField] private int damage;
    GameObject player;
    [SerializeField] private GameObject punch;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<KnightScript>().ReceiveAttack(damage);
            collision.GetComponent<Knockback>().PlayFeedback(punch, player.GetComponent<Rigidbody2D>());
        }
    }
}
