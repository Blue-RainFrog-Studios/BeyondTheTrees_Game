using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseInventory : MonoBehaviour
{
    [SerializeField]
    private Button invButton;
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject invActionPanel;
    private void Start()
    {
        this.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => {  inventory.SetActive(false); invActionPanel.SetActive(false);  if (Application.isMobilePlatform) invButton.gameObject.SetActive(true); } );
    }
}
