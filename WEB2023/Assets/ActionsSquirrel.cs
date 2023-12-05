using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsSquirrel : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerTransform;
   // [SerializeField] private Transform acornTransform;
    [SerializeField] private Transform squirrelTransform;
    [SerializeField] private float speed;
    //public bool consumedAcorn;
    int numReady;
    [SerializeField] private int damage;

    [SerializeField]
    private AudioClip eatClip;

    [SerializeField]
    private GameObject squirrelController;

    [SerializeField]
    private AudioSource audioSource;

    public string rol;

    private ActionsSquirrel[] squirrels;

    static private List<GameObject> acorns;
    private bool aux = false;

    private void Awake()
    {
        //consumedAcorn = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        squirrels = FindObjectsOfType<ActionsSquirrel>();
        squirrels[0].rol = "Eater";
        for(int i = 1;i < squirrels.Length; i++)
        {
            squirrels[i].rol = "Protector";
        }
        numReady = 0;
        acorns = new List<GameObject>(GameObject.FindGameObjectsWithTag("Acorn"));
        //ended2 = squirrelController.GetComponent<SquirrelController>().ended;
    }
    
    public void StartWalkAcorn()
    {


    }

    public Status UpdateWalkAcorn()
    {
        if (CheckAcornInRange())
        {
            //consumedAcorn = true;
            return Status.Success;
        }
        else
        {
            if (acorns[0] == null)
                return Status.Running;
        

            squirrelTransform.position = Vector2.MoveTowards(squirrelTransform.position, acorns[0].transform.position, speed * Time.deltaTime);
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
        if (acorns[0] != null)
            return acorns[0].activeSelf;
        return false;
    }

    public bool CheckAcornInRange()
    {
        if(acorns[0] != null)
            return Vector2.Distance(squirrelTransform.position, acorns[0].transform.position) < 1.0f;
        return true;
    }

    public void StartEatAcorn()
    {
        audioSource.PlayOneShot(eatClip);
        StartCoroutine(WaitSeconds(1));
    }

    public Status UpdateEatAcorn()
    {
        
        if (!aux) return Status.Running;
        if (!CheckAcornEated())
        {
            aux = false;
            //squirrelController.GetComponent<SquirrelController>().ended = false;
            if (this.rol == "Eater")   //AQUÍ SE MIRA EL ROL
            {
                //squirrelController.GetComponent<SquirrelController>().DestroyAcorn(acorns[0]);
                Destroy(acorns[0]);
                acorns.RemoveAt(0);
            }
                
            return Status.Success;
        }
        else
            return Status.Running;
    }
    public bool CheckEnded()
    {
        return squirrelController.GetComponent<SquirrelController>().ended;
    }

    IEnumerator WaitSeconds(float Time)
    {
        yield return new WaitForSeconds(Time);
        aux = true;
        //squirrelController.GetComponent<SquirrelController>().ended = true;
    }

    public void StartForming()
    {

    }

    public Status UpdateForming()
    {
        if (CheckSquirrelEaterInRange())
        {
            numReady++;
            return Status.Success;
        }
        else
        {
            squirrelTransform.position = Vector2.MoveTowards(squirrelTransform.position, squirrels[0].transform.position, speed * Time.deltaTime);
            return Status.Running;
        }
    }

    public bool CheckSquirrelEaterInRange()
    {
        if (rol == "Protector")
            return Vector2.Distance(this.transform.position, squirrels[0].transform.position) < 3.0f;
        return true;
    }

    public bool CheckFormationDone()
    {
        return (squirrels.Length - 1) == numReady;
    }

    public void StartProtecting()
    {

    }

    public Status UpdateProtecting()
    {
        if (CheckAcornEated())
        {
            return Status.Success;
        }


        else if (CheckSquirrelEaterInRange())
            return Status.Running;

        squirrelTransform.position = Vector2.MoveTowards(squirrelTransform.position, squirrels[0].transform.position, speed * Time.deltaTime);
        return Status.Running;

    }

    public bool CheckAcornEated()
    {
        return acorns.Count == 0;
    }

}


