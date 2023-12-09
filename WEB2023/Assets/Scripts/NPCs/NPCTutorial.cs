using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCTutorial : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public List<string> dialogueList;
    private int index;
    public string nameCharacter;
    public Sprite speackImg;
    public BoxCollider2D tutorialBegginingZone;
    public bool ultimoTutorial;
    public bool checksProgress;
    public bool isExit;
    public int checkType; // Donde 0 = items, 1 = pociones, 2 = inventario
    public GameObject actualTutorial;
    public GameObject siguienteTutorial;
    public GameObject barrera;

    private GameObject player;
    public GameObject continueButton;
    public GameObject sceneData;
    public float wordSpeed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            if (isExit)
            {
                player.GetComponent<PlayerMovementInputSystem>().enabled = true;

                dialoguePanel.SetActive(false); // Cierra el panel de diálogo
            }
            else
            {
                player.GetComponent<PlayerMovementInputSystem>().enabled = true;
                dialoguePanel.SetActive(false); // Cierra el panel de diálogo
                                                //this.dialogueList.Clear();
                actualTutorial.SetActive(false);
                
                if (ultimoTutorial)
                {
                    player.GetComponent<ReactionNPCs>().newPlayer = false;
                    
                    sceneData.GetComponent<NPCHelperManager>().TutorialesCompletos();
                }
                else
                {
                    if (siguienteTutorial.GetComponent<NPCTutorial>().ultimoTutorial)
                    {
                        barrera.SetActive(false);
                    }
                    siguienteTutorial.SetActive(true);
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D tutorialBegginingZone)
    {
        if (tutorialBegginingZone.CompareTag("Player"))
        {
            // Para los tutoriales que requieran que el jugador haga algo primero, como por ejemp`lo comprar un objeto

            if (checksProgress)
            {
                switch (checkType)
                {
                    case 0:
                        if (sceneData.GetComponent<NPCHelperManager>().tutorialTienda)
                        {
                            EnseñarDialogo();
                        }
                        break;
                    case 1:
                        if (sceneData.GetComponent<NPCHelperManager>().tutorialPociones)
                        {
                            EnseñarDialogo();
                        }
                        break;
                    case 2:
                        if (sceneData.GetComponent<NPCHelperManager>().tutorialInventario)
                        {
                            EnseñarDialogo();
                        }
                        break;
                        default: break;
                }
            }
            else
            {
                EnseñarDialogo();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D tutorialBegginingZone)
    {
        if (tutorialBegginingZone.CompareTag("Player"))
        {
            zeroText();
            player.GetComponent<PlayerMovementInputSystem>().enabled = true;
        }
    }

}