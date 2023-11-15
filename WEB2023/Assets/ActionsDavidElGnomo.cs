using BehaviourAPI.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsDavidElGnomo : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform DavidElGnomoTransform;
    [SerializeField] private float speed;
    public Status UpdateMethodWalk()
    {
        //make the object move to the player position
        DavidElGnomoTransform.position = Vector2.MoveTowards(DavidElGnomoTransform.position, playerTransform.transform.position, speed * Time.deltaTime);
        return Status.Running;
    }

    public Status UpdateMethodPunch()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //play the punch animation
        return Status.Success;
    }


    public bool CheckPlayerInPunchRange()
    {
        return Vector2.Distance(playerTransform.position, DavidElGnomoTransform.position) < 0.5f;
    }

}
