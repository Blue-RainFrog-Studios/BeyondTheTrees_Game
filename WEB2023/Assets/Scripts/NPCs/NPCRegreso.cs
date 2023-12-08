using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCAyuda : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public List<string> dialogueList;
    private int index;
    public string nameCharacter;
    public Sprite speackImg;
    public BoxCollider2D speakZone;

    private GameObject player;
    public GameObject continueButton;
    public GameObject sceneData;
    public float wordSpeed;


    /*private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }*/
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CrearFrase();
    }

    public void EnseñarDialogo()
    {
        //player.GetComponent<PlayerMovementInputSystem>().enabled = false;
        if (dialoguePanel.activeInHierarchy)
        {
            zeroText();
        }
        else
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }

    public void zeroText()
    {
        this.dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        player.GetComponent<PlayerMovementInputSystem>().enabled = false;
        foreach (char letter in this.dialogueList[index].ToCharArray())
        {
            this.dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        continueButton.SetActive(true);
        


    }

    public void NextLine()
    {
        continueButton.SetActive(false);

        if (index < this.dialogueList.Count - 1)
        {
            index++;
            this.dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            //zeroText();
            player.GetComponent<PlayerMovementInputSystem>().enabled = true;
            dialoguePanel.SetActive(false); // Cierra el panel de diálogo
            player.GetComponent<ReactionNPCs>().comingBack = false;
            this.dialogueList.Clear();
            player.GetComponent<ReactionNPCs>().resetReturnReactions();
            speakZone.enabled = false;
        }
    }

    public void OpenShop()
    {

        dialoguePanel.SetActive(false);
    }

    public void CrearFrase()
    {
        this.dialogueList.Clear();
        // Frases para recibir al jugador
        if (player.GetComponent<ReactionNPCs>().expedicionExito)
        {
            this.dialogueList.Add("¡JAJA! Buen trabajo chavalote");
            this.dialogueList.Add("Seguro que te has hecho con un buien botín");
        }
        else if (player.GetComponent<ReactionNPCs>().expedicionInterrumpida)
        {
            this.dialogueList.Add("¿Se te ha complicado la expedición, eh?");
            this.dialogueList.Add("No pasa nada, haz acopio de pociones o comprate algo nuevo");
            this.dialogueList.Add("Ya verás como la siguiente se te da mejor");
        }
        else if (player.GetComponent<ReactionNPCs>().expedicionFallida)
        {
            this.dialogueList.Add("¿Va todo bien chaval?");
            this.dialogueList.Add("Hemos tenido que sacarte del bosque a cuestas");
            this.dialogueList.Add("Tómatelo con un poco más de calma la próxima vez, no queremos que te pase nada");
        }

        // Frases para decirle al jugador que puede comprar

        if (sceneData.GetComponent<NPCHelperManager>().cheapestItem<=player.GetComponent<CoinCounter>().totalMoney)
        {
            this.dialogueList.Add("¿Hey! Parece que tienes suficientes monedas para comprar algun objeto nuevo");
            this.dialogueList.Add("Habla con el viejo Eerling y comprate algo bonito");
        }


        // Activas cuando estén las tiendas de pociones e inventario


        /*if (sceneData.GetComponent<NPCHelperManager>().cheapestPotion <= player.GetComponent<CoinCounter>().totalMoney)
        {
            this.dialogueList.Add("Tienes una buena cantidad de dinero, deberías pasarte a mirar las pociones");
            this.dialogueList.Add("Puede que Alexia tenga algún brebaje que te sea de ayuda");
        }
        if (sceneData.GetComponent<NPCHelperManager>().cheapestInvUpgrade <= player.GetComponent<CoinCounter>().totalMoney)
        {
            this.dialogueList.Add("Veo que tienes una pequeña fortuna, deberías pensarte mejorar tu mochila");
            this.dialogueList.Add("El bueno de Florentino siempre está dispuesto a mejorar tu inventario");
        }*/

    }

    private void OnTriggerEnter2D(Collider2D speakZone)
    {
        if (speakZone.CompareTag("Player") && sceneData.GetComponent<NPCHelperManager>().remindBack)
        {
            EnseñarDialogo();
        }
    }

    private void OnTriggerExit2D(Collider2D speakZone)
    {
        if (speakZone.CompareTag("Player"))
        {
            zeroText();

        }
    }
}
