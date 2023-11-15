using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsDavidElGnomo : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform DavidElGnomoTransform;
    [SerializeField] private float speed;
    public void StartMethodWalk()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }
    public Status UpdateMethodWalk()
    {
        //make the object move to the player position
        DavidElGnomoTransform.position = Vector2.MoveTowards(DavidElGnomoTransform.position, playerTransform.transform.position, speed * Time.deltaTime);
        return Status.Running;
    }

    public void StartMethodWalkAttack()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }
    public Status UpdateMethodWalkAttack()
    {
        return Status.Success;
    }


    public void StartMethodPunch()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    public Status UpdateMethodPunch()
    {
        //wait 2 seconds
                

        //play the punch animation
        return Status.Success;
    }


    public bool CheckPlayerInPunchRange()
    {
        return Vector2.Distance(playerTransform.position, DavidElGnomoTransform.position) < 2f;
    }

    public bool CheckCollisionWithYAxis()
    {
        return ((playerTransform.position.y > DavidElGnomoTransform.position.y-1) && (playerTransform.position.y < DavidElGnomoTransform.position.y + 1));
    }
}
