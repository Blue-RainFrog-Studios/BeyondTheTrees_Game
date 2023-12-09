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
    static public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
                case (EnemyType.Ranged):
                    break;
                case (EnemyType.Teleport):
                    GetComponent<EnemyController>().audioSource.PlayOneShot(GetComponent<EnemyController>().teleportHit);
                    break;
            }
        }

        life -= damage;
        this.GetComponent<Knockback>().PlayFeedback(player, this.gameObject.GetComponent<Rigidbody2D>());
        if (life <= 0) { 
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
