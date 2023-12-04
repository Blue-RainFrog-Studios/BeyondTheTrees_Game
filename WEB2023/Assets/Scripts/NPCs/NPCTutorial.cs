using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCTutorial : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;

    // LAs siguientes variables osn los distintos dialogos que puede mostrar el personaje del tutorial
    public string[] dialogueTutorial;

    public string[] dialogue;
    private int index;

    private GameObject player;
    public GameObject continueButton;
    public float wordSpeed;
    public bool playerIsClose;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {

        //SWitch que cambie segun la situacion que se quiera mostrar en el dialogo
        if (dialogueText.text == dialogue[index])
        {
            player.GetComponent<PlayerMovementInputSystem>().enabled = true;
            //continueButton.SetActive(true);
            //buyButton.SetActive(true);
        }
    }
    public void EnseñarDialogo()
    {
        player.GetComponent<PlayerMovementInputSystem>().enabled = false;
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
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnseñarDialogo();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            zeroText();
        }
    }
}
