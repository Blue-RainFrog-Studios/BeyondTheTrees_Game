using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : EnemyController
{
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        else if (!isPlayerInRange(range)){
            currState = EnemyState.Wander;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            currState = EnemyState.Attack;
        }




    }
}
