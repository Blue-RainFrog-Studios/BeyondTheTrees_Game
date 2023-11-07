using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    private KnightScript player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)
                item.DestroyItem();
            else
                item.Quantity = reminder;

            player = GameObject.FindWithTag("Player").GetComponent<KnightScript>(); //Estadísticas PROPIO
            player.attack += item.InventoryItem.Attack;
            player.defense += item.InventoryItem.Defense;
            player.GetComponent<PlayerMovementInputSystem>().speed += item.InventoryItem.Speed;
        }
    }
}
