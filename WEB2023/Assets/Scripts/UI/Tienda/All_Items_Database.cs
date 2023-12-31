using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="All_Items_Database")]
[System.Serializable]
public class All_Items_Database : ScriptableObject
{
    public List<ItemSO> All_items;
    public List<ItemSO> Shop_Items;
    public List<UpgradeSO> Shop_Potions_Upgrades;
    
    public List<UpgradeSO> Shop_Inventory_Upgrades;

    public List<ItemSO> Run_Items;
    public List<ItemSO> Run_Common_Items;
    public List<ItemSO> Run_Rare_Items;
    public List<ItemSO> Run_Epic_Items;
    public List<ItemSO> Run_Legendary_Items;

    public List<ItemSO> Run_PotionsAvalible;

    public UpgradeSO PotionToSpawn;


    public int All_Items_Count
    {
        get { return All_items.Count; }

    }

    public int Shop_Items_Count
    {
        get { return Shop_Items.Count; }

    }
    public int Potions_Shop_Items_Count
    {
        get { return Shop_Potions_Upgrades.Count; }

    }
    public int Inventory_Shop_Items_Count
    {
        get { return Shop_Inventory_Upgrades.Count; }

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

    public void PotionPurchaseUpgrades(int index)
    {
        Shop_Potions_Upgrades[index].IsPurchased = true;

        if (Shop_Potions_Upgrades[index].UpgradeTier>=PotionToSpawn.UpgradeTier)
        {
            PotionToSpawn = Shop_Potions_Upgrades[index];
        }
    }

    public void InventoryPurchaseUpgrades(int index)
    {
        Shop_Inventory_Upgrades[index].IsPurchased = true;
    }

    public ItemSO ShopGetItemFromPool(int index)
    {
        return Shop_Items[index];
    }

    public UpgradeSO PotionShopGetItemFromDB(int index)
    {
        return Shop_Potions_Upgrades[index];
    }

    public UpgradeSO InventoryShopGetItemFromDB(int index)
    {
        return Shop_Inventory_Upgrades[index];
    }

    public ItemSO RunGetItemFromPool(int index)
    {
        return Run_Items[index];
    }
    public ItemSO RunGetPotionToSpawn()
    {
        ItemSO yourPotion = Run_PotionsAvalible[0];
        foreach (ItemSO potion in Run_PotionsAvalible)
        {
            if(PotionToSpawn.Name == potion.Name)
            {
                yourPotion = potion;
            }
        }
        return yourPotion;
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
    public void ResetDatabase()
    {
        Run_Rare_Items.Clear();
        Debug.Log("Items raros eliminados");
        Run_Epic_Items.Clear();
        Debug.Log("Items epicos eliminados");
        Run_Legendary_Items.Clear();
        Debug.Log("Items legendarios eliminados");
        /*if (Run_Items.Count > 4)
        {
            for (int i = 4; i < Run_Items.Count+1; i++)
            {
                Run_Items.RemoveAt(i);
            }
        }*/
        Run_Items.Clear();
        for(int i = 0; i< Run_Common_Items.Count; i++)
        {
            Run_Items.Add(Run_Common_Items[i]);
        }
        Debug.Log("Items run eliminados");
        for (int i = 0; i < Shop_Items.Count; i++)
        {
            Shop_Items[i].IsPurchased = false;
        }

        //Reseteo las mejoras de la tienda
        for (int i = 0; i < Potions_Shop_Items_Count; i++)
        {
            Shop_Potions_Upgrades[i].IsPurchased = false;
        }

        for (int i = 0; i < Inventory_Shop_Items_Count; i++)
        {
            Shop_Inventory_Upgrades[i].IsPurchased = false;
        }

        PotionToSpawn = Shop_Potions_Upgrades[0];

        /*Debug.Log("Items purchased eliminados");

        Debug.Log("//////////////////////////////////////////////////////////////////////");
        Debug.Log("RESETEEEEEEEEEEEOOOO");
        Debug.Log("//////////////////////////////////////////////////////////////////////");
    */
    }

    public int CalculateCheapestItem(int currentCheapest)
    {
        
        foreach (ItemSO item in Shop_Items)
        {

            if (item.ShopValue < currentCheapest && !item.IsPurchased)
            {
                currentCheapest = item.ShopValue;
            }
        }
        Debug.Log("Item mas barato recalculado");

        return currentCheapest;
    }
    public int CalculateCheapestPotion(int currentCheapest)
    {
        foreach (UpgradeSO item in Shop_Potions_Upgrades)
        {

            if (item.ShopValue < currentCheapest && !item.IsPurchased)
            {
                currentCheapest = item.ShopValue;
            }
        }
        return currentCheapest;
    }
    public int CalculateCheapestInvUpgrade(int currentCheapest)
    {
        foreach (UpgradeSO item in Shop_Inventory_Upgrades)
        {

            if (item.ShopValue < currentCheapest && !item.IsPurchased)
            {
                currentCheapest = item.ShopValue;
            }
        }
        return currentCheapest;
    }
    }
