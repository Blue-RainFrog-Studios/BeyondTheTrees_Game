using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBtnScript : MonoBehaviour
{
    public GameObject inventory;

    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => { inventory.SetActive(true); this.gameObject.SetActive(false); } );
    }
}
