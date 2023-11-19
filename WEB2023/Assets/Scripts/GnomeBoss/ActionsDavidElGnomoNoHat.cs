using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionsDavidElGnomoNoHat : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform DavidElGnomoTransform;

    //[SerializeField] private float speed;
    //[SerializeField] private int damage;
    //[SerializeField] private float HP;
    //[SerializeField] private float HPGnomeMode;

    [SerializeField] private float TimeTired;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject littleGnome;

    private bool invulnerable = false;
    private bool hasBeenPlayed = false;

    Vector2 walkAttackDistance = new Vector2(0, 2);
    ScreenShake screenShake;
    #endregion

    #region MethodsWalkNoHat
    public void StartMethodWalkNoHat()
    {
        Debug.Log("ANDANDO AL JUGADOR");
        GetComponent<SpriteRenderer>().color = Color.red;
        animator.Play("WalkFrontNoHat");
    }
    public Status UpdateMethodWalkNoHat()
    {
        //make the object move to the player position
        DavidElGnomoTransform.position = Vector2.MoveTowards(DavidElGnomoTransform.position, playerTransform.transform.position, GetComponent<DavidElGnomoController>().speedNoHat * Time.deltaTime);
        return Status.Running;
    }
    #endregion

    #region MethodsWalkAttackNoHat
    public void StartMethodWalkAttackNoHat()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        screenShake = GetComponent<ScreenShake>();
        GetComponent<Knockback>().strength = 40f;
        ended = false;
        if (playerTransform.position.x > DavidElGnomoTransform.position.x)
            animator.Play("WalkSideNoHat");
        else
            animator.Play("WalkSideLeftNoHat");

    }

    private bool ended;

    public Status UpdateMethodWalkAttackNoHat()
    {
        //move right for 2 seconds
        //coroutine that moves the object to the right for 2 seconds
        if (playerTransform.position.x > DavidElGnomoTransform.position.x)
        {
            StartCoroutine(MoveRightForTwoSeconds(DavidElGnomoTransform, GetComponent<DavidElGnomoController>().speedNoHat));
            screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        else
        {
            StartCoroutine(MoveLeftForTwoSeconds(DavidElGnomoTransform, GetComponent<DavidElGnomoController>().speedNoHat));
            screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        if (ended)
        {
            GetComponent<Knockback>().strength = 10f;
            StopAllCoroutines();
            return Status.Success;
        }
        return Status.Running;
    }
    #endregion

    #region MethodsPunchNoHat
    public void StartMethodPunchNoHat()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //StartCoroutine(PlayAnimation("PunchDG"));
        animator.Play("PunchNoHat");
    }
    public Status UpdateMethodPunchNoHat()
    {
        //wait 2 seconds
        //if the animation "PunchDG" is playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PunchNoHat"))
        {
            return Status.Running;
        }
        else
        {
            return Status.Success;
        }
    }
    #endregion

    #region MethodsGnomeModeNoHat
    public void StartMethodGnomeModeNoHat()
    {
        StopAllCoroutines();

        GetComponent<SpriteRenderer>().color = Color.red;
        
        animator.Play("IdleNoHat");

        StartCoroutine(InvokeGnomes(1f));
        StartCoroutine(InvokeGnomes(2f));
        StartCoroutine(InvokeGnomes(3f)); 
        StartCoroutine(InvokeGnomes(1f));
        StartCoroutine(InvokeGnomes(2f));
        StartCoroutine(InvokeGnomes(3f));
        StartCoroutine(WaitSeconds(6));
        
        invulnerable = true;
    }
    public Status UpdateMethodGnomeModeNoHat()
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

    #region MethodsTiredNoHat
    public void StartMethodTiredNoHat()
    {
        ended = false;
        animator.Play("IdleNoHat");
        //cambia el color a morado
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(WaitSeconds(TimeTired));
    }
    public Status UpdateMethodTiredNoHat()
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
    public bool CheckPlayerInPunchRangeNoHat()
    {
        return Vector2.Distance(playerTransform.position, DavidElGnomoTransform.position) < 2f;
    }

    public bool CheckCollisionWithYAxisNoHat()
    {
        return ((playerTransform.position.y > DavidElGnomoTransform.position.y - 1) && (playerTransform.position.y < DavidElGnomoTransform.position.y + 1));
    }

    public bool CheckHPLowNoHat()
    {
        return GetComponent<DavidElGnomoController>().HP < GetComponent<DavidElGnomoController>().HPGnomeMode && !hasBeenPlayed;
    }
    #endregion

    #region Coroutines
    IEnumerator MoveRightForTwoSeconds(Transform objectTransform, float speed)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = objectTransform.position;
        Vector3 targetPos = startingPos + Vector3.right * 5;

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
        Vector3 targetPos = startingPos + Vector3.left * 5;

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
        Instantiate(littleGnome, new Vector3(new System.Random().Next((int)DavidElGnomoTransform.position.x - 3, (int)DavidElGnomoTransform.position.x + 3), new System.Random().Next((int)DavidElGnomoTransform.position.y - 3, (int)DavidElGnomoTransform.position.y + 3), 0), Quaternion.identity);
    }

    #endregion

}
