using Inventory;
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

    public int health { get; set; }
    public int totalHealth { get; set; }
    public float speed { get; set; }

    public int attack { get; set; }
    public int defense { get; set; }

    public KnightScript() {
            totalHealth = 50;
            health = 50;
            speed = 6;
            attack = 15;
            defense = 7;
        }
        
    

    private void Awake()
    {
        r = FindObjectOfType<RoomController>();
        knight = new();
    }

    public void ReceiveAttack(int dmgValue)
    {

        knight.health -= (dmgValue - knight.defense);
        lifeBar.value = knight.health;
        if (knight.health <= 0)
        {
            //r.DestroyRooms();
            SceneManager.LoadScene("GameOver");
            knight.health = 50;
            lifeBar.value = knight.health;
            this.gameObject.transform.position = new Vector2(0,-4);
            //this.GetComponentInParent<GameObject>().SetActive(false);
            GetComponent<PlayerMovementInputSystem>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //GameObject.Find("CanvasInv").gameObject.transform.Find("Menu").gameObject.SetActive(false);
            GetComponentInChildren<Canvas>().enabled = false;
        }
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
        public void MoneyDealer()
        {
            this.GetComponent<CoinCounter>().TotalMoneyChanger(this.GetComponent<CoinCounter>().expeditionMoney);
            this.GetComponent<CoinCounter>().expeditionMoneyTxt.gameObject.SetActive(false);
            this.GetComponent<CoinCounter>().totalMoneyTxt.gameObject.SetActive(true);
            this.GetComponent<InventoryController>().EmptyInventory();
            this.GetComponent<CoinCounter>().ResetExpeditionMoney();
        }

}
