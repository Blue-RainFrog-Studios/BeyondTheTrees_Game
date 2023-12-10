using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeSO : ScriptableObject
{

    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    [field: TextArea] //Para poder añadir la descripcion desde el inspector
    public string Descrption { get; set; }

    [field: SerializeField]
    public Sprite UpgradeImage { get; set; }

    [field: SerializeField]
    public int ShopValue { get; set; }

    [field: SerializeField]
    public bool IsPurchased { get; set; }

    [field: SerializeField]
    public bool InventoryUpgrade { get; set; }

    [field: SerializeField]
    public bool PotionsUpgrade { get; set; }

    [field: SerializeField]
    public int UpgradeTier { get; set; }
}
