using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionsDavidElGnomoNoHat : MonoBehaviour
{
    #region Variables
    private GameObject player;
    private Transform playerTransform;
    private Transform DavidElGnomoTransform;
    bool collisionDetected = false;

    //[SerializeField] private float speed;
    //[SerializeField] private int damage;
    //[SerializeField] private float HP;
    //[SerializeField] private float HPGnomeMode;

    [SerializeField] private float TimeTired;
    public event EventHandler OnWalkAttack;
    public event EventHandler OnWalkAttackEnd;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject littleGnome;

    [SerializeField] private GameObject TiredNoHat;
    [SerializeField] private GameObject GnomeNoHat;


    //audio
    [SerializeField] private AudioSource dieSound;

    public bool invulnerable = false;
    private bool hasBeenPlayed = false;

    Vector2 walkAttackDistance = new Vector2(0, 2);
    ScreenShake screenShake;
    private bool animationPlayed = false;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        DavidElGnomoTransform = GetComponent<Transform>();
    }

    public void StartMethodNoHat()
    {
        GetComponent<ActionsDavidElGnomo>().TiredHat.SetActive(false);
        GetComponent<ActionsDavidElGnomo>().GnomeHat.SetActive(false);
        OnWalkAttack?.Invoke(this, EventArgs.Empty);
        screenShake = GetComponent<ScreenShake>();
        StopAllCoroutines();
        GetComponent<ActionsDavidElGnomo>().StopAllCoroutines();
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.Play("HatFall");

    }
    public Status UpdateMethodNoHat()
    {
        StartCoroutine(WaitSeconds(5));
        //screenShake.StartCoroutine(screenShake.ShakeScreen());
        if (!ended) { 
            return Status.Running;
        }
        else
        {
            ended = false;
            OnWalkAttackEnd?.Invoke(this, EventArgs.Empty);
            return Status.Success;
        }
    }


    #region MethodsWalkNoHat
    public void StartMethodWalkNoHat()
    {
        Debug.Log("ANDANDO AL JUGADOR");
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
        collisionDetected = false;
        GetComponent<Knockback>().strength = 40f;
        ended = false;
        OnWalkAttack?.Invoke(this, EventArgs.Empty);

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
            StartCoroutine(WalkRightUntilCollision(this.gameObject, GetComponent<DavidElGnomoController>().speedNoHat));
            //screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        else
        {
            StartCoroutine(WalkLeftUntilCollision(this.gameObject, GetComponent<DavidElGnomoController>().speedNoHat));
            //screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        if (collisionDetected)
        {
            GetComponent<Knockback>().strength = 10f;
            StopAllCoroutines();
            collisionDetected = false;
            OnWalkAttackEnd?.Invoke(this, EventArgs.Empty);
            return Status.Success;
        }
        return Status.Running;
    }
    #endregion

    #region MethodsPunchNoHat


    public void StartMethodPunchNoHat()
    {
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
        GetComponent<ActionsDavidElGnomo>().StopAllCoroutines();
        invulnerable = true;
        animator.Play("IdleNoHat");
        TiredNoHat.SetActive(false);
        GetComponent<DavidElGnomoController>().ouchNoHatFace.SetActive(false);
        GnomeNoHat.SetActive(true);
        StartCoroutine(InvokeGnomes(1f));
        StartCoroutine(InvokeGnomes(2f));
        StartCoroutine(InvokeGnomes(3f)); 
        StartCoroutine(InvokeGnomes(1f));
        StartCoroutine(InvokeGnomes(2f));
        StartCoroutine(InvokeGnomes(3f));
        StartCoroutine(WaitSeconds(6));
        
    }
    public Status UpdateMethodGnomeModeNoHat()
    {
        if (ended)
        {
            GnomeNoHat.SetActive(false);
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
        TiredNoHat.SetActive(true);
        animator.Play("IdleNoHat");
        StartCoroutine(WaitSeconds(TimeTired));
    }
    public Status UpdateMethodTiredNoHat()
    {
        if (ended)
        {
            TiredNoHat.SetActive(false);
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }
    #endregion

    public void StartMethodDieGnome()
    {
        GnomeNoHat.SetActive(false);
        TiredNoHat.SetActive(false);
        ended = false; 
        StopAllCoroutines();
        StartCoroutine(animAndDie());
        dieSound.Play();

    }
    public Status UpdateMethodDieGnome()
    {
        if (ended) { 
        Destroy(gameObject);
        player.GetComponent<PlayerMovementInputSystem>().nivel++;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoandingBoss");
        return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }

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
        return GetComponent<DavidElGnomoController>().HP < GetComponent<DavidElGnomoController>().HPGnomeMode-200 && !hasBeenPlayed;
    }
    #endregion

    #region Coroutines
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

    IEnumerator WaitSeconds(float Time)
    {
        yield return new WaitForSeconds(Time);
        ended = true;
    }
    IEnumerator animAndDie()
    {
        animator.Play("DieGnome");
        yield return new WaitForSeconds(2f);
        ended=true;
    }

    IEnumerator InvokeGnomes(float timeSpawn)
    {
        yield return new WaitForSeconds(timeSpawn); ;
        Instantiate(littleGnome, new Vector3(new System.Random().Next((int)DavidElGnomoTransform.position.x - 3, (int)DavidElGnomoTransform.position.x + 3), new System.Random().Next((int)DavidElGnomoTransform.position.y - 3, (int)DavidElGnomoTransform.position.y + 3), 0), Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player") && !collision.collider.CompareTag("Attack") && !collision.collider.CompareTag("Root") && !collision.collider.CompareTag("LittleGnome") && !collision.collider.CompareTag("LittleGnome"))
        collisionDetected = true;
    }
    #endregion

}
