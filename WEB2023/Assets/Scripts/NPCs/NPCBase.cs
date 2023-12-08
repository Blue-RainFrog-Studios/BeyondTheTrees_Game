using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCBase : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public List<string> dialogueList; 
    private int index;
    public string nameCharacter;
    public Sprite speackImg;
    public BoxCollider2D speakZone;


    private GameObject player;
    public GameObject continueButton;
    public GameObject buyButton;
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
        this.dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        player.GetComponent<PlayerMovementInputSystem>().enabled = false;
        foreach (char letter in dialogueList[index].ToCharArray())
        {
            this.dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        continueButton.SetActive(true);
        if(index == 0)
        {
            buyButton.SetActive(true);
        }
        

    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        buyButton.SetActive(false);

        if (index < dialogueList.Count-1)
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
            //speakZone.enabled = false;
        }
    }

    public void OpenShop()
    {

        shopUI.SetActive(true);
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