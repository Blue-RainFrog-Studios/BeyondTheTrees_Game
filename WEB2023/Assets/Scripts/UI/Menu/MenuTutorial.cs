using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTutorial : MonoBehaviour
{
    [SerializeField]
    private Button btnR;
    [SerializeField]
    private Button btnL;
    [SerializeField]
    private GameObject escenas;
    private int orden;
    private void Awake()
    {
        orden = 0;
    }
    private void Update()
    {
        if (orden == 0)
        {
            btnL.gameObject.SetActive(false);
            btnR.gameObject.SetActive(true);
        }
        else if (orden == escenas.transform.childCount - 1)
        {
            btnR.gameObject.SetActive(false);
            btnL.gameObject.SetActive(true);
        }
        else
        {
            btnL.gameObject.SetActive(true);
            btnR.gameObject.SetActive(true);
        }
    }
    public void IrIzquierda()
    {
        orden--;
        escenas.transform.GetChild(orden).gameObject.SetActive(true);
        escenas.transform.GetChild(orden + 1).gameObject.SetActive(false);

    }
    public void IrDerecha()
    {
        orden++;
        escenas.transform.GetChild(orden).gameObject.SetActive(true);
        escenas.transform.GetChild(orden - 1).gameObject.SetActive(false);
    }
}
