using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float damage;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up*velocity*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("Enemy"))
        //{
        //    collision.GetComponent<Enemy>().TomarDaño(damage);
        //    Destroy(gameObject);
        //}
    }
}
