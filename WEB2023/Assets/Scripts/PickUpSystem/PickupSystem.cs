using Inventory.Model;
using System;
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
            this.GetComponent<CanvasController>().AssignItem(item.InventoryItem.ItemImage, item.InventoryItem.name, item.InventoryItem.Descrption);
            this.GetComponent<CanvasController>().pickupPanel.SetActive(true);
            
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)  //Coje todos los items
            {
                item.DestroyItem();
                IncrementStats(item, item.Quantity);
            }
            else if(item.Quantity == reminder)
            {

            }
            else    //Deja en el suelo "reminder" unidades de items
            {
                IncrementStats(item, item.Quantity);
                item.Quantity = reminder;
            }
        }
    }

    private void IncrementStats(Item item, int quantity)
    {
        player = GameObject.FindWithTag("Player").GetComponent<KnightScript>(); //Estadísticas PROPIO
        player.ModifyStats(1, item.InventoryItem, quantity);
    }
}
