using BehaviourAPI.UnityToolkit.Demos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornScript : MonoBehaviour
{
    [SerializeField]
    private GameObject squirrel;
    public void Awake()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            squirrel.GetComponent<SScript>().stop.Fire();
            Destroy(this.gameObject);
        }
    }
}
