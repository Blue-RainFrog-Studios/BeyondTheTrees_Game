using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHelperManager : MonoBehaviour
{
    
    // Comprobantes del tutorial
    public bool tutorialTienda;
    public bool tutorialPociones;
    public bool tutorialInventario;
    //public bool tutorialAumentoInventario;
    public bool tutorialCompleto;

    // Tiendas para activar o desactivar
    public GameObject TiendaItems;
    public GameObject TiendaPociones;
    public GameObject TiendaInventario;

    // Recordatorios al volver
    public bool remindBack;


    // Se tienen que poner precios disparatados para que funcione la primera vez
    public int cheapestItem;
    public int cheapestPotion;
    public int cheapestInvUpgrade;

    public GameObject returnArea;
 
    public GameObject barrier;
    private GameObject player;

    public GameObject primerTutorial;
    
        
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cheapestItem = DataManager_Items_Database.Instance.myItemsData.CalculateCheapestItem(cheapestItem);
        cheapestPotion = DataManager_Items_Database.Instance.myItemsData.CalculateCheapestPotion(cheapestPotion);
        cheapestInvUpgrade = DataManager_Items_Database.Instance.myItemsData.CalculateCheapestInvUpgrade(cheapestInvUpgrade);
       // Desactiva el tutorial si el jugador no es nuevo
        if (player.GetComponent<ReactionNPCs>().newPlayer)
        {
            primerTutorial.SetActive(true);
        }
        else
        {
            this.TutorialesCompletos();
            barrier.SetActive(false);
        }

        // Muestra la ayuda si el jugador tiene selccionado que quiere verlas
        if (player.GetComponent<ReactionNPCs>().comingBack)
        {
            returnArea.SetActive(true);
            Debug.Log("Activando ayuda");
        }

        player.transform.position = new Vector3(0.0f, -4.0f);
    }
    public void TutorialesCompletos()
    {
        tutorialTienda = true;
        tutorialPociones = true;
        tutorialInventario = true;
        tutorialCompleto = true;
    }
}
