using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private GameObject punch;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<KnightScript>().ReceiveAttack(damage);
            GetComponent<Knockback>().PlayFeedback(punch, player);
        }
    }
}
