using Inventory;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class KnightScript : MonoBehaviour
{

    [SerializeField]
    public Slider lifeBar;

    KnightScript knight;
    RoomController r;

    public int bleed=1;
    public int col = -1;
    private int minDmg = 3;
    public bool king = false;
    public bool bush = false;
    public int health { get; set; }
    public int totalHealth { get; set; }
    [SerializeField]
    public float speed { get; set; }
    [SerializeField]
    public int attack { get; set; }
    public float attackSpeed { get; set; }
    public int defense { get; set; }

    [SerializeField]
    private AudioSource hitSource;    
    
    [SerializeField]
    private AudioClip hitClip;

    private bool inmune = false;

    [SerializeField]
    private int maxAttack = 60;

    [SerializeField]
    private int minAttack = 5;

    [SerializeField]
    private int maxSpeed = 20;

    [SerializeField]
    private int minSpeed = 6;

    [SerializeField]
    private const float maxAttackSpeed = 0.3f;

    [SerializeField]
    private const float minAttackSpeed = 0.6f;

    public KnightScript() 
    {
        totalHealth = 50;
        health = 50;
        speed = 10;
        attack = 20;
        defense = 7;
        attackSpeed = 0.5f;
    }

    private void Awake()
    {
        r = FindObjectOfType<RoomController>();
        knight = new KnightScript();
    }

    public void ReceiveAttack(int dmgValue)
    {
        if (inmune) return;
        inmune = true;
        if (knight.health <= 0)
        {
            StartCoroutine(Wait(1));
            SceneManager.LoadScene("GameOver");
            GetComponent<PlayerMovementInputSystem>().nivel = 0;
            knight.health = 50;
            lifeBar.value = knight.health;
            this.gameObject.transform.position = new Vector2(0, -4);
            GetComponent<PlayerMovementInputSystem>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponentInChildren<Canvas>().enabled = false;
        }
        GetComponent<PlayerMovementInputSystem>().Head.Play("Hit");
        knight.health -= (dmgValue - knight.defense <= 0.0f) ? minDmg : (dmgValue - knight.defense);
        lifeBar.value = knight.health;
        hitSource.PlayOneShot(hitClip);
        StartCoroutine(WaitSeconds(1));
    }

    IEnumerator WaitSeconds(float Time)
    {
        yield return new WaitForSeconds(Time);
        GetComponent<SpriteRenderer>().color = Color.white;
        inmune = false;
    }


    private void Update()
    {
        lifeBar.value= knight.health;
    }

    public void AddHealth(int val)
    {
        if (knight.health + val <= knight.totalHealth)
        {
            knight.health += val;
            lifeBar.value = knight.health;
        }
        else
        {
            knight.health = knight.totalHealth;
            lifeBar.value = knight.totalHealth;
        }

    }
    public void MoneyDealer(float percentage, int quantity)
    {
        this.GetComponent<CoinCounter>().TotalMoneyChanger(quantity, percentage);
        ResetMoneyCanvas();
    }

    public void ResetMoneyCanvas()
    {
        this.GetComponent<CoinCounter>().expeditionMoneyTxt.gameObject.SetActive(false);
        this.GetComponent<CoinCounter>().totalMoneyTxt.gameObject.SetActive(true);
        this.GetComponent<InventoryController>().EmptyInventory();
        this.GetComponent<CoinCounter>().ResetExpeditionMoney();

    }
    public void ResetStats()
    {
        knight.health = knight.totalHealth;
        knight.speed = 10;
        knight.attack = 20;
        knight.defense = 7;
        knight.attackSpeed = 0.5f;
        knight.bleed = 1;
    }

    public void ModifyStats(int v, ItemSO inventoryItem, int quantity)
    {
        attack += v * inventoryItem.Attack;

        defense += v * inventoryItem.Defense;  //Este no hace falta revisarlo porque ya se revisa cuando se resta daño en "ReceiveAttack()"

        GetComponent<PlayerMovementInputSystem>().speed += v * inventoryItem.Speed;
        speed += v * inventoryItem.Speed;

        GetComponent<PlayerMovementInputSystem>().shoteRate -= v * inventoryItem.AttackSpeed;
        attackSpeed -= v * inventoryItem.AttackSpeed;
        
        
        GetComponent<CoinCounter>().ExpeditionMoneyChanger(v * (inventoryItem.Value * quantity));

        if (GetComponent<PlayerMovementInputSystem>().speed > maxSpeed) { 
            GetComponent<PlayerMovementInputSystem>().speed = maxSpeed;
            speed = maxSpeed;
        }

        else if (GetComponent<PlayerMovementInputSystem>().speed < minSpeed)
        {
            GetComponent<PlayerMovementInputSystem>().speed = minSpeed;
            speed = minSpeed;
        }

        if (GetComponent<PlayerMovementInputSystem>().shoteRate < maxAttackSpeed)
        {
            GetComponent<PlayerMovementInputSystem>().shoteRate = maxAttackSpeed;
            attackSpeed = maxAttackSpeed;
        }

        else if (GetComponent<PlayerMovementInputSystem>().shoteRate > minAttackSpeed)
        {
            GetComponent<PlayerMovementInputSystem>().shoteRate = minAttackSpeed;
            attackSpeed = minAttackSpeed;
        }
            
        if (attack > maxAttack)
            attack = maxAttack;

        else if (attack < minAttack)
            attack = minAttack;

    }

    private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("ColliderTop") && !collision.CompareTag("ColliderRight") && !collision.CompareTag("ColliderLeft"))
                col = 0;

            else if (collision.CompareTag("ColliderTop") && collision.CompareTag("ColliderRight"))
                col = 1;
            else if (collision.CompareTag("ColliderTop") && collision.CompareTag("ColliderLeft"))
                col = 2;
            else if (collision.CompareTag("ColliderRight") && !collision.CompareTag("ColliderTop") && !collision.CompareTag("ColliderBot"))
                col = 3;
            else if (collision.CompareTag("ColliderBot") && collision.CompareTag("ColliderRight"))
                col = 4;
            else if (collision.CompareTag("ColliderBot") && collision.CompareTag("ColliderLeft"))
                col = 5;
            else if (collision.CompareTag("ColliderBot") && !collision.CompareTag("ColliderRight") && !collision.CompareTag("ColliderLeft"))
                col = 6;
            else if (collision.CompareTag("ColliderLeft") && !collision.CompareTag("ColliderTop") && !collision.CompareTag("ColliderBot"))
                col = 7;
            else if (collision.CompareTag("ColliderLeft") && collision.CompareTag("ColliderRight") && !collision.CompareTag("ColliderTop"))
                col = 8;
            else if (collision.CompareTag("ColliderLeft") && collision.CompareTag("ColliderRight") && !collision.CompareTag("ColliderBot"))
                col = 9;
            else
                col = -1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ColliderKing"))
        {
            king = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ColliderKing"))
        {
            king = false;
        }
    }

    private IEnumerator Wait(int seconds)
    {
        GetComponent<PlayerMovementInputSystem>().Head.Play("Die");
        yield return new WaitForSeconds(seconds);
    }
}

