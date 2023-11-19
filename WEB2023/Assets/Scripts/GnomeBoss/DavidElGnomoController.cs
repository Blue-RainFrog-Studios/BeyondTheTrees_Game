using BehaviourAPI.UnityToolkit.Demos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidElGnomoController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public int damage; 
    [SerializeField] public float speedNoHat;
    [SerializeField] public int damageNoHat;

    [SerializeField] public float HP;
    [SerializeField] public float HPGnomeMode;
    [SerializeField] public float HPSecondPhase;
    GameObject player;
    private bool Played;
    private bool PlayedSF;

    #region BossController

    private void Start()
    {
        Played = false;
        PlayedSF = false;
        player = GameObject.FindGameObjectWithTag("Player");
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
    }
    //ESTO EN UN FUTURO DEBE ESTAR EN UN ENEMYCONTROLLER
    void DieGO()
    {
        StartCoroutine(animAndDie());
            Destroy(gameObject);
    }
    IEnumerator animAndDie()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("DieGnome");
        yield return new WaitForSeconds(1f);
    }
    // Update is called once per frame
    public void RecieveDamage(float damage)
    {

        if (!GetComponent<ActionsDavidElGnomo>().invulnerable)
        {
            HP -= damage;
            Debug.Log("Recibo daño");
            if (HP <= 0)
            {
                DieGO();
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
}
