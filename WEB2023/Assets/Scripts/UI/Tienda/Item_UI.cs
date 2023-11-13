using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


public class Item_UI : MonoBehaviour
{
    [SerializeField] Color itemNotSelectedColor;
    [SerializeField] Color itemSelectedColor;

    [Space(20f)]

    [SerializeField] Sprite purchItemImage;
    [SerializeField] TMP_Text purchItemName;
    [SerializeField] TMP_Text purchItemDescription;
    [SerializeField] TMP_Text purchItemPriceText;
    [SerializeField] Button purchItemPurchaseButton;

    [Space(20f)]
    [SerializeField] Button itemButtton;
    [SerializeField] Image itemImage;
    [SerializeField] Outline itemOutline;
        
    //----------------------------------------
    public void SetItemPosittion(Vector2 Pos)
    {
        GetComponent<RectTransform>().anchoredPosition += Pos;
    }
    public void SetItemImage(Sprite image)
    {
        purchItemImage= image;
    }
    public void SetItemName(string name)
    {
        purchItemName.text = name;
    }
    public void SetItemDescription(string description)
    {
        purchItemDescription.text = description;
    }

    public void SetItemPrice(int price)
    {
        purchItemPriceText.text = price.ToString();
    }

    public void SetItemAsPurchased()
    {
        purchItemPurchaseButton.gameObject.SetActive(false);
        itemButtton.interactable= true;

        itemImage.color = itemNotSelectedColor;
    }

    public void OnItemPurchased(int ItemIndex, UnityAction<int> action)
    {
        purchItemPurchaseButton.onClick.RemoveAllListeners();
        purchItemPurchaseButton.onClick.AddListener (()=> action.Invoke (ItemIndex));
    }
    public void OnItemSelect(int ItemIndex, UnityAction<int> action)
    {
        itemButtton.interactable = true;
        itemButtton.onClick.RemoveAllListeners();
        purchItemPurchaseButton.onClick.AddListener(()=> action.Invoke (ItemIndex));
    }
    public void SelectItem()
    {
        itemOutline.enabled= true;
        itemImage.color = itemSelectedColor;
        itemButtton.interactable= false;
    }

    public void DeSelectItem()
    {
        itemOutline.enabled = false;
        itemImage.color = itemNotSelectedColor;
        itemButtton.interactable = true;
    }
}
