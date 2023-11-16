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

    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float HP;
    [SerializeField] private float HPGnomeMode;
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
        GetComponent<SpriteRenderer>().color = Color.green;
        animator.Play("WalkFrontDG");
    }
    public Status UpdateMethodWalkNoHat()
    {
        //make the object move to the player position
        DavidElGnomoTransform.position = Vector2.MoveTowards(DavidElGnomoTransform.position, playerTransform.transform.position, speed * Time.deltaTime);
        return Status.Running;
    }
    #endregion

    #region MethodsWalkAttackNoHat
    public void StartMethodWalkAttackNoHat()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        screenShake = GetComponent<ScreenShake>();
        GetComponent<Knockback>().strength = 40f;
        ended = false;
        if (playerTransform.position.x > DavidElGnomoTransform.position.x)
            animator.Play("WalkSideRightDG");
        else
            animator.Play("WalkSideDG");

    }

    private bool ended;

    public Status UpdateMethodWalkAttackNoHat()
    {
        //move right for 2 seconds
        //coroutine that moves the object to the right for 2 seconds
        if (playerTransform.position.x > DavidElGnomoTransform.position.x)
        {
            StartCoroutine(MoveRightForTwoSeconds(DavidElGnomoTransform, speed));
            screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        else
        {
            StartCoroutine(MoveLeftForTwoSeconds(DavidElGnomoTransform, speed));
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
        GetComponent<SpriteRenderer>().color = Color.cyan;
        //StartCoroutine(PlayAnimation("PunchDG"));
        animator.Play("PunchDG");
    }
    public Status UpdateMethodPunchNoHat()
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

    #region MethodsGnomeModeNoHat
    public void StartMethodGnomeModeNoHat()
    {
        StopAllCoroutines();

        GetComponent<SpriteRenderer>().color = Color.black;
        
        animator.Play("Idle");
        
        StartCoroutine(InvokeGnomes(1f));
        StartCoroutine(InvokeGnomes(2f));
        StartCoroutine(InvokeGnomes(3f));
        StartCoroutine(WaitSeconds(5));
        
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
        animator.Play("Idle");
        //cambia el color a morado
        GetComponent<SpriteRenderer>().color = Color.yellow;
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
        return HP < HPGnomeMode && !hasBeenPlayed;
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

    #region BossController

    //ESTO EN UN FUTURO DEBE ESTAR EN UN ENEMYCONTROLLER
    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    public void RecieveDamage(float damage)
    {
        if (!invulnerable)
        {
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
        if (!invulnerable)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                player.GetComponent<KnightScript>().ReceiveAttack(damage);
                GetComponent<Knockback>().PlayFeedback(gameObject, player.GetComponent<Rigidbody2D>());
            }
        }
    }
    #endregion
}
