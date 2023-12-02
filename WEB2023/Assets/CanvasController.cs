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
        StartCoroutine(AnimateFadeOut());
    }

    private IEnumerator AnimateFadeOut()
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        pickupPanel.SetActive(false);
    }
}
