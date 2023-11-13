using Assets.Scripts.Model;
using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Assets.Scripts.Model.NormalItemSO;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        private Map playerInputActions;

        [SerializeField]
        private AudioClip dropClip;

        [SerializeField]
        private AudioSource audioSource;

        private KnightScript player;


        private void Awake()
        {
            playerInputActions = new Map();
            playerInputActions.Enable();
            playerInputActions.Player.Inventory.performed += ShowInventory;

        }

        private void ShowInventory(InputAction.CallbackContext context)
        {
            if (this == null) return;
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key,
                        item.Value.item.ItemImage,  //Muestra lo que hay en el inventario
                        item.Value.quantity);
                }
            }
            else
            {
                inventoryUI.Hide();
            }
        }

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            /*foreach (InventoryItem item in initialItems)     PARA EMPEZAR SIN ITEMS
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }*/
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        public List<InventoryItem> initialItems = new();

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)  //Para consumir objetos
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)  //Si la casilla est� vac�a volver
                return;
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));  //Si se pulsa el boton        
            }
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;  //Para que si se consume un item se pueda reducir la cantidad
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("Tirar", () => DropItem(inventoryItem, itemIndex, inventoryItem.quantity));
            }
            INormalItem normalItem = inventoryItem.item as INormalItem;  //Para que si se consume un item se pueda reducir la cantidad
            if (normalItem != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction("Tirar", () => DropItem(inventoryItem, itemIndex, inventoryItem.quantity));
            }
        }

        private void DropItem(InventoryItem inventoryItem, int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
            player = GameObject.FindWithTag("Player").GetComponent<KnightScript>(); //Estad�sticas PROPIO
            player.ModifyStats(-1, inventoryItem.item, quantity);
            audioSource.PlayOneShot(dropClip);
        }

        public void PerformAction(int itemIndex)  //Para mostrar el action panel
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)  //Si la casilla est� vac�a volver
                return;
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;  //Para que si se consume un item se pueda reducir la cantidad
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject);
                audioSource.PlayOneShot(itemAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Descrption);
        }

        public void EmptyInventory()
        {
            int itemIndex = 0;
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
                DropItem(inventoryItem, itemIndex, inventoryItem.quantity);
                itemIndex++;
            }

        } 
    }
}
