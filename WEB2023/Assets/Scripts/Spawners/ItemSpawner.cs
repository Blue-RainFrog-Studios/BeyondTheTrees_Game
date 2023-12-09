using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] All_Items_Database ItemDB;
    // NO DEBEN SUMAR MÁS DE 100
    [SerializeField] int CommonChance;
    [SerializeField] int RareChance;
    [SerializeField] int EpicChance;
    [SerializeField] int LegendaryChance;

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

    public void RandomItemFromRun()
    {
        //int maxList = ItemDB.Run_Items_Count;
        //int RandomNumer = Random.Range(0, (ItemDB.Run_Items_Count + 1));
        //int RandomNumer = Random.Range(0, (maxList));
        int RandomNumer = UnityEngine.Random.Range(0, ItemDB.Run_Items_Count);
        //Debug.Log("Tamano pool" + maxList);
        ItemSO data = ItemDB.RunGetItemFromPool(RandomNumer);

        GameObject newItem = Instantiate(myItemPrefab, transform.position, Quaternion.identity);

        Item itemComponent = newItem.GetComponent<Item>();

        itemComponent.InventoryItem = data;
        itemComponent.Quantity = 1;

    }

    public void RandomItem_BasedOnProbability_FromRun()
    {
        int rarityRandomizer = UnityEngine.Random.Range(0, 101);
        string rarity = "Common";
        if (rarityRandomizer <= CommonChance)
        {
            rarity = "Common";
        }
        if(rarityRandomizer > CommonChance && rarityRandomizer<= CommonChance+RareChance)
        {
            rarity = "Rare";
        }
        if (rarityRandomizer > CommonChance + RareChance && rarityRandomizer <= CommonChance + RareChance+EpicChance)
        {
            rarity = "Epic";
        }
        if (rarityRandomizer > CommonChance + RareChance + EpicChance && rarityRandomizer <= CommonChance + RareChance + EpicChance+ LegendaryChance)
        {
            rarity = "Legendary";
        }
        
            switch (rarity)
            {
                case "Legendary":
                // Si la lista de legendarios está vacía buscamos en los objetos epicos
                if (!ItemDB.Run_Legendary_Items.Any())
                {
                    goto case "Epic";
                }
                else
                {
                    int RandomNumer = UnityEngine.Random.Range(0, ItemDB.Run_Legendary_Items.Count);
                    //Debug.Log("Tamano pool" + maxList);
                    ItemSO data = ItemDB.RunGetLegendaryItemFromPool(RandomNumer);

                    GameObject newItem = Instantiate(myItemPrefab, transform.position, Quaternion.identity);

                    Item itemComponent = newItem.GetComponent<Item>();

                    itemComponent.InventoryItem = data;
                    itemComponent.Quantity = 1;
                }
                    Debug.Log("Spawnea Lenegndary");
                    break;
                case "Epic":
                // Si la lista de epicos está vacía buscamos en los objetos raros
                if (!ItemDB.Run_Epic_Items.Any())
                {
                    goto case "Epic";
                }
                else
                {
                    int RandomNumer = UnityEngine.Random.Range(0, ItemDB.Run_Epic_Items.Count);
                    //Debug.Log("Tamano pool" + maxList);
                    ItemSO data = ItemDB.RunGetEpicItemFromPool(RandomNumer);

                    GameObject newItem = Instantiate(myItemPrefab, transform.position, Quaternion.identity);

                    Item itemComponent = newItem.GetComponent<Item>();

                    itemComponent.InventoryItem = data;
                    itemComponent.Quantity = 1;
                }
                Debug.Log("Spawnea Epic");
                    break;
                case "Rare":
                // Si la lista de raros está vacía buscamos en los objetos comunes
                if (!ItemDB.Run_Rare_Items.Any())
                {
                    goto case "Common";
                }
                else
                {
                    int RandomNumer = UnityEngine.Random.Range(0, ItemDB.Run_Rare_Items.Count);
                    //Debug.Log("Tamano pool" + maxList);
                    ItemSO data = ItemDB.RunGetRareItemFromPool(RandomNumer);

                    GameObject newItem = Instantiate(myItemPrefab, transform.position, Quaternion.identity);

                    Item itemComponent = newItem.GetComponent<Item>();

                    itemComponent.InventoryItem = data;
                    itemComponent.Quantity = 1;
                }
                Debug.Log("Spawnea Rare");
                    break;
                
                case "Common":

                if (ItemDB.Run_Common_Items.Any())
                {
                    int RandomNumer = UnityEngine.Random.Range(0, ItemDB.Run_Common_Items.Count);
                    //Debug.Log("Tamano pool" + maxList);
                    ItemSO data = ItemDB.RunGetCommonItemFromPool(RandomNumer);

                    GameObject newItem = Instantiate(myItemPrefab, transform.position, Quaternion.identity);

                    Item itemComponent = newItem.GetComponent<Item>();

                    itemComponent.InventoryItem = data;
                    itemComponent.Quantity = 1;
                }

                
                Debug.Log("Spawnea Common");
                    break;

                default:
                    Debug.Log("No se ha spawneado nada");
                    break;
            }
    }
    public void SpawObject()
    {
        RandomItemFromRun();
        //RandomItem_BasedOnProbability_FromRun();
        Debug.LogFormat("<color=green>Se ha spawneado un objeto.</color>");
    }
}
