using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsDavidElGnomo : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform DavidElGnomoTransform;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float HP;
    [SerializeField] private float HPGnomeMode;
    [SerializeField] private float TimeTired;
    [SerializeField] private Animator animator;
    private bool invulnerable = false;
    private bool hasBeenPlayed = false;
    Vector2 walkAttackDistance = new Vector2(0, 2);

    //SE EJECUTA MIENTRAS ESTA EN EL ESTADO DE WALKTOPLAYER
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void StartMethodWalk()
    {
        Debug.Log("ANDANDO AL JUGADOR");
        GetComponent<SpriteRenderer>().color = Color.blue;
        animator.Play("WalkFrontDG");
    }
    public Status UpdateMethodWalk()
    {
        //make the object move to the player position
        DavidElGnomoTransform.position = Vector2.MoveTowards(DavidElGnomoTransform.position, playerTransform.transform.position, speed * Time.deltaTime);
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
        if (playerTransform.position.x > DavidElGnomoTransform.position.x)
            animator.Play("WalkSideRightDG");
        else
        animator.Play("WalkSideDG");

    }

    private bool ended;

    public Status UpdateMethodWalkAttack()
    {
        //move right for 2 seconds
        //coroutine that moves the object to the right for 2 seconds
        if (playerTransform.position.x > DavidElGnomoTransform.position.x) {
            StartCoroutine(MoveRightForTwoSeconds(DavidElGnomoTransform, speed));
        }
        else
        {
            StartCoroutine(MoveLeftForTwoSeconds(DavidElGnomoTransform, speed));
        }
        if (ended){
            StopAllCoroutines();
            return Status.Success;
        }
        return Status.Running;
    }

    //SE EJECUTA MIENTRAS ESTA EN EL ESTADO DE PUNCH
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void StartMethodPunch()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //StartCoroutine(PlayAnimation("PunchDG"));
        animator.Play("PunchDG");
    }
    public Status UpdateMethodPunch()
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
    //SE EJECUTA MIENTRAS ESTA EN EL ESTADO DE GNOMEMODE
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void StartMethodGnomeMode()
    {
        StopAllCoroutines();
        GetComponent<SpriteRenderer>().color = Color.yellow;
        animator.Play("Idle");
        StartCoroutine(WaitSeconds(5));
        invulnerable = true;
    }
    public Status UpdateMethodGnomeMode()
    {
        if (ended)
        {
            invulnerable = false;
            hasBeenPlayed = true;
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 
    /// 
    /// 
    /// 
    public void StartMethodTired()
    {
        ended=false;
        animator.Play("Idle");
        //cambia el color a morado
        GetComponent<SpriteRenderer>().color = Color.magenta;
        StartCoroutine(WaitSeconds(TimeTired));
    }
    public Status UpdateMethodTired()
    {
        if (ended)
        {
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }
    /// 
    /// 
    /// 
    /// 
    /// 
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public bool CheckPlayerInPunchRange()
    {
        return Vector2.Distance(playerTransform.position, DavidElGnomoTransform.position) < 2f;
    }

    public bool CheckCollisionWithYAxis()
    {
        return ((playerTransform.position.y > DavidElGnomoTransform.position.y-1) && (playerTransform.position.y < DavidElGnomoTransform.position.y + 1));
    }

    public bool CheckHPLow()
    {
        return HP<HPGnomeMode && hasBeenPlayed;
    }

    /// <summary>
    /// ////////////////////////////////////////
    /// </summary>
    /// 
    //EN UN FUTURO MOVE RIGHT HASTA QUE TE CHOQUES CON UNA PARED
    IEnumerator MoveRightForTwoSeconds(Transform objectTransform, float speed)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = objectTransform.position;
        Vector3 targetPos = startingPos + Vector3.right*5;

        while (elapsedTime < 2f)
        {
            objectTransform.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ended = true;

    }
    IEnumerator MoveLeftForTwoSeconds(Transform objectTransform, float speed)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = objectTransform.position;
        Vector3 targetPos = startingPos + Vector3.left*5;

        while (elapsedTime < 2f)
        {
            objectTransform.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ended = true;
    }

    IEnumerator WaitSeconds(float Time)
    {
        yield return new WaitForSeconds(Time);
        ended = true;
    }

    IEnumerator PlayAnimation(string animationName)
    {
        animator.Play(animationName);
        yield return null;
        ended= true;
    }

    ///////////////////////////////////////////

    //ESTO EN UN FUTURO DEBE ESTAR EN UN ENEMYCONTROLLER
    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    public void RecieveDamage(float damage)
    {
        if (!invulnerable) { 
            HP -= damage;
            Debug.Log("Recibo daño");
            if (HP <= 0)
            {
                Die();
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invulnerable) { 
            if (collision.gameObject.CompareTag("Player"))
            {
                player.GetComponent<KnightScript>().ReceiveAttack(damage);
            }
        }
    }
 }


