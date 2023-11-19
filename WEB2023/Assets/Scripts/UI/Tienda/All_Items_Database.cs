using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="All_Items_Database")]

public class All_Items_Database : ScriptableObject
{
    public List<ItemSO> All_items;
    public List<ItemSO> Shop_Items;
    public List<ItemSO> Run_Items;
    public List<ItemSO> Run_Common_Items;
    public List<ItemSO> Run_Rare_Items;
    public List<ItemSO> Run_Epic_Items;
    public List<ItemSO> Run_Legendary_Items;


    public int All_Items_Count
    {
        get { return All_items.Count; }

    }

    public int Shop_Items_Count
    {
        get { return Shop_Items.Count; }

    }
    public int Run_Items_Count
    {
        get { return Run_Items.Count; }

    }

    public void PurchaseItem(int index)
    {
        Shop_Items[index].IsPurchased = true;
        Run_Items.Add(Shop_Items[index]);
        int itemRarity = Shop_Items[index].Rarity;
        switch (itemRarity)
        {
            case 0:
                Run_Common_Items.Add(Shop_Items[index]);
                break;
            case 1:
                Run_Rare_Items.Add(Shop_Items[index]);
                break;
            case 2:
                Run_Epic_Items.Add(Shop_Items[index]);
                break;
            case 3:
                Run_Legendary_Items.Add(Shop_Items[index]);
                break;
        }
    }

    public ItemSO ShopGetItemFromPool(int index)
    {
        return Shop_Items[index];
    }
    public ItemSO RunGetItemFromPool(int index)
    {
        return Run_Items[index];
    }
    public ItemSO RunGetCommonItemFromPool(int index)
    {
        return Run_Common_Items[index];
    }
    public ItemSO RunGetRareItemFromPool(int index)
    {
        return Run_Rare_Items[index];
    }
    public ItemSO RunGetEpicItemFromPool(int index)
    {
        return Run_Epic_Items[index];
    }
    public ItemSO RunGetLegendaryItemFromPool(int index)
    {
        return Run_Legendary_Items[index];
    }
    public void RelocateItemsRarity()
    {

    }
}
