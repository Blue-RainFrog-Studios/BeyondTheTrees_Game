using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : Enemy
{
    [SerializeField] public float speed;
    [SerializeField] public float speedJump;
    //[SerializeField] public int damage;
    public int initialHP;
    [SerializeField] public float HP;
    //GameObject player;
    private bool Played;
    private bool PlayedSF;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        HP = initialHP;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            if (!coolDownAttack)
            {
                player.GetComponent<KnightScript>().ReceiveAttack(damage);
                StartCoroutine(CoolDown());
            }

        }
    }
}
