using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AproximacionTienda : MonoBehaviour
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
    public float wordSpeed;

    



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        /*if (dialogueText.text == dialogueList[index])
        {
            //player.GetComponent<PlayerMovementInputSystem>().enabled = true;
            
        }*/
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
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        player.GetComponent<PlayerMovementInputSystem>().enabled = false;
        foreach (char letter in dialogueList[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        continueButton.SetActive(true);
        if (index == 0)
        {
        }


    }

    public void NextLine()
    {
        continueButton.SetActive(false);

        if (index < dialogueList.Count - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            //zeroText();
            player.GetComponent<PlayerMovementInputSystem>().enabled = true;
            dialoguePanel.SetActive(false); // Cierra el panel de diálogo
            //speakZone.enabled = false;
        }
    }

    public void OpenShop()
    {

        dialoguePanel.SetActive(false);
    }
    public void CrearDialogo()
    {
        /*if ()
        {

        }
        player.GetComponent<ReactionNPCs>().expedicionExito;*/
    }

    private void OnTriggerEnter2D(Collider2D speakZone)
    {
        if (speakZone.CompareTag("Player"))
        {
            CrearDialogo();
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
