using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] All_Items_Database ItemDB;
    public GameObject myItemPrefab;

    void Start()
    {
        RandomItemFromRun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomItemFromRun()
    {
        int RandomNumer = Random.Range(0, (ItemDB.Run_Items_Count + 1));
        ItemSO data = ItemDB.RunGetItemFromPool(RandomNumer);

        GameObject newItem = Instantiate(myItemPrefab, transform.position, Quaternion.identity);

        Item itemComponent = newItem.GetComponent<Item>();

        itemComponent.InventoryItem = data;
        itemComponent.Quantity = 1;

    }
}
