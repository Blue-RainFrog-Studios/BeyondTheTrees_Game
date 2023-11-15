using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;

public class SScript : MonoBehaviour
{

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void Update()
    {
        this.gameObject.transform.parent.rotation.y =0;
    }
}
