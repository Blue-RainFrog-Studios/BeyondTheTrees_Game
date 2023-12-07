using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float life;
    public bool notInRoom = false;
    public float range;
    public float attackRange;
    protected bool healed = false;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void RecieveDamage(float damage)
    {
        Debug.Log("OKAY, LETS GO");
        life -= damage;
        this.GetComponent<Knockback>().PlayFeedback(player, this.gameObject.GetComponent<Rigidbody2D>());
        if (life <= 0) { 
            RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
            Destroy(this.gameObject);
        }
    }
}
