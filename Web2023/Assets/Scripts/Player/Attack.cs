using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private Animator animator;
    [SerializeField] private float damage;

    private Vector2 pmad;

    private void Awake()
    {
        pmad = FindAnyObjectByType<PlayerMovementInputSystem>().attackDirection;
        //StartCoroutine(waiter());
    }

    private void Start()
    {
        damage = GameObject.FindWithTag("Player").GetComponent<KnightScript>().attack;
        animator.Play("AttackDist");
    }

    private void FixedUpdate()
    {
        transform.Translate(pmad*velocity*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().RecieveDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<DavidElGnomoController>().RecieveDamage(damage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Root"))
        {
            collision.GetComponent<RootController>().RecieveDamage((int)damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("LittleGnome"))
        {
            collision.GetComponent<LittleGnomes>().RecieveDamage((int)damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("GhostTree"))
        {
            collision.GetComponent<GhostTree>().RecieveDamage((int)damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("BossTree"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Prop"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
