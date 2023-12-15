using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PotionSpawner : MonoBehaviour
{
    [SerializeField] All_Items_Database ItemDB;

    public GameObject myItemPrefab;
    void Start()
    {
        //RandomItemFromRun();
        //RandomItem_BasedOnProbability_FromRun();
        ItemDB = DataManager_Items_Database.Instance.myItemsData;
        SpawObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetPotion()
    {

        ItemSO data = ItemDB.RunGetPotionToSpawn();

        GameObject newItem = Instantiate(myItemPrefab, transform.position, Quaternion.identity);

        Item itemComponent = newItem.GetComponent<Item>();

        itemComponent.InventoryItem = data;
        itemComponent.Quantity = 1;

    }
    public void SpawObject()
    {
        GetPotion();
    }
}
