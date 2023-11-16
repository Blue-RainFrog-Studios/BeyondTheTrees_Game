using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullManager : MonoBehaviour
{
    [SerializeField]
    private GameObject skull;
    private GameObject[] enemies;
    private GameObject player;
    private int cantidadEnemigos = 0;
    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        //cantidadEnemigos = enemies.Length;
        
    }

    private void Update()
    {
        Debug.Log(enemies.Length);
        //foreach(var enemy in enemies)
        //{
        //     cantidadEnemigos += 1;
        //}
        //if(enemies.Length > 0) { }
    }
}
