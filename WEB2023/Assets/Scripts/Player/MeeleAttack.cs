using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeeleAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().RecieveDamage(damage);
        }
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<DavidElGnomoController>().RecieveDamage(damage);
        }
        if (collision.CompareTag("LittleGnome"))
        {
            collision.GetComponent<LittleGnomes>().RecieveDamage((int)damage);
        }
        if (collision.CompareTag("GhostTree"))
        {
            collision.GetComponent<GhostTree>().RecieveDamage((int)damage);
        }
        if (collision.CompareTag("Root"))
        {
            collision.GetComponent<RootController>().RecieveDamage((int)damage);
        }
    }
    }

