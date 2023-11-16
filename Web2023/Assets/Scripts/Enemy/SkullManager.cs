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
            enemy = collision.gameObject;
            if(collision.gameObject.GetComponent<EnemyController>().life <= 0)
            {
                Debug.Log("TETAS GORDAS");
                skull.GetComponent<Animator>().SetTrigger("Fase 1-2");
            }
        }
    }
}
