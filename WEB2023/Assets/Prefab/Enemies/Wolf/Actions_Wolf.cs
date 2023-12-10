using BehaviourAPI.Core;
using BehaviourAPI.UnityToolkit.Demos;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

public class Actions_Wolf : MonoBehaviour
{

    #region Varibles
    private GameObject player;
    private Transform playerTransform;
    private Transform WolfTransform;
    private float distanciaUmbral;
    private bool ended;
    private bool endedDazed;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float TimeCharge;
    [SerializeField] private float TimeEating;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject itemToChase;
    private Transform itemTransform;
    bool collisionDetected = false;
    Vector2 direction;
    Vector3 targetPosition;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        distanciaUmbral = 1;
        playerTransform = player.transform;
        WolfTransform = GetComponent<Transform>();
        itemTransform= itemToChase.transform;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        

    }
    private void Update()
    {
        direction = targetPosition - WolfTransform.position;
        
    }
    #region MethodsIdle
    public void StartMethodWaiting()
    {
        //si el jugador esta a 1 metro de distancia
        //animator.Play("Patroll");

    }
    public Status UpdateMethodWaiting()
    {
        if (Vector2.Distance(playerTransform.position, WolfTransform.position) < 3f || GetComponent<WolfController>().HP < 50)
        {
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }
    #endregion

    #region MethodsWalk
    public void StartMethodWalk()
    {
        Debug.Log("ANDANDO AL JUGADOR");


        //if (playerTransform.position.x > WolfTransform.position.x)
        //    animator.Play("WolfWalkRight");
        //else
        //    animator.Play("WolfWalkLeft");

        
        targetPosition = playerTransform.position;

        if (direction.x > 0.0f)
        {
            if (direction.y + 1.0f > direction.x)
            {
                animator.Play("WolfWalkBack");
            }
            else
            {
                animator.Play("WolfWalkRight");
            }
        }
        else if (direction.x < 0.0f)
        {
            if (direction.y + 1.0f < direction.x)
            {
                animator.Play("WolfWalkFront");
            }
            else
            {
                animator.Play("WolfWalkLeft");
            }
        }
    }
    public Status UpdateMethodWalk()
    {
        //make the object move to the player position
        //WolfTransform.position = Vector2.MoveTowards(WolfTransform.position, playerTransform.transform.position, GetComponent<WolfController>().speed * Time.deltaTime);

        navMeshAgent.SetDestination(player.transform.position);
        return Status.Running;
    }
    #endregion

    #region Charging
    public void StartMethodCharge()
    {
        Debug.Log("cargando");
        ended = false;


        //if (playerTransform.position.x > WolfTransform.position.x)
        //    animator.Play("WolfChargeRight");
        //else
        //    animator.Play("WolfChargeLeft");

        targetPosition = playerTransform.position;

        if (direction.x > 0.0f)
        {
            if (direction.y + 1.0f > direction.x)
            {
                animator.Play("WolfChargeBack");
            }
            else
            {
                animator.Play("WolfChargeRight");
            }
        }
        else if (direction.x < 0.0f)
        {
            if (direction.y + 1.0f < direction.x)
                animator.Play("WolfChargeFront");
            else
                animator.Play("WolfChargeLeft");
        }

        StartCoroutine(WaitSeconds(TimeCharge));
    }
    public Status UpdateMethodCharge()
    {
        if (ended)
        {
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }
    #endregion

    #region ItemChase
    public void StartMethodItemChase()
    {
        Debug.Log("ANDANDO AL ITEM");


        //if (playerTransform.position.x > WolfTransform.position.x)
        //    animator.Play("WolfWalkRight");
        //else
        //    animator.Play("WolfWalkLeft");


        //targetPosition = playerTransform.position;
        targetPosition = itemTransform.position;

        if (direction.x > 0.0f)
        {
            if (direction.y + 1.0f > direction.x)
            {
                animator.Play("WolfWalkBack");
            }
            else
            {
                animator.Play("WolfWalkRight");
            }
        }
        else if (direction.x < 0.0f)
        {
            if (direction.y + 1.0f < direction.x)
            {
                animator.Play("WolfWalkFront");
            }
            else
            {
                animator.Play("WolfWalkLeft");
            }
        }
    }
    public Status UpdateMethodItemChase()
    {
        //make the object move to the player position
        //WolfTransform.position = Vector2.MoveTowards(WolfTransform.position, playerTransform.transform.position, GetComponent<WolfController>().speed * Time.deltaTime);

        navMeshAgent.SetDestination(targetPosition);
        
        if (ComprobarProximidad())
        {
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
        if (itemToChase == null)
        {
            GetComponent<FSMWolf>().LoseItem();
            Debug.Log("Perdiste el ITEEEEM GUASAAAAAAAAAA JAJAJAJAJAJAJAJAJA");
        }

    }
    #endregion

    #region DestroyItem
    public void StartMethodDestroyItem()
    {
        Debug.Log("DESTRUYENDO AL ITEM");
        ended = false;

        //if (playerTransform.position.x > WolfTransform.position.x)
        //    animator.Play("WolfWalkRight");
        //else
        //    animator.Play("WolfWalkLeft");


        //targetPosition = playerTransform.position;
        targetPosition = itemTransform.position;

        animator.Play("WolfEating");
        StartCoroutine(WaitSeconds(TimeEating));
    }
    public Status UpdateMethodDestroyItem()
    {
        if (ended)
        {
            GetComponent<WolfController>().HP += 30;
            itemToChase.SetActive(false);
            //GetComponent<WolfController>().HP += itemToChase.GetComponent<Item>().InventoryItem.HealthValue;
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
        
        //GetComponent<WolfController>().HP += itemToChase.GetComponent<Item>().InventoryItem.HealthValue;
        
    }
    #endregion

    //cambiar bite a lgun tipo de dash hacia el jugador
    #region MethodBite
    public void StartMethodBiteAttack()
    {
        Debug.Log("cargando al jugador");
        collisionDetected = false;
        //screenShake = GetComponent<ScreenShake>();
        GetComponent<Knockback>().strength = 30f;
        ended = false;
        //if (playerTransform.position.x > WolfTransform.position.x)
        //    animator.Play("WolfAtackRight");
        //else
        //    animator.Play("WolfAtackLeft");

        targetPosition = playerTransform.position;

        if (direction.x > 0.0f)
        {
            if (direction.y + 1.0f > direction.x)
            {
                animator.Play("WolfAtackBack");
            }
            else
            {
                animator.Play("WolfAtackRight");
            }
        }
        else if (direction.x < 0.0f)
        {
            if (direction.y + 1.0f < direction.x)
                animator.Play("WolfAtackFront");
            else
                animator.Play("WolfAtackLeft");
        }

     

        /*
        if (playerTransform.position.x > WolfTransform.position.x)
        {
            //StartCoroutine(WalkRightUntilCollision(this.gameObject, GetComponent<WolfController>().speed));
            //screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        else
        {
            //StartCoroutine(WalkLeftUntilCollision(this.gameObject, GetComponent<WolfController>().speed));
            //screenShake.StartCoroutine(screenShake.ShakeScreen());
        }
        */
    }

   
    public Status UpdateMethodBiteAttack()
    {
        //move right for 2 seconds
        //coroutine that moves the object to the right for 2 seconds

        WolfTransform.position = Vector2.MoveTowards(WolfTransform.position, targetPosition, GetComponent<WolfController>().speedJump * Time.deltaTime); //el salto
        if (collisionDetected || WolfTransform.position.x == targetPosition.x && WolfTransform.position.y == targetPosition.y)
        {
            GetComponent<Knockback>().strength = 10f;
            StopAllCoroutines();
            collisionDetected = false;
            
            return Status.Success;
        }
        ended = true;
        return Status.Running;

    }
    #endregion

    #region Dazed
    public void StartMethodDazed()
    {
        Debug.Log("estoy stuned");
        ended = false;
        animator.Play("WolfDazed");
        //cambia el color a morado
        StartCoroutine(WaitSeconds(TimeCharge));
    }
    public Status UpdateMethodDazed()
    {
        if (ended)
        {
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }
    #endregion

    #region CheckTransitions
    public bool CheckPlayerInBiteRange()
    {
        return Vector2.Distance(playerTransform.position, WolfTransform.position) < 4f;
    }

    public bool CheckCollisionWithYAxis()
    {
        return ((playerTransform.position.y > WolfTransform.position.y - 1) && (playerTransform.position.y < WolfTransform.position.y + 1));
    }
    
    public bool CheckItemExist()
    {
        if (itemToChase != null && itemToChase.activeSelf )
        {
            return true;
        }
        else
        {
            return false;
        }
        
        //return GetComponent<DavidElGnomoController>().HP < GetComponent<DavidElGnomoController>().HPSecondPhase;
    }

    public bool CheckHPLow()
    {
        return GetComponent<WolfController>().HP <= GetComponent<WolfController>().initialHP*0.3;
    }
    #endregion


    #region Courutines
    IEnumerator WaitSeconds(float Time)
    {
        yield return new WaitForSeconds(Time);
        ended = true;
    }


    IEnumerator WalkRightUntilCollision(GameObject gnomoObj, float speed)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = gnomoObj.transform.position;
        Vector3 targetPos = startingPos + Vector3.right * 20;

        while (gnomoObj.GetComponent<Collider2D>())
        {
            gnomoObj.transform.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime) * 0.45f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ended = true;

    }
    IEnumerator WalkLeftUntilCollision(GameObject gnomoObj, float speed)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = gnomoObj.transform.position;
        Vector3 targetPos = startingPos + Vector3.left * 20;
        //mientras que no haya collisiones
        while (gnomoObj.GetComponent<Collider2D>())
        {
            gnomoObj.transform.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime) * 0.45f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ended = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") == false)
        {
            collisionDetected = true;
            GetComponent<FSMWolf>().Crash();
            Debug.Log(collisionDetected);
        }

        
    }
    #endregion
    private bool ComprobarProximidad()
    {
        float distancia = Vector3.Distance(itemTransform.position, WolfTransform.position);

        if (distancia < distanciaUmbral)
        {
            return true; 
        }
        else
        {
            return false;
        }
    }
}
