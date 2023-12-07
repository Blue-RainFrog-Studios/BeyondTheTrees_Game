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
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void RecieveDamage(float damage)
    {
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
