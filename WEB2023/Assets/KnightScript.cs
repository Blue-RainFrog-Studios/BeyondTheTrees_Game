using Inventory;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KnightScript : MonoBehaviour
{

    [SerializeField]
    public Slider lifeBar;

    KnightScript knight;
    RoomController r;

    public int col = -1;
    public bool king = false;
    public bool bush = false;
    public int health { get; set; }
    public int totalHealth { get; set; }
    public float speed { get; set; }

    public int attack { get; set; }
    public int attackSpeed { get; set; }
    public int defense { get; set; }

    public KnightScript() 
    {
        totalHealth = 50;
        health = 11111150;
        speed = 6;
        attack = 100;
        defense = 7;
        attackSpeed = 3;
    }

    private void Awake()
    {
        r = FindObjectOfType<RoomController>();
        knight = new KnightScript();
    }

    public void ReceiveAttack(int dmgValue)
    {

        knight.health -= (dmgValue - knight.defense);
        lifeBar.value = knight.health;
        if (knight.health <= 0)
        {
            //r.DestroyRooms();
            SceneManager.LoadScene("GameOver");
            GetComponent<PlayerMovementInputSystem>().nivel =0;
            knight.health = 50;
            lifeBar.value = knight.health;
            this.gameObject.transform.position = new Vector2(0, -4);
            GetComponent<PlayerMovementInputSystem>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponentInChildren<Canvas>().enabled = false;
        }
    }
    private void Update()
    {
        //Debug.Log(king);
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

    public void ModifyStats(int v, ItemSO inventoryItem, int quantity)
    {
        attack += v * inventoryItem.Attack;
        defense += v * inventoryItem.Defense;
        GetComponent<PlayerMovementInputSystem>().speed += v * inventoryItem.Speed;
        GetComponent<PlayerMovementInputSystem>().shoteRate -= v * inventoryItem.AttackSpeed;
        GetComponent<CoinCounter>().ExpeditionMoneyChanger(v * (inventoryItem.Value * quantity));
    }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("ColliderTop") && !collision.CompareTag("ColliderRight") && !collision.CompareTag("ColliderLeft"))
            {
                col = 0;

            }
            else if (collision.CompareTag("ColliderTop") && collision.CompareTag("ColliderRight"))
            {
                col = 1;
            }
            else if (collision.CompareTag("ColliderTop") && collision.CompareTag("ColliderLeft"))
            {
                col = 2;
            }
            else if (collision.CompareTag("ColliderRight") && !collision.CompareTag("ColliderTop") && !collision.CompareTag("ColliderBot"))
            {
                col = 3;
            }
            else if (collision.CompareTag("ColliderBot") && collision.CompareTag("ColliderRight"))
            {
                col = 4;
            }
            else if (collision.CompareTag("ColliderBot") && collision.CompareTag("ColliderLeft"))
            {
                col = 5;
            }
            else if (collision.CompareTag("ColliderBot") && !collision.CompareTag("ColliderRight") && !collision.CompareTag("ColliderLeft"))
            {
                col = 6;
            }
            else if (collision.CompareTag("ColliderLeft") && !collision.CompareTag("ColliderTop") && !collision.CompareTag("ColliderBot"))
            {
                col = 7;
            }
            else if (collision.CompareTag("ColliderLeft") && collision.CompareTag("ColliderRight") && !collision.CompareTag("ColliderTop"))
            {
                col = 8;
            }
            else if (collision.CompareTag("ColliderLeft") && collision.CompareTag("ColliderRight") && !collision.CompareTag("ColliderBot"))
            {
                col = 9;
            }
            else
            {
                col = -1;
            }

       

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

}

