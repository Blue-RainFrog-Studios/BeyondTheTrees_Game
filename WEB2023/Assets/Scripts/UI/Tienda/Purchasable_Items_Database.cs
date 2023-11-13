using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ItemShopDatabase", menuName ="Shopping/shop database")]
public class Purchasable_Items_Database : ScriptableObject
{
    public Shop_Item[] items;

    public int ItemCount
    {
        get { return items.Length; }
    }
    public Shop_Item GetItemToPool(int index)
    {
        return items[index];
    }
    public void PurchaseItem(int index)
    {
        items[index].isPurchased= true; 
    }
}
