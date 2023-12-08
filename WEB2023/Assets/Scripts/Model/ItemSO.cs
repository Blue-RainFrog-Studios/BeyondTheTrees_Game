using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ItemSO: ScriptableObject
{
    [field: SerializeField]
    public bool IsStackable { get; set; }

    public int ID => GetInstanceID();  //Para saber si hay que stackear (si ya hay n item con ese id en el inventario)

    [field: SerializeField]
    public int MaxStackSize { get; set; } = 1;

    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    [field: TextArea] //Para poder añadir la descripcion desde el inspector
    public string Descrption { get; set; }

    [field: SerializeField]
    public Sprite ItemImage { get; set; }

    [field: SerializeField]
    public int Attack { get; set; }

    [field: SerializeField]
    public int Defense { get; set; }

    [field: SerializeField]
    public int Speed { get; set; }

    [field: SerializeField]
    public float AttackSpeed { get; set; }

    [field: SerializeField]
    public int Value { get; set; }
    
    [field: SerializeField]
    public int ShopValue { get; set; }

    [field: SerializeField]
    public bool IsPurchased { get; set; }

    [field: SerializeField]
    public int Rarity { get; set; }

    [field: SerializeField]

    public int dropChance { get; set; }

}

namespace Inventory.Model
{ 
}
