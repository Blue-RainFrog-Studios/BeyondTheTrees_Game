using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHelperManager : MonoBehaviour
{
    
    // Comprobantes del tutorial
    public bool tutorialTienda;
    public bool tutorialPociones;
    public bool tutorialInventario;
    public bool tutorialAumentoInventario;
    public bool tutorialCompleto;

    // Recordatorios al volver
    public bool remindBack;


    // Se tienen que poner precios disparatados para que funcione la primera vez
    public int cheapestItem;
    public int cheapestPotion;
    public int cheapestInvUpgrade;

    public GameObject returnArea;
    public GameObject itemTutorialArea;
    public GameObject potionTutorialArea;
    public GameObject invTutorialArea;
    public GameObject exitTutorialArea;
    public GameObject barrier;
    private GameObject player;

    public GameObject tutoriales;
    
        
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cheapestItem = DataManager_Items_Database.Instance.myItemsData.CalculateCheapestItem(cheapestItem);
        cheapestPotion = DataManager_Items_Database.Instance.myItemsData.CalculateCheapestPotion();
        cheapestInvUpgrade = DataManager_Items_Database.Instance.myItemsData.CalculateCheapestInvUpgrade();
       // Desactiva el tutorial si el jugador no es nuevo
        if (player.GetComponent<ReactionNPCs>().newPlayer)
        {
            tutoriales.SetActive(true);
        }

        // Muestra la ayuda si el jugador tiene selccionado que quiere verlas
        if (player.GetComponent<ReactionNPCs>().comingBack)
        {
            returnArea.SetActive(true);
            Debug.Log("Activando ayuda");
        }
    }
}
