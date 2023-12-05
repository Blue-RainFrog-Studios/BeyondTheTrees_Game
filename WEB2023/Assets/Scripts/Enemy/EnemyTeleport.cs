using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : EnemyController
{
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
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            case (EnemyState.Die):
                Die();
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

            if (isPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (isPlayerInRangeTeleport(rangeTeleport) && currState != EnemyState.Die)
            {
                currState = EnemyState.Teleport;
            }
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {
                currState = EnemyState.Attack;
            }
            if (healed == false && room.GetComponent<RoomController>().lowHealth() && room.GetComponent<RoomController>().heal)
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

}
