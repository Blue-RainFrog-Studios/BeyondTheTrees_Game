using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionsSquirrel : Enemy
{
    [SerializeField] private float speed;


    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private AudioClip eatClip;
    [SerializeField]
    private GameObject squirrelController;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float rangeToEater = 4.0f;
    [SerializeField]
    private Animator animator;
    public bool rolB;
    private List<ActionsSquirrel> squirrels;
    static bool hayArdillaCome;
    static private List<GameObject> acorns;
    private bool aux = false;

    private void Awake()
    {
        hayArdillaCome = false;
        squirrels = new List<ActionsSquirrel>(FindObjectsOfType<ActionsSquirrel>());
        acorns = new List<GameObject>(GameObject.FindGameObjectsWithTag("Acorn"));
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
    private void Update()
    {
        if (!notInRoom)
            GetComponent<BTSquirrel>().enabled = true;

        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            if(!coolDownAttack)
            {
                player.GetComponent<KnightScript>().ReceiveAttack(damage);
                StartCoroutine(CoolDown());
            }

        }
    }
    public void StartWalkAcorn() {
        if(acorns[0] != null)
        {
            if (transform.position.x >= acorns[0].transform.position.x)
            {
                animator.Play("SquirrelAnimationRigth");
            }
            else
            {
                animator.Play("SquirrelAnimation");
            }
        }
        
        this.rolB = true;
        hayArdillaCome = true;
    }

    public Status UpdateWalkAcorn()
    {

        
        if (CheckAcornInRange())
        {
            return Status.Success;
        }
        else
        {
            if (acorns[0] == null)
                return Status.Running;
        

            this.transform.position = Vector2.MoveTowards(transform.position, acorns[0].transform.position, speed * Time.deltaTime);
            return Status.Running;
        }
    }

    public void StartWalkPlayer()
    {
            
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    public Status UpdateWalkPlayer()
    {
        if (transform.position.x >= player.transform.position.x)
        {
            animator.Play("SquirrelAnimationRigth");
        }
        else
        {
            animator.Play("SquirrelAnimation");
        }
        //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        //squirrelTransform.position = Vector2.MoveTowards(squirrelTransform.position, playerTransform.position, speed * Time.deltaTime);

        navMeshAgent.SetDestination(player.transform.position);
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
            return Vector2.Distance(this.transform.position, acorns[0].transform.position) < 1.0f;
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
            if (this.rolB)   //AQU� SE MIRA EL ROL
            {
                Destroy(acorns[0]);
                acorns.RemoveAt(0);
                hayArdillaCome = false;
            }
                
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
        
    }
    IEnumerator WaitSeconds(float Time)
    {
        yield return new WaitForSeconds(Time);
        aux = true;
    }


    public void StartProtecting()
    {
        this.rolB = false;
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
        Vector3 posicionProtegida = squirrels[0].transform.position - direccionPC * rangeToEater + perpendicular * auxP;
        transform.position = Vector2.MoveTowards(transform.position, posicionProtegida, speed * Time.deltaTime);
        return Status.Running;
    }

    public bool CheckAcornEated()
    {
        return acorns.Count == 0 || squirrels[0] == null;
    }
    public bool CheckSquirrelEater()
    {
        return hayArdillaCome;
    }
      override public void RecieveDamage(float damage)
    {
        if(life <= 0)
        {
            if(this.rolB)
                hayArdillaCome = false;
        }
            
        base.RecieveDamage(damage);
    }
}


