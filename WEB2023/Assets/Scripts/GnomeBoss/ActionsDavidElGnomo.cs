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
    private bool ended;

    //audio
    [SerializeField] private AudioSource currentTheme;
    [SerializeField] private AudioSource sleepTheme;
    [SerializeField] private AudioSource bossTheme;

    public event EventHandler OnWalkAttack;
    public event EventHandler OnWalkAttackEnd;
    public bool despierto = false;

    [SerializeField] public GameObject TiredHat;
    [SerializeField] public GameObject GnomeHat;

    [SerializeField] private float TimeTired;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject littleGnome;
    public bool invulnerable = false;
    private bool hasBeenPlayed = false;
    Vector2 walkAttackDistance = new Vector2(0, 2);
    bool collisionDetected = false;

    ScreenShake screenShake;

    GameObject music;
    #endregion

    private void Start()
    {
        music = GameObject.FindWithTag("Music");
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        DavidElGnomoTransform = GetComponent<Transform>();
    }

    #region MethodsIdle
    public void StartMethodSleep()
    {
        //si el jugador esta a 1 metro de distancia
        animator.Play("SleepGnome");

    }
    public Status UpdateMethodSleep()
    {
        if (Vector2.Distance(playerTransform.position, DavidElGnomoTransform.position) < 9f)
        {
            //stop all music
            music.GetComponent<MusicSelector>().OstPiso2.Stop();
            sleepTheme.Play();
        }
        if (Vector2.Distance(playerTransform.position, DavidElGnomoTransform.position) < 3f || GetComponent<DavidElGnomoController>().HP<500)
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
        animator.Play("WalkFrontDG");
        if (!bossTheme.isPlaying)
        {
            //music starts from volume 0 to volume 1 in 2 seconds
            StartCoroutine(MusicVolumeUp());
            sleepTheme.Stop();
            bossTheme.Play();
        }

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
        collisionDetected = false;
        OnWalkAttack?.Invoke(this, EventArgs.Empty);
        //screenShake = GetComponent<ScreenShake>();
        GetComponent<Knockback>().strength = 30f;
        ended = false;
        if (playerTransform.position.x > DavidElGnomoTransform.position.x)
            animator.Play("WalkSideRightDG");
        else
        animator.Play("WalkSideDG");

        if (playerTransform.position.x > DavidElGnomoTransform.position.x)
        {
            StartCoroutine(WalkRightUntilCollision(this.gameObject, GetComponent<DavidElGnomoController>().speed));
            //screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        else
        {
            StartCoroutine(WalkLeftUntilCollision(this.gameObject, GetComponent<DavidElGnomoController>().speed));
            //screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
    }


    public Status UpdateMethodWalkAttack()
    {
        //move right for 2 seconds
        //coroutine that moves the object to the right for 2 seconds
        
        if (collisionDetected){
            GetComponent<Knockback>().strength = 10f;
            StopAllCoroutines();
            collisionDetected = false;
            OnWalkAttackEnd?.Invoke(this, EventArgs.Empty);
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
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StopAllCoroutines();
        animator.Play("Idle");
        TiredHat.SetActive(false);
        GnomeHat.SetActive(true);
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
            GnomeHat.SetActive(false);
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
        TiredHat.SetActive(true);
        //cambia el color a morado
        StartCoroutine(WaitSeconds(TimeTired));
    }
    public Status UpdateMethodTired()
    {
        if (ended)
        {
            TiredHat.SetActive(false);
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
        return GetComponent<DavidElGnomoController>().HP < GetComponent<DavidElGnomoController>().HPSecondPhase ;
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
    IEnumerator WalkRightUntilCollision(GameObject gnomoObj, float speed)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = gnomoObj.transform.position;
        Vector3 targetPos = startingPos + Vector3.right * 20;

        while (gnomoObj.GetComponent<Collider2D>())
        {
            gnomoObj.transform.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime)*0.45f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ended = true;

    }
    IEnumerator WalkLeftUntilCollision(GameObject gnomoObj, float speed)
  {
        float elapsedTime = 0f;
        Vector3 startingPos = gnomoObj.transform.position;
        Vector3 targetPos = startingPos + Vector3.left*20;
        //mientras que no haya collisiones
        while (gnomoObj.GetComponent<Collider2D>())
        {
            gnomoObj.transform.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime)*0.45f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ended = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionDetected = true;
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

    IEnumerator MusicVolumeUp()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            bossTheme.volume = Mathf.Lerp(0, 1, (elapsedTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    #endregion


}


