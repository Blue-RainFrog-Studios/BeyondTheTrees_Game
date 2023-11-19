using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprobarAtaque : MonoBehaviour
{

    public bool cercano = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss") || collision.CompareTag("Root") || collision.CompareTag("LittleGnome") ||
            collision.CompareTag("GhostTree") || collision.CompareTag("BossTree"))
        {
            cercano=true;
        }
        else
        {
            cercano = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //this.gameObject.SetActive(false);
    }

    public bool Cercano()
    {
        return cercano;

    }
}
