using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarrMovil4 : MonoBehaviour
{
    //private bool cruceta;

    [SerializeField]
    private GameObject pausa;
    private void Update()
    {
        //cruceta = GameObject.FindGameObjectWithTag("Pause").GetComponent<MenuPausa>().GetAtaqueCruceta();
        //gameObject.SetActive(Application.isMobilePlatform && pausa.GetComponent<MenuPausa>().GetAtaqueCruceta());
    }
}
