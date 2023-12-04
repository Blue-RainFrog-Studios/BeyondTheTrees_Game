using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsSquirrel : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform acornTransform;
    [SerializeField] private Transform squirrelTransform;
    [SerializeField] private float speed;
    private bool ended = false;
    public bool consumedAcorn;
    [SerializeField] private int damage;

    [SerializeField]
    private AudioClip eatClip;

    [SerializeField]
    private AudioSource audioSource;

    public string rol;

    private void Awake()
    {
        consumedAcorn = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    public void StartWalkAcorn()
    {

    }

    public Status UpdateWalkAcorn()
    {
        if (CheckAcornInRange())
        {
            consumedAcorn = true;
            return Status.Success;
        }
        else
        {
            if (acornTransform == null)
                return Status.Running;
        

            squirrelTransform.position = Vector2.MoveTowards(squirrelTransform.position, acornTransform.position, speed * Time.deltaTime);
            return Status.Running;
        }
    }

    public void StartWalkPlayer()
    {
        if(audioSource.isPlaying)
            audioSource.Stop();
    }

    public Status UpdateWalkPlayer()
    {
        squirrelTransform.position = Vector2.MoveTowards(squirrelTransform.position, playerTransform.position, speed * Time.deltaTime);
        return Status.Running;
    }

    public bool CheckAcornExists()
    {
        return consumedAcorn;
    }

    public bool CheckAcornInRange()
    {
        if(acornTransform != null)
            return Vector2.Distance(squirrelTransform.position, acornTransform.position) < 1.0f;
        return true;
    }

    public void StartEatAcorn()
    {
        audioSource.PlayOneShot(eatClip);
    }

    public Status UpdateEatAcorn()
    {
        StartCoroutine(WaitSeconds(1));
        if (ended){
            ended = false;
            if(this.rol == "Eater")   //AQUÍ SE MIRA EL ROL
                Destroy(acornTransform.gameObject);
            return Status.Success;
        }
        else
            return Status.Running;
    }
    public bool CheckEnded()
    {
        return ended;
    }

    IEnumerator WaitSeconds(float Time)
    {
        yield return new WaitForSeconds(Time);
        ended = true;
    }
}


