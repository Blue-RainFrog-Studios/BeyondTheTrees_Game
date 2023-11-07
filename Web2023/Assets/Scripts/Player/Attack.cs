using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float velocity;
    
    [SerializeField] private float damage;

    private Vector2 pmad;

    private void Awake()
    {
        pmad = FindAnyObjectByType<PlayerMovementInputSystem>().attackDirection;
    }

    private void Start()
    {
        damage = GameObject.FindWithTag("Player").GetComponent<KnightScript>().attack;
    }

    private void FixedUpdate()
    {
        transform.Translate(pmad*velocity*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {  
            collision.GetComponent<EnemyController>().RecieveDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Prop"))
        {
            Destroy(gameObject);
        }
    }
}
