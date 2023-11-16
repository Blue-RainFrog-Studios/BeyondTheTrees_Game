using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public int valor = 9;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CoinCounter>().ExpeditionMoneyChanger(valor);
            Destroy(this.gameObject);
        }
    }
}
