using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : EnemyController
{
    
    // Start is called before the first frame update
    private void Awake()
    {
        Idle();
    }
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        room = GameObject.FindGameObjectWithTag("RoomController");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)        
        {
            case (EnemyState.Idle):
                Idle();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            /*case (EnemyState.Die):
                Die();
                break;*/
            case (EnemyState.GoHeal):
                GoHeal();
                break;
        }
        if (!notInRoom)
        {
            if (isPlayerInRange(range)/* && currState != EnemyState.Die*/)
            {
                currState = EnemyState.Follow;
            }
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {
                currState = EnemyState.Attack;
            }
            if (healed == false  && room.GetComponent<RoomController>().healing && GetComponent<Enemy>().life < iLife)
            {
                currState = EnemyState.GoHeal;
            }
            }
        else
        {
            currState = EnemyState.Idle;
        }

        if (IAmAGhost != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            //do it gradually
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 1 - (distance / 10);
            GetComponent<SpriteRenderer>().color = color;
        }

        direction = player.transform.position - transform.position;
        if (direction.x > 0)
            animator.Play("GhostRight");
        else
            animator.Play("GhostLeft");

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy") && healed == false && can == false)
        {
            StartCoroutine(Wait());

            GetComponent<Enemy>().life += 20;
            if (GetComponent<Enemy>().life >= iLife)
            {
                Debug.Log("Vida tras cura " + GetComponent<Enemy>().life);
                healed = true;
            }
        }
    }


}
