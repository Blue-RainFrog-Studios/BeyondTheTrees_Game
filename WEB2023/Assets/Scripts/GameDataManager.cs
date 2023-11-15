using System.Collections.Generic;

[System.Serializable] public class ShopData
{
    public List<int> purchasedItemsIndexes = new List<int>();

}

public static class GameDataManager
{
    static ShopData shopData= new ShopData();

    static GameDataManager()
    {
        LoadShopData();
    }


    // Metodos ShopData

    public static void AddPurchasedItems(int itemIndex)
    {
        shopData.purchasedItemsIndexes.Add(itemIndex);
        SaveShopData();
    }

    public static List<int> GetAllPurchasedItems()
    {
        return shopData.purchasedItemsIndexes;
    }
    public static int GetPurchasedItem(int index)
    {
        return shopData.purchasedItemsIndexes[index];
    }
    static void SaveShopData()
    {
        BinarySerializer.Save(shopData, "item-shop-data.txt");
        UnityEngine.Debug.Log("<color=green>[itemShopData] Saved.</color>");
    }
    static void LoadShopData()
    {
        shopData = BinarySerializer.Load<ShopData> ("item-shop-data.txt");
        UnityEngine.Debug.Log("<color=green>[itemShopData] Saved.</color>");
    }
}
