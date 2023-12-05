using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Actions_Wolf : MonoBehaviour
{

    #region Varibles
    private GameObject player;
    private Transform playerTransform;
    private Transform WolfTransform;
    private bool ended;
    private bool endedDazed;

    [SerializeField] private float TimeCharge;
    [SerializeField] private Animator animator;
    bool collisionDetected = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        WolfTransform = GetComponent<Transform>();
    }

    #region MethodsIdle
    public void StartMethodWaiting()
    {
        //si el jugador esta a 1 metro de distancia
        //animator.Play("Patroll");

    }
    public Status UpdateMethodWaiting()
    {
        if (Vector2.Distance(playerTransform.position, WolfTransform.position) < 3f || GetComponent<WolfController>().HP < 50)
        {
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }
    #endregion

    #region MethodsWalk
    public void StartMethodWalk()
    {
        Debug.Log("ANDANDO AL JUGADOR");
        //animator.Play("WalkFront");
    }
    public Status UpdateMethodWalk()
    {
        //make the object move to the player position
        WolfTransform.position = Vector2.MoveTowards(WolfTransform.position, playerTransform.transform.position, GetComponent<WolfController>().speed * Time.deltaTime);
        return Status.Running;
    }
    #endregion

    #region Charging
    public void StartMethodCharge()
    {
        Debug.Log("cargando");
        ended = false;
        //animator.Play("Charge");
        //cambia el color a morado
        StartCoroutine(WaitSeconds(TimeCharge));
    }
    public Status UpdateMethodCharge()
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



    //cambiar bite a lgun tipo de dash hacia el jugador
    #region MethodBite
    public void StartMethodBiteAttack()
    {
        Debug.Log("cargando al jugador");
        collisionDetected = false;
        //screenShake = GetComponent<ScreenShake>();
        GetComponent<Knockback>().strength = 30f;
        ended = false;
        if (playerTransform.position.x > WolfTransform.position.x)
            animator.Play("WalkSideRightDG");
        else
            animator.Play("WalkSideDG");

        if (playerTransform.position.x > WolfTransform.position.x)
        {
            StartCoroutine(WalkRightUntilCollision(this.gameObject, GetComponent<WolfController>().speed));
            //screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        else
        {
            StartCoroutine(WalkLeftUntilCollision(this.gameObject, GetComponent<WolfController>().speed));
            //screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
    }


    public Status UpdateMethodBiteAttack()
    {
        //move right for 2 seconds
        //coroutine that moves the object to the right for 2 seconds

        if (collisionDetected)
        {
            GetComponent<Knockback>().strength = 10f;
            StopAllCoroutines();
            collisionDetected = false;
            return Status.Success;
        }
        return Status.Running;
    }
    #endregion

    #region Dazed
    public void StartMethodDazed()
    {
        Debug.Log("estoy stuned");
        ended = false;
        //animator.Play("Charge");
        //cambia el color a morado
        StartCoroutine(WaitSeconds(TimeCharge));
    }
    public Status UpdateMethodDazed()
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
    public bool CheckPlayerInBiteRange()
    {
        return Vector2.Distance(playerTransform.position, WolfTransform.position) < 2f;
    }

    public bool CheckCollisionWithYAxis()
    {
        return ((playerTransform.position.y > WolfTransform.position.y - 1) && (playerTransform.position.y < WolfTransform.position.y + 1));
    }
    
    public bool CheckItemExist()
    {
        //comprobar item existe
        return true;
        //return GetComponent<DavidElGnomoController>().HP < GetComponent<DavidElGnomoController>().HPSecondPhase;
    }
    #endregion


    #region Courutines
    IEnumerator WaitSeconds(float Time)
    {
        yield return new WaitForSeconds(Time);
        ended = true;
    }


    IEnumerator WalkRightUntilCollision(GameObject gnomoObj, float speed)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = gnomoObj.transform.position;
        Vector3 targetPos = startingPos + Vector3.right * 20;

        while (gnomoObj.GetComponent<Collider2D>())
        {
            gnomoObj.transform.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime) * 0.45f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ended = true;

    }
    IEnumerator WalkLeftUntilCollision(GameObject gnomoObj, float speed)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = gnomoObj.transform.position;
        Vector3 targetPos = startingPos + Vector3.left * 20;
        //mientras que no haya collisiones
        while (gnomoObj.GetComponent<Collider2D>())
        {
            gnomoObj.transform.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime) * 0.45f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ended = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionDetected = true;
    }
    #endregion

}
