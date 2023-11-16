using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullManager : MonoBehaviour
{
    [SerializeField]
    private GameObject skull;
    private  List<GameObject> enemies = new List<GameObject>();
    private GameObject enemy;
    private GameObject player;
    private int cantidadEnemigos = 0;
    private int valor = 9;
    private void Awake()
    {
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        //cantidadEnemigos = enemies.Length;
        
    }

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.gameObject);
            cantidadEnemigos++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("PENE");
            //enemy = collision.gameObject;
            if(collision.gameObject.GetComponent<EnemyController>().life <= 0)
            {
                Debug.Log("TETAS GORDAS");
                if(valor == 9)
                {
                    skull.GetComponent<Animator>().SetTrigger("Fase 1-2");
                    valor = 6;
                }
                else if (valor == 6)
                {
                    skull.GetComponent<Animator>().SetTrigger("Fase 2-3");
                    valor = 3;
                }
                else if (valor == 3)
                {
                    skull.GetComponent<Animator>().SetTrigger("Fase 3-4");
                    valor = 0;
                }

            }
        }
    }
}
