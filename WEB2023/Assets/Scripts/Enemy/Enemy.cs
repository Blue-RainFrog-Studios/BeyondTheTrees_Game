using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float life;
    public bool notInRoom = false;
    public float range;
    public float attackRange;
    protected bool healed = false;
    public int damage;
    protected bool coolDownAttack = false;
    public float coolDown;
    GameObject player;
    protected GameObject room;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        room = GameObject.FindGameObjectWithTag("RoomController");
    }

    virtual public void RecieveDamage(float damage)
    {
        if (GetComponent<EnemyController>() != null)
        {
            switch (GetComponent<EnemyController>().enemyType)
            {
                case (EnemyType.Melee):
                    GetComponent<EnemyController>().audioSource.PlayOneShot(GetComponent<EnemyController>().ghostClip);
                    break;
                case (EnemyType.Teleport):
                    GetComponent<EnemyController>().audioSource.PlayOneShot(GetComponent<EnemyController>().teleportHit);
                    break;
            }
        }

        life -= damage;
        this.GetComponent<Knockback>().PlayFeedback(player, this.gameObject.GetComponent<Rigidbody2D>());
        if (life <= 0) {
            if (GetComponent<EnemyController>() != null)
            {
                switch (GetComponent<EnemyController>().enemyType)
                {
                    
                    case (EnemyType.Ranged):
                        room.GetComponent<RoomController>().healing = false;
                        break;
                }
            }
            RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());

            Destroy(this.gameObject);
        }
    }
    public IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }
}
