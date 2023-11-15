using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 direccion;
    Vector2 reflejado_aux;
    bool pega = false;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Damos una velocidad inicial
        rb.velocity = new Vector2(1, -1) * 10;
    }
    void FixedUpdate()
    {

        //Almacenamos la velocidad que lleva antes de la colision
        if (!pega)
        {
            direccion = rb.velocity;
        }

        
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        pega = true;
        //coll.contacts nos devuelve una matriz con los contactos de la colision
        Vector2 reflejado = Vector2.Reflect(direccion, coll.contacts[0].normal);
        rb.velocity = reflejado;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        pega = false;
    }
}
