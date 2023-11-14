using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemsInRunDatabase", menuName = "Items_Database")]
public class Items_In_Run : ScriptableObject
{
    public Item[] AllItems;
    public Item[] LockedItems;
    public Item[] UnlockedItems;

    public int AllItemCount
    {
        get { return AllItems.Length; }
    }
    public int LockedItemsCount
    {
        get { return LockedItems.Length; }
    }
    public int UnlockedItemsCount
    {
        get { return UnlockedItems.Length; }
    }
    public Item ItemAleatorioPool()
    {
        Item itemSelect= new Item();  
        return itemSelect;
    }
}
