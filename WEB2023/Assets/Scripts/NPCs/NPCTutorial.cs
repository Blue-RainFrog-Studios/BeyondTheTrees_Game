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
    public List<string> dialogueTutorial;

    public List<string> dialogueList;
    private int index;
    public string nameCharacter;
    public Image speackImg;

    private GameObject player;
    public GameObject continueButton;
    public float wordSpeed;
    public bool playerIsClose;

    public BoxCollider2D areaRegreso;
    public BoxCollider2D areaHablar;

    

    

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

    private void OnTriggerEnter2D(Collider2D speakZone)
    {
        if (speakZone.CompareTag("Player"))
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