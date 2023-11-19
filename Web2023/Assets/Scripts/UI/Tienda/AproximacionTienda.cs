using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AproximacionTienda : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.8f, 0.8f, 1f);
                
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

}
