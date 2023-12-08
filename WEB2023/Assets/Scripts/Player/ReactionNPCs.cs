using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionNPCs : MonoBehaviour
{
    public bool newPlayer;
    public bool expedicionFallida;
    public bool expedicionExito;
    public bool expedicionInterrumpida;
    public bool allItems;
    public bool allPotions;
    public bool allInventory;
    public bool comingBack;

    public void resetReturnReactions()
    {
        expedicionFallida= false;
        expedicionExito= false;
        expedicionInterrumpida= false;
        comingBack= false;
    }
}
