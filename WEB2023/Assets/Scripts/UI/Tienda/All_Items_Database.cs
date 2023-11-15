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
        Shop_Items[index].isPurchased = true;
        Run_Items.Add(Shop_Items[index]);
    }

    public void Random_Run_Item()
    {

    }
}
