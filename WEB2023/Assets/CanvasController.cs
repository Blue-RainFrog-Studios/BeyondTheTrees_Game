using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    public GameObject pickupPanel;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TextMeshProUGUI itemName;

    [SerializeField]
    private TextMeshProUGUI itemDescription;

    [SerializeField]
    private float duration = 0.1f;

    public void AssignItem(Sprite image, string name, string description)
    {
        itemImage.sprite = image;
        itemName.text = name;
        itemDescription.text = description;
        StartCoroutine(WaitSeconds(6));
    }

    IEnumerator WaitSeconds(float Time)
    {
        yield return new WaitForSeconds(Time);
        pickupPanel.SetActive(false);
    }
}
