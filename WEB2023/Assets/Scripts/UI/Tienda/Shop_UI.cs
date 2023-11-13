using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = .5f;
    float itemHeight;

    [Space(20)]

    [Header("UI elements")]
    [SerializeField] GameObject ShopPanel;
    [SerializeField] Transform ShopMenu;
    [SerializeField] Transform ShopItemsContainer;
    [SerializeField] GameObject itemPrefab;

    [Space(20)]

    [SerializeField] Purchasable_Items_Database itemDB;
    [Space(20)]

    [Header("Shop Event")]
    [SerializeField] GameObject shopUI;
    [SerializeField] Button openShopButton;
    [SerializeField] Button closeShopButton;



    // Start is called before the first frame update
    void Start()
    {
        AddShopEvents();
        GenerateShopItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AddShopEvents()
    {
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);

        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);
    }
    void GenerateShopItems()
    {
        // DeleteItem Template After generating items
        itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemsContainer.GetChild(0).gameObject);
        ShopItemsContainer.DetachChildren();

        // GenerateItems
        for(int i = 0; i < itemDB.ItemCount; i++)
        {
            Shop_Item shop_Item = itemDB.GetItemToPool(i);
            Item_UI ui_item = Instantiate (itemPrefab, ShopItemsContainer).GetComponent<Item_UI>();

            // Move item to position
            ui_item.SetItemPosittion(Vector2.down * i * (itemHeight + itemSpacing));

            // AddInformation to UI
            ui_item.SetItemName(shop_Item.name);
            ui_item.SetItemImage(shop_Item.image);
            ui_item.SetItemDescription(shop_Item.description);
            ui_item.SetItemPrice(shop_Item.price);
            
            if(shop_Item.isPurchased)
            {
                ui_item.SetItemAsPurchased();
                ui_item.OnItemSelect ();
            }
            else
            {

            }

        }

    }
    void CloseShop()
    {
        shopUI.SetActive(false);
    }

    void OpenShop()
    {
        shopUI.SetActive(true);
    }
    void OnItemSelect(int index)
    {
        Debug.Log("Select " + index);
    }
}
