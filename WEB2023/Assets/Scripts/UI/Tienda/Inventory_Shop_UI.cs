using Inventory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Inventory_Shop_UI : MonoBehaviour
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

    //[SerializeField] Purchasable_Items_Database itemDB;
    [SerializeField] All_Items_Database itemDB;
    [SerializeField] GameObject tutorialData;
    [Space(20)]

    [Header("Shop Event")]
    [SerializeField] GameObject shopUI;
    [SerializeField] Button openShopButton;
    [SerializeField] Button closeShopButton;
    [SerializeField] GameObject dialogUI;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buyClip;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    GameObject player;

    void Start()
    {
        Debug.Log("Abre tienda");
        itemDB = DataManager_Items_Database.Instance.myItemsData;
        AddShopEvents();
        GenerateShopItems();
        player = GameObject.FindGameObjectWithTag("Player");

    }
    void Update()
    {

    }
    void AddShopEvents()
    {
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);

        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);
        if (tutorialData.GetComponent<NPCHelperManager>().tutorialInventario)
        {
            closeShopButton.gameObject.SetActive(true);
        }
        else
        {
            closeShopButton.gameObject.SetActive(false);

        }
    }

    void GenerateShopItems()
    {

        Debug.Log("GENERANDO Mejoras inventario");
        // Puede que haya que cambiar de donde pilla los datos
        // Escribe los items que se han compreado como bloqueados
        /*for(int i = 0; i < GameDataManager.GetAllPurchasedItems().Count; i++)
        {
            int purchasedItemsIndex = GameDataManager.GetPurchasedItem(i);
            itemDB.PurchaseItem(purchasedItemsIndex);
        }*/

        // DeleteItem Template After generating items
        itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemsContainer.GetChild(0).gameObject);
        ShopItemsContainer.DetachChildren();

        // GenerateItems
        // for (int i = 0; i < itemDB.ItemCount; i++)
        for (int i = 0; i < itemDB.Inventory_Shop_Items_Count; i++)
        {
            //Shop_Item shop_Item = itemDB.GetItemToPool(i);
            UpgradeSO shop_Item = itemDB.InventoryShopGetItemFromDB(i);
            Item_UI ui_item = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<Item_UI>();

            // Move item to position
            ui_item.SetItemPosittion(Vector2.down * i * (itemHeight + itemSpacing));

            // AddInformation to UI
            //ui_item.SetItemName(shop_Item.name);
            ui_item.SetItemName(shop_Item.Name);
            Debug.Log(shop_Item.Name);
            //ui_item.SetItemImage(shop_Item.image);
            ui_item.SetItemImage(shop_Item.UpgradeImage);
            //ui_item.SetItemDescription(shop_Item.description);
            ui_item.SetItemDescription(shop_Item.Descrption);
            Debug.Log(shop_Item.Descrption);
            //ui_item.SetItemPrice(shop_Item.price);
            ui_item.SetItemPrice(shop_Item.ShopValue);
            Debug.Log(shop_Item.ShopValue);

            if (shop_Item.IsPurchased)
            {
                ui_item.SetItemAsPurchased();
            }
            else
            {
                ui_item.SetItemPrice(shop_Item.ShopValue);
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
        player.GetComponent<PlayerMovementInputSystem>().enabled = true;
        //dialogUI.SetActive(true);
        /*player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<KnightScript>().lifeBar.gameObject.SetActive(true);  //Mostrar barra de vida*/
    }

    void OpenShop()
    {
        shopUI.SetActive(true);
        player.GetComponent<PlayerMovementInputSystem>().enabled = false;
        /*player = GameObject.FindGameObjectWithTag("Player");
        moneyText.text = player.GetComponent<CoinCounter>().totalMoney.ToString();
        player.GetComponentInChildren<KnightScript>().lifeBar.gameObject.SetActive(false);  //Mostrar barra de vida*/
    }
    void OnItemSelected(int index)
    {
        Debug.Log("Select" + index);
    }
    void OnItemPurchased(int index)


        // Suma 1 a la capacidad de inventario
    {
        UpgradeSO shop_Item = itemDB.InventoryShopGetItemFromDB(index);
        Item_UI ui_item = GetItem_UI(index);

        if (player.GetComponent<CoinCounter>().totalMoney >= itemDB.InventoryShopGetItemFromDB(index).ShopValue)
        {
            // Compra el objeto y resta el dinero
            Debug.Log("Purchased" + index);
            player.GetComponent<CoinCounter>().totalMoney -= itemDB.InventoryShopGetItemFromDB(index).ShopValue;
            player.GetComponent<CoinCounter>().UpdateTotalMoneyText();

            ui_item.SetItemAsPurchased();
            //GameDataManager.AddPurchasedItems(index);

            audioSource.PlayOneShot(buyClip);
            //GameDataManager.AddPurchasedItems(index);


            // Se completa el tutorial al comprar el primer item
            if (!tutorialData.GetComponent<NPCHelperManager>().tutorialInventario)
            {
                tutorialData.GetComponent<NPCHelperManager>().tutorialInventario = true;
                closeShopButton.gameObject.SetActive(true);
            }
            player.GetComponent<InventoryController>().InventorySize();

        }
        else
        {
            Debug.Log("No tienes dinero suficiente");
        }
    }
}
