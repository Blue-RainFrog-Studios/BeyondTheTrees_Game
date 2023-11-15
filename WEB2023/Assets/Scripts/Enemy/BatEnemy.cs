using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 direction;
    GameObject player;
    bool col = false;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        //Damos una velocidad inicial
        rb.velocity = new Vector2(1, -1) * 10;
    }
    void FixedUpdate()
    {

        //Almacenamos la velocidad que lleva antes de la colision
        if (!col)
        {
            direction = rb.velocity;
        }

        
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        col = true;
        //coll.contacts nos devuelve una matriz con los contactos de la colision
        Vector2 reflejado = Vector2.Reflect(direction, coll.contacts[0].normal);
        rb.velocity = reflejado;
   
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        col = false;
    }
}
