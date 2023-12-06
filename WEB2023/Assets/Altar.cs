using Inventory;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private InventorySO inventory;
    int quantity;
    private int state;

    private void Awake()
    {
        state = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        inventory = collision.GetComponent<InventoryController>().inventoryData;
        for (int i = 0; i < inventory.Size; i++)
        {
            if (inventory.GetItemAt(i).item != null && inventory.GetItemAt(i).item.Name == "Bellota")
            {
                quantity = inventory.GetItemAt(i).quantity;
                inventory.RemoveItem(i, inventory.GetItemAt(i).quantity);
                if (quantity == 1)
                {
                    state += 1; 
                    //Poner estado 2 de la animación del altar
                }

                else if (quantity == 2)
                {
                    state += 2; 
                    //Poner estado 3 de la animación del altar
                }

                else
                {
                    state += 3;
                    //Poner estado 4 de la animación del altar
                }

                if(state == 3)
                    collision.GetComponent<KnightScript>().AddHealth((int)(collision.GetComponent<KnightScript>().health * 0.5f));

            }
        }

    }
}
