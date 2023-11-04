using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private UIInventoryItem item;

    private void Awake()
    {
        canvas = transform.GetComponentInParent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();  //El prefab es el hijo del GO MouseFollower
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }

    //Se calcula la logica para que siga al mouse
    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(  //Transforma de voordenadas en la ScreenSpace a coordenadas en local del RectTransform
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position  //Se guarda en la variable posicion
                );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);
    }
}
