using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        //Imagen del objeto
        [SerializeField]
        private Image itemImage;

        //Cantidad de objetos
        [SerializeField]
        private TMP_Text quantityTxt;

        [SerializeField]
        private Image borderImage;

        //Action manda la referencia del item que se manipula y ayuda a buscar
        //en la lista de objetos de UIInventoryPage
        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,
            OnRightMouseBtnClick;

        //Los eventos no se llaman si la celda está vacía
        private bool empty = true;

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            empty = true;
        }

        public void Deselect()
        {
            borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";
            empty = false;
        }

        public void Select()
        {
            borderImage.enabled = true;
        }

        //Interfaces necesarias para que funcione el scroll
        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);  //Si hay algo asociado se llama al método
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                //!!!!!IMPORTANTE: SÓLO VALE PARA PC
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}