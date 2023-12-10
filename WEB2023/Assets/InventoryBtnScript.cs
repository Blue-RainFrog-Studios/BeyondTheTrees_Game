using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBtnScript : MonoBehaviour
{
    public GameObject inventory;

    public Button inventoryBtn;

    void Start()
    {
        inventoryBtn.onClick.AddListener(() => { inventory.SetActive(true); this.gameObject.SetActive(false); } );
    }
}
