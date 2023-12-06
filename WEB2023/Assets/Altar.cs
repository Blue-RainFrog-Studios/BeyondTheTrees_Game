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

    [SerializeField]
    private AudioClip endSound;

    [SerializeField]
    private AudioClip placeSound;

    [SerializeField]
    private AudioSource audioSource;


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

                state += quantity;

                if (state == 3)
                {
                    audioSource.PlayOneShot(endSound);
                    collision.GetComponent<KnightScript>().AddHealth((int)(collision.GetComponent<KnightScript>().health * 0.5f));
                    GetComponent<Animator>().SetTrigger("Fase 3-4");
                }
                else
                {
                    audioSource.PlayOneShot(placeSound);
                    if(state == 1)
                        GetComponent<Animator>().SetTrigger("Fase 1-2");
                    else if(state == 2)
                        GetComponent<Animator>().SetTrigger("Fase 2-3");
                }

            }
        }

    }
}
