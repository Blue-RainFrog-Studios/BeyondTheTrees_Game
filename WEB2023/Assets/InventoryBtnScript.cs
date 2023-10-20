using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBtnScript : MonoBehaviour
{
    public GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => { if (inventory.activeInHierarchy) { inventory.SetActive(false); } else { inventory.SetActive(true); } });
    }
}
