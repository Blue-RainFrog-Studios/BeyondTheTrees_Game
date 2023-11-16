using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsSquirrel : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform acornTransform;
    [SerializeField] private Transform squirrelTransform;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    [SerializeField] private Animator animator;
    private bool acornExists = false;


    //SE EJECUTA MIENTRAS ESTA EN EL ESTADO DE WALKTOPLAYER
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void StartMethodWalkToPlayer()
    {
        Debug.Log("ANDANDO AL JUGADOR");
        GetComponent<SpriteRenderer>().color = Color.blue;
        //animator.Play("WalkFrontDG");
    }
    public Status UpdateMethodWalkToPlayer()
    {
        //make the object move to the player position
        squirrelTransform.position = Vector2.MoveTowards(squirrelTransform.position, playerTransform.transform.position, speed * Time.deltaTime);
        return Status.Running;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //SE EJECUTA MIENTRAS ESTA EN EL ESTADO DE WALKTOPLAYERATTACK
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void StartMethodWalkAttack()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        ended = false;
        if (playerTransform.position.x > squirrelTransform.position.x)
            animator.Play("WalkSideRightDG");
        else
            animator.Play("WalkSideDG");

    }

    private bool ended;

    //SE EJECUTA MIENTRAS ESTA EN EL ESTADO DE PUNCH
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void StartMethodAcorn()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //StartCoroutine(PlayAnimation("PunchDG"));
        animator.Play("PunchDG");
    }
    public Status UpdateMethodAcorn()
    {
        //wait 2 seconds
        //if the animation "PunchDG" is playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PunchDG"))
        {
            return Status.Running;
        }
        else
        {
            return Status.Success;
        }
    }
    

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public bool CheckInAcornRange()
    {
        return Vector2.Distance(acornTransform.position, squirrelTransform.position) < 2f;
    }

    public bool CheckCollisionWithYAxis()
    {
        return ((playerTransform.position.y > squirrelTransform.position.y - 1) && (playerTransform.position.y < squirrelTransform.position.y + 1));
    }

    ///////////////////////////////////////////

    public bool CheckAcornExists()
    {
        return acornTransform != null;
    }



    //ESTO EN UN FUTURO DEBE ESTAR EN UN ENEMYCONTROLLER
    void Die()
    {
        Destroy(gameObject);
    }
}


