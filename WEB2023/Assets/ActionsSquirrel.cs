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

    private List<ActionsSquirrel> squirrels;

    static private List<GameObject> acorns;
    private bool aux = false;

    private void Awake()
    {
        //consumedAcorn = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        squirrels = new List<ActionsSquirrel>(FindObjectsOfType<ActionsSquirrel>());
        if(squirrels != null )
        {
            squirrels[0].rol = "Eater";
            for (int i = 1; i < squirrels.Count; i++)
            {
                squirrels[i].rol = "Protector";
            }
        }
        numReady = 0;
        acorns = new List<GameObject>(GameObject.FindGameObjectsWithTag("Acorn"));
        //ended2 = squirrelController.GetComponent<SquirrelController>().ended;
    }
    void OnDrawGizmos()
    {
        // Obt�n la posici�n del GameObject
        Vector3 position = player.transform.position;

        // Obt�n el vector "right" del GameObject
        //Vector3 rightVector = player.transform.forward;
        Vector3 direccionPC = (squirrels[0].transform.position - player.transform.position).normalized*2;
        // Configura el color de los gizmos
        Gizmos.color = Color.red;

        // Dibuja una l�nea en la escena para representar el vector "right"
        Gizmos.DrawLine(position, position + direccionPC);
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
            if (this.rol == "Eater")   //AQU� SE MIRA EL ROL
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
        {;
            Vector3 direccionPC = (squirrels[0].transform.position - player.transform.position).normalized;
            Vector3 perpendicular = new Vector3(-direccionPC.y, direccionPC.x, 0.0f);
            float auxP;
            if(squirrels.IndexOf(this) %2  == 0)
            {
                auxP = squirrels.IndexOf(this) * 0.5f;
            }
            else{
                auxP = -(squirrels.IndexOf(this) - 1) * 0.5f;
            }
            Vector3 posicionProtegida = squirrels[0].transform.position - direccionPC* 3.0f  + perpendicular*auxP;
            squirrelTransform.position = Vector2.MoveTowards(transform.position, posicionProtegida, speed * Time.deltaTime);
        }

        return Status.Running;
    }

    public bool CheckSquirrelEaterInRange()
    {
        if (rol == "Protector" && squirrels[0] != null)
        {
            return Vector2.Distance(this.transform.position, squirrels[0].transform.position) < 3.0f;
        }
            
        return true;
    }

    public bool CheckFormationDone()
    {
        return (squirrels.Count - 1) == numReady;
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

        Vector3 direccionPC = (squirrels[0].transform.position - player.transform.position).normalized;
        Vector3 perpendicular = new Vector3(-direccionPC.y, direccionPC.x, 0.0f);
        float auxP;
        if (squirrels.IndexOf(this) % 2 == 0)
        {
            auxP = squirrels.IndexOf(this) * 0.5f;
        }
        else
        {
            auxP = -(squirrels.IndexOf(this) - 1) * 0.5f;
        }
        Vector3 posicionProtegida = squirrels[0].transform.position - direccionPC * 3.0f + perpendicular * auxP;
        squirrelTransform.position = Vector2.MoveTowards(transform.position, posicionProtegida, speed * Time.deltaTime);
        return Status.Running;
    }

    public bool CheckAcornEated()
    {
        return acorns.Count == 0 || squirrels[0] == null;
    }
    public bool CheckOtherSquirrels()
    {
        return squirrels.Count > 0;
    }
}


