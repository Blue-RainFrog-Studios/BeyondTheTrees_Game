using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Material enemyMaterial;

    public float life;
    public bool notInRoom = false;
    public float range;
    public float attackRange;
    protected bool healed = false;
    public int damage;
    protected bool coolDownAttack = false;
    public float coolDown;
    
    protected GameObject room;

    public float blinkDuration;
    public int blinkNumber;
    protected bool blinking = false;
    static public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        room = GameObject.FindGameObjectWithTag("RoomController");
        enemyMaterial = GetComponent<Renderer>().material;
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
        if (!blinking)
        {
            StartCoroutine(Blink());
        }
        if (life <= 0) {
            if (player.GetComponent<KnightScript>().king)
            {
                player.GetComponent<CoinCounter>().ExpeditionMoneyChanger(2);
            }
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
    public IEnumerator Blink()
    {
        blinkDuration = 0.05f; ;
        blinkNumber = 3;
        blinking = true;

        if(enemyMaterial != null) 
        {
            // Almacenar el color original del material
            Color colorOriginal = enemyMaterial.color;

            // Cambiar el color a rojo durante el parpadeo
            for (int i = 0; i < blinkNumber; i++)
            {
                enemyMaterial.color = Color.red;
                yield return new WaitForSeconds(blinkDuration);

                enemyMaterial.color = colorOriginal;
                yield return new WaitForSeconds(blinkDuration);
            }

            blinking = false;
        }
    }
       
}
