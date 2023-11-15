using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItems
{
    public enum NombreItem
    {
        DagaSolar,
        DagaLunar,

    }


    public static int GetPrecio(NombreItem nombre)
    {
        switch(nombre)
        {
            default:
            case NombreItem.DagaSolar:          return 20;
            case NombreItem.DagaLunar:          return 30;
        }
    }

    public static Sprite GetSprite(NombreItem nombre)
    {
        switch(nombre)
        {
            default:
            case NombreItem.DagaSolar:          return ItemAssets.Instance.DagaSolarSprite;
            case NombreItem.DagaLunar:          return ItemAssets.Instance.DagaLunarSprite;
        }
    }
    public NombreItem nombreItem;
    public int cantidad;
}
