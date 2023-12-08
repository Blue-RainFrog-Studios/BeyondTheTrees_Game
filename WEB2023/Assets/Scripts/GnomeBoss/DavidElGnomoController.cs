using BehaviourAPI.UnityToolkit.Demos;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DavidElGnomoController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public int damage; 
    [SerializeField] public float speedNoHat;
    [SerializeField] public int damageNoHat;

    [SerializeField] public float HP;
    [SerializeField] public float HPGnomeMode;
    [SerializeField] public float HPSecondPhase;
    [SerializeField] private AudioSource grunt1;
    [SerializeField] private AudioSource grunt2;
    [SerializeField] private AudioSource metalicS; 
    [SerializeField] private AudioSource metalicS2;

    [SerializeField] GameObject ouchFace;
    [SerializeField] GameObject ouchNoHatFace;

    GameObject player;
    private bool Played;
    private bool PlayedSF;
    private bool PlayedSF2;
    private bool walkAtt;


    #region BossController

    private void Start()
    {
        Played = false;
        PlayedSF = false;
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<ActionsDavidElGnomo>().OnWalkAttack += WalkAttackDoing;
        GetComponent<ActionsDavidElGnomo>().OnWalkAttackEnd += WalkAttackEnd; 
        
        GetComponent<ActionsDavidElGnomoNoHat>().OnWalkAttack += WalkAttackDoing;
        GetComponent<ActionsDavidElGnomoNoHat>().OnWalkAttackEnd += WalkAttackEnd;
    }

    private void Update()
    {
        if (HP < HPGnomeMode && !Played)
        {
            GetComponent<FMSDavidElGnomo>().ChangeToGnomeMode();
            Played = true;
        }
        if (HP < HPSecondPhase && !PlayedSF)
        {
            GetComponent<FMSDavidElGnomo>().ChangeToNoHat();
            PlayedSF = true;
        }
        if (HP< HPSecondPhase - 200 && !PlayedSF2) {
            GetComponent<FMSDavidElGnomo>().ChangeToGnomeModeNoHat();
            PlayedSF2 = true;
        }
        if (walkAtt)
        {
            ouchFace.SetActive(false);
            ouchNoHatFace.SetActive(false);
        }
        
    }
    //ESTO EN UN FUTURO DEBE ESTAR EN UN ENEMYCONTROLLER
    //void DieGO()
    //{
    //    StartCoroutine(animAndDie());
    //    Destroy(gameObject);
    //    player.GetComponent<PlayerMovementInputSystem>().nivel++;
    //    SceneManager.LoadScene("LoandingBoss");

    //}
    //IEnumerator animAndDie()
    //{
    //    Animator animator = GetComponent<Animator>();
    //    animator.Play("DieGnome");
    //    yield return new WaitForSeconds(1f);
    //}
    // Update is called once per frame
    public void RecieveDamage(float damage)
    {

        if (!GetComponent<ActionsDavidElGnomo>().invulnerable)
        {
            //random between 1 and 2
            int random = UnityEngine.Random.Range(1, 3);
            if (random == 1 && HP>=0 && !walkAtt)
            {
                grunt1.Play();
            }
            else if (random == 2 && HP >= 0 && !walkAtt)
            {
                grunt2.Play();
            }
            HP -= damage;

            if(HP>HPSecondPhase && !walkAtt)
            {
                ouchFace.SetActive(true);
                StartCoroutine(wait1second());
            }
            else if (HP < HPSecondPhase && !walkAtt && HP >= 0)
            {
                ouchNoHatFace.SetActive(true);
                StartCoroutine(wait1second());
            }

            Debug.Log("Recibo daño");
            if (HP <= 0)
            {
                GetComponent<FMSDavidElGnomo>().DieGnome();
            }
        }
        else
        {
            int random = UnityEngine.Random.Range(1, 3);
            if (random == 1 && HP >= 0 && !walkAtt)
            {
                metalicS.Play();
            }
            else if (random == 2 && HP >= 0 && !walkAtt)
            {
                metalicS2.Play();
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GetComponent<ActionsDavidElGnomo>().invulnerable)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (HP > HPSecondPhase)
                {
                    player.GetComponent<KnightScript>().ReceiveAttack(damage);
                    player.GetComponent<Knockback>().PlayFeedback(gameObject, player.GetComponent<Rigidbody2D>());
                }
                else
                {
                    player.GetComponent<KnightScript>().ReceiveAttack(damageNoHat);
                    player.GetComponent<Knockback>().PlayFeedback(gameObject, player.GetComponent<Rigidbody2D>());
                }

            }
        }
    }
    #endregion

    public void WalkAttackDoing(object sender, EventArgs e)
    {
        walkAtt = true;
    }
    public void WalkAttackEnd(object sender, EventArgs e)
    {
        walkAtt = false;
    }

    IEnumerator wait1second()
    {
        //wait 1 second 
        yield return new WaitForSeconds(1f);
        ouchFace.SetActive(false);
        ouchNoHatFace.SetActive(false);
    }
}
