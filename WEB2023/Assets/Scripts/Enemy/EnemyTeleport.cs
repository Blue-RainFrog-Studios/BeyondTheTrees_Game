using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : EnemyController
{
    
    public float rangeTeleport;
    private void Awake()
    {
        Idle();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        room = GameObject.FindGameObjectWithTag("RoomController");
    }


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
            case (EnemyState.GoHeal):
                GoHeal();
                break;
            case (EnemyState.Teleport):
                Teleport();
                break;
        }

        if (!notInRoom)
        {

            if (isPlayerInRange(range) )
            {
                currState = EnemyState.Follow;
            }else if(!isPlayerInRangeTeleport(rangeTeleport) )
            {
                currState = EnemyState.Follow;
            }
            else if (isPlayerInRangeTeleport(rangeTeleport))
            {
                currState = EnemyState.Teleport;
            }
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange && currState != EnemyState.GoHeal)
            {
                currState = EnemyState.Attack;
            }
            if (healed == false && room.GetComponent<RoomController>().healing && GetComponent<Enemy>().life < iLife)
            {
                currState = EnemyState.GoHeal;
            }

        }
        else
        {
            currState = EnemyState.Idle;
        }
        direction = player.transform.position - transform.position;
        if (isPlayerInRange(range))
        {
            if (direction.x > 0.0f)
            {
                if (direction.y + 1.0f > direction.x)
                {
                    animator.Play("WalkBackTeleGoblin");


                }
                else
                {
                    animator.Play("WalkRightTeleGoblin");


                }
            }
            else if (direction.x < 0.0f)
            {
                if (direction.y + 1.0f < direction.x)
                    animator.Play("WalkFrontTeleGoblin");
                else
                    animator.Play("WalkLeftTeleGoblin");

            }
        }
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
