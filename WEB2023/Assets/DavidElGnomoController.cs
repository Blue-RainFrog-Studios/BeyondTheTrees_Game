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
    [SerializeField] public GameObject player;
    private bool Played;
    private bool PlayedSF;

    #region BossController

    private void Start()
    {
        Played = false;
        PlayedSF = false;
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
        Destroy(gameObject);
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
                    GetComponent<Knockback>().PlayFeedback(gameObject, player.GetComponent<Rigidbody2D>());
                }
                else
                {
                    player.GetComponent<KnightScript>().ReceiveAttack(damageNoHat);
                    GetComponent<Knockback>().PlayFeedback(gameObject, player.GetComponent<Rigidbody2D>());
                }

            }
        }
    }
    #endregion
}
