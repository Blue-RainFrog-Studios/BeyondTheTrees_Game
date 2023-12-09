using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : Enemy
{
    Rigidbody2D rb;
    Vector2 direction;
    GameObject player;
    bool col = false;
    public Animator animator;
    // Use this for initialization
    void Start()
    {
        animator.Play("BatAnimation");
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        //Damos una velocidad inicial
        rb.velocity = new Vector2(1, -1) * 4;
    }
    private void Update()
    {
        if(!coolDownAttack)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {
                player.GetComponent<KnightScript>().ReceiveAttack(damage * player.GetComponent<KnightScript>().bleed);
                player.GetComponent<KnightScript>().bleed += 1;
                StartCoroutine(CoolDown());
            }
            

        }


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
