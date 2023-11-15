using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = .5f;
    float itemHeight;

    [Space(20)]

    [Header("UI elements")]
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
    [SerializeField] GameObject dialogUI;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        AddShopEvents();
        GenerateShopItems();
        player = GameObject.FindGameObjectWithTag("Player");
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
        // Puede que haya que cambiar de donde pilla los datos
        // Escribe los items que se han compreado como bloqueados
        for(int i = 0; i < itemDB.ItemCount; i++)
        {
            Shop_Item checkPucrh = itemDB.GetItemToPool(i);
            if (checkPucrh.isPurchased)
            {

            }
        }

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
            }
            else
            {
                ui_item.SetItemPrice(shop_Item.price);
                ui_item.OnItemPurchased(i, OnItemPurchased);
            }

            //ShopItemsContainer.GetComponent<RectTransform>().sizeDelta=
            //    Vector2.up *(itemHeight+itemSpacing)* itemDB.ItemCount;
            //Hay que expandir el bottom de content de objetos 86,66 unidades por cada objeto que se �ada a la tienda
        }

    }
    Item_UI GetItem_UI(int index)
    {
        return ShopItemsContainer.GetChild(index).GetComponent<Item_UI>();
    }
    void CloseShop()
    {
        shopUI.SetActive(false);
        dialogUI.SetActive(true);
        /*player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<KnightScript>().lifeBar.gameObject.SetActive(true);  //Mostrar barra de vida*/
    }

    void OpenShop()
    {
        shopUI.SetActive(true);
        /*player = GameObject.FindGameObjectWithTag("Player");
        moneyText.text = player.GetComponent<CoinCounter>().totalMoney.ToString();
        player.GetComponentInChildren<KnightScript>().lifeBar.gameObject.SetActive(false);  //Mostrar barra de vida*/
    }
    void OnItemSelected(int index)
    {
        Debug.Log("Select" + index);
    }
    void OnItemPurchased(int index)
    {
        Shop_Item shop_Item = itemDB.GetItemToPool(index);
        Item_UI ui_item = GetItem_UI(index);

        if (player.GetComponent<CoinCounter>().totalMoney >= itemDB.GetItemToPool(index).price)
        {
            // Compra el objeto y resta el dinero
            Debug.Log("Purchased" + index);
            player.GetComponent<CoinCounter>().totalMoney -= itemDB.GetItemToPool(index).price;
            player.GetComponent<CoinCounter>().UpdateTotalMoneyText();
            itemDB.PurchaseItem(index);

            ui_item.SetItemAsPurchased();
            
        }
        else
        {
            Debug.Log("No tienes dinero suficiente");
        }
    }
}
