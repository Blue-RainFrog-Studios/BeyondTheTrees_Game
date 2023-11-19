using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionsDavidElGnomo : MonoBehaviour
{
    #region Varibles
    private GameObject player; //        player = GameObject.FindGameObjectWithTag("Player");
    private Transform playerTransform;
    private Transform DavidElGnomoTransform;

    [SerializeField] private float TimeTired;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject littleGnome;
    public bool invulnerable = false;
    private bool hasBeenPlayed = false;
    Vector2 walkAttackDistance = new Vector2(0, 2);
    ScreenShake screenShake;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        DavidElGnomoTransform = GetComponent<Transform>();
    }

    #region MethodsWalk
    public void StartMethodWalk()
    {
        Debug.Log("ANDANDO AL JUGADOR");
        animator.Play("WalkFrontDG");
    }
    public Status UpdateMethodWalk()
    {
        //make the object move to the player position
        DavidElGnomoTransform.position = Vector2.MoveTowards(DavidElGnomoTransform.position, playerTransform.transform.position, GetComponent<DavidElGnomoController>().speed * Time.deltaTime);
        return Status.Running;
    }
    #endregion

    #region MethodsWalkAttack
    public void StartMethodWalkAttack()
    {
        screenShake = GetComponent<ScreenShake>();
        GetComponent<Knockback>().strength = 30f;
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
            StartCoroutine(MoveRightForTwoSeconds(DavidElGnomoTransform, GetComponent<DavidElGnomoController>().speed));
            screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        else
        {
            StartCoroutine(MoveLeftForTwoSeconds(DavidElGnomoTransform, GetComponent<DavidElGnomoController>().speed));
            screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        if (ended){
            GetComponent<Knockback>().strength = 10f;
            StopAllCoroutines();
            return Status.Success;
        }
        return Status.Running;
    }
    #endregion

    #region MethodsPunch
    public void StartMethodPunch()
    {
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
    #endregion

    #region MethodsGnomeMode
    public void StartMethodGnomeMode()
    {
        StopAllCoroutines();
        animator.Play("Idle");
        StartCoroutine(InvokeGnomes(1f));
        StartCoroutine(InvokeGnomes(2f));
        StartCoroutine(InvokeGnomes(3f));
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
    #endregion

    #region MethodsTired
    public void StartMethodTired()
    {
        ended=false;
        animator.Play("Idle");
        //cambia el color a morado
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
    #endregion

    #region CheckTransitions
    public bool CheckPlayerInPunchRange()
    {
        return Vector2.Distance(playerTransform.position, DavidElGnomoTransform.position) < 2f;
    }

    public bool CheckCollisionWithYAxis()
    {
        return ((playerTransform.position.y > DavidElGnomoTransform.position.y-1) && (playerTransform.position.y < DavidElGnomoTransform.position.y + 1));
    }

    public bool CheckHPVeryLow()
    {
        return GetComponent<DavidElGnomoController>().HP < GetComponent<DavidElGnomoController>().HPSecondPhase;
    }
    #endregion

    #region Courutines
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

    IEnumerator InvokeGnomes(float timeSpawn)
    {
        yield return new WaitForSeconds(timeSpawn); ;
        Instantiate(littleGnome, new Vector3(new System.Random().Next((int)DavidElGnomoTransform.position.x-3, (int)DavidElGnomoTransform.position.x+3), new System.Random().Next((int)DavidElGnomoTransform.position.y-3, (int)DavidElGnomoTransform.position.y + 3), 0), Quaternion.identity);
    }

    IEnumerator PlayAnimation(string animationName)
    {
        animator.Play(animationName);
        yield return null;
        ended= true;
    }
    #endregion


}


