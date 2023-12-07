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
    public Image speackChar;
    public BoxCollider2D speakZone;


    private GameObject player;
    public GameObject continueButton;
    public GameObject buyButton;
    public float wordSpeed;
    public bool playerIsClose;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (dialogueText.text == dialogueList[index])
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
        foreach (char letter in dialogueList[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        buyButton.SetActive(false);
        if (index < dialogueList.Count)
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

    public void OpenShop()
    {
        shopUI.SetActive(true);
        dialoguePanel.SetActive(false);
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