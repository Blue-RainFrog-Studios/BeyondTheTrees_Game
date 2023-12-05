using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyController
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
            case (EnemyState.Heal):
                Heal();
                break;
            case (EnemyState.Run):
                Run();

                break;
        }
        if (!notInRoom)
        {
            if (isPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Run;
            }
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange && Vector3.Distance(transform.position, player.transform.position) > range && !room.GetComponent<RoomController>().lowHealth())
            {
                currState = EnemyState.Attack;

            }
            else if (Vector3.Distance(transform.position, player.transform.position) < attackRange && Vector3.Distance(transform.position, player.transform.position) > range && room.GetComponent<RoomController>().lowHealth())
            {
                if (room.GetComponent<RoomController>().heal)
                {
                    currState = EnemyState.Attack;
                }
                else
                {
                    currState = EnemyState.Heal;
                }
            }
            else if (Vector3.Distance(transform.position, player.transform.position) < range)
            {
                currState = EnemyState.Run;
            }
            else
            {

                currState = EnemyState.Follow;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
        direction = player.transform.position - transform.position;
        if (!(Vector3.Distance(transform.position, player.transform.position) < attackRange && currState != EnemyState.Run))
        {
            if (direction.x > 0.0f)
            {
                if (direction.y + 1.0f > direction.x && currState != EnemyState.Run)
                {
                    animator.Play("WalkBackRangedGoblin");

                }
                else if (direction.y + 1.0f < direction.x && currState != EnemyState.Run)
                {
                    animator.Play("WalkRightRangedGoblin");

                }
                else if (direction.y + 1.0f > direction.x && currState == EnemyState.Run)
                {
                    animator.Play("WalkFrontRangedGoblin");
                }
                else
                {
                    animator.Play("WalkLeftRangedGoblin");
                }
            }
            else if (direction.x < 0.0f)
            {
                if (direction.y + 1.0f < direction.x && currState != EnemyState.Run)
                {
                    animator.Play("WalkFrontRangedGoblin");

                }
                else if (direction.y + 1.0f > direction.x && currState != EnemyState.Run)
                {
                    animator.Play("WalkLeftRangedGoblin");

                }
                else if (direction.y + 1.0f < direction.x && currState == EnemyState.Run)
                {
                    animator.Play("WalkBackRangedGoblin");
                }
                else
                {
                    animator.Play("WalkRightRangedGoblin");
                }
            }
        }
        else
        {
            if (direction.x > 0.0f)
            {
                if (direction.y + 1.0f > direction.x)
                {
                    animator.Play("AttackBackGoblin");


                }
                else
                {
                    animator.Play("AttackRightGoblin");


                }
            }
            else if (direction.x < 0.0f)
            {
                if (direction.y + 1.0f < direction.x)
                {
                    animator.Play("AttackFrontGoblin");


                }
                else
                {
                    animator.Play("AtackLeftGoblin");


                }
            }
        }

    }
}
