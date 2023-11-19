using BehaviourAPI.UnityToolkit.Demos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;


public enum EnemyState
{
    Idle,

    Wander,

    Follow,

    Attack,

    Die,

    Teleport,

    WalkAcorn,

    EatAcorn
};
public enum EnemyType
{
    Melee,

    Ranged,
    
    Teleport,

    Squirrel
};
public class EnemyController : MonoBehaviour
{

    Material enemyMaterial;

    public GameObject ghost;
    GameObject acorn;
    GameObject player;
    public GameObject EnemyBullet;
    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;
    public float range;
    public float rangeTeleport;
    public float attackRange;
    public float rangeSquirrel;
    public float eatRange;
    [SerializeField]public bool consumed;
    Rigidbody2D rb;

    [SerializeField]
    private AudioClip eatClip;

    [SerializeField]
    private AudioSource audioSource;

    public float coolDown;
    public float coolDownTp;
    public float speed;
    public float life;

    private bool animationEx = false;
    private bool coolDownAttack = false;
    private bool coolDownTeleport = false;
    private bool chooseDir = false;
    public float bulletSpeed;
    private bool dead = false;
    private Vector3 randomDir;
    private Vector3 space = new Vector3(2, 0, 0);
    private Vector3 space1 = new Vector3(0, 2, 0);
    private Vector3 space2 = new Vector3(2, 0, 0);
    private Vector3 space3 = new Vector3(-2, 0, 0);
    private Vector3 space4 = new Vector3(0, -2, 0);
    public Animator animator;
    Vector2 direction;
    Vector2 directionSquirrel;

    public int damage = 20;
    // Start is called before the first frame update

    public bool notInRoom = false;

    public float blinkDuration;
    public int blinkNumber;
    private bool blinking = false;
    private void Awake()
    {
        Idle();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        ghost = this.gameObject;

        enemyMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case (EnemyState.Idle):
                Idle();
                break;
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            case (EnemyState.Die):
                Die();
                break;
            case (EnemyState.Teleport):
                Teleport();
            break;
            case (EnemyState.WalkAcorn):
                WalkAcorn();
                break;
            case (EnemyState.EatAcorn):
                EatAcorn();
                break;
        }

        if (!notInRoom)
        {
            if (isPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (!isPlayerInRange(range) && !isPlayerInRangeTeleport(rangeTeleport) && !isAcornInRangeSquirrel(rangeSquirrel) && currState != EnemyState.Die)
            {
                currState = EnemyState.Wander;
            }
            else if(isPlayerInRangeTeleport(rangeTeleport) && currState!=EnemyState.Die)
            {       
                currState = EnemyState.Teleport;
            }
            else if(isAcornInRangeSquirrel(rangeSquirrel) && currState != EnemyState.Die)
            {
                currState = EnemyState.WalkAcorn;
            }

            if (acorn != null && Vector3.Distance(transform.position, acorn.transform.position) < eatRange && currState != EnemyState.Die && consumed == false)
            {
                currState = EnemyState.EatAcorn;
            }

            if (Vector3.Distance(transform.position, player.transform.position) < attackRange) 
            {
                currState = EnemyState.Attack;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }

        direction = player.transform.position - transform.position;
        if (direction.x > 0.0f)
        {
            if (direction.y + 1.0f > direction.x)
                animator.Play("GhostBack");
            else
                animator.Play("GhostRight");
        }
        else if (direction.x < 0.0f)
        {
            if (direction.y + 1.0f < direction.x)
                animator.Play("GhostFront");
            else
                animator.Play("GhostLeft");
        }
        if (isPlayerInRange(range))
        {
            if (direction.x > 0.0f)
            {
                if (direction.y + 1.0f > direction.x)
                    animator.Play("WalkTop");
                else
                    animator.Play("WalkRight");
            }
            else if (direction.x < 0.0f)
            {
                if (direction.y + 1.0f < direction.x)
                    animator.Play("WalkDown");
                else
                    animator.Play("WalkLeft");

            }
        }
        if (isPlayerInRange(range) && !(Vector3.Distance(transform.position, player.transform.position) < attackRange))
        {
            if (direction.x > 0.0f)
            {
                if (direction.y + 1.0f > direction.x)
                    animator.Play("WalkTopMage");
                else
                    animator.Play("WalkRightMage");
            }
            else if (direction.x < 0.0f)
            {
                if (direction.y + 1.0f < direction.x)
                    animator.Play("WalkDownMage");
                else
                    animator.Play("WalkLeftMage");

            }
        }
        else
        {
            if (direction.x > 0.0f)
            {
                if (direction.y + 1.0f > direction.x)
                    animator.Play("MageUp");
                else
                    animator.Play("MageRight");
            }
            else if (direction.x < 0.0f)
            {
                if (direction.y + 1.0f < direction.x)
                    animator.Play("MageDown");
                else
                    animator.Play("MageLeft");
            }
        }
        if (acorn != null) { 
        directionSquirrel = acorn.transform.position - transform.position;
        }
        if (direction.x < 0.0f && acorn == null){
            animator.Play("SquirrelAnimationRigth");

        }
        else if (direction.x > 0.0f && acorn == null)
        {
            animator.Play("SquirrelAnimation");
        }
        if (directionSquirrel.x < 0.0f && acorn != null)
        {
            animator.Play("SquirrelAnimationRigth");

        }
        else if (directionSquirrel.x > 0.0f && acorn != null)
        {
            animator.Play("SquirrelAnimation");
        }

    }

    private bool isPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
    private bool isPlayerInRangeTeleport(float rangeTeleport)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= rangeTeleport;
    }

    private bool isAcornInRangeSquirrel(float rangeSquirrel)
    {
        acorn = GameObject.FindGameObjectWithTag("Acorn");
        if (acorn != null)
            return Vector3.Distance(transform.position, acorn.transform.position) <= rangeSquirrel;
        else
            switch (enemyType)
            {
                case (EnemyType.Squirrel):

                    currState = EnemyState.Follow;
                    range = 10;
                    break;
            }
        return false;
    }    

    private void WalkAcorn()
    {
        if(acorn != null)
            transform.position = Vector2.MoveTowards(transform.position, acorn.transform.position, speed * Time.deltaTime);
    }

    private void EatAcorn()
    {
        StartCoroutine(WaitSeconds(1));
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(eatClip);
    }

    IEnumerator WaitSeconds(float Time)
    {
        yield return new WaitForSeconds(Time);
        consumed = true;
        if (consumed)
        {
            Destroy(acorn);
        }
        switch (enemyType)
        {
            case (EnemyType.Squirrel):              
                range = 10;
                break;
        }

    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        // Quaternion nextRotation = Quaternion.Euler(randomDir);
        //transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }
    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if (isPlayerInRange(range))
        {
            currState = EnemyState.Follow;
        }
        else if (isPlayerInRangeTeleport(rangeTeleport))
        {
            currState = EnemyState.Teleport;
        }

    }
    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position,speed * Time.deltaTime);
    }

    void Idle()
    {
        StopCoroutine(ChooseDirection());
    }
    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }
    private IEnumerator CoolDownTP()
    {
        coolDownTeleport = true;
        yield return new WaitForSeconds(coolDownTp);
        coolDownTeleport = false;
    }
    private IEnumerator wait()
    {

        animator.Play("Aplauso");
        

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Aplauso"))
        {
            animationEx = false;
            yield return null;
        }
        

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animationEx = true;



    }
    void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType)
            {
                case (EnemyType.Melee):
                    player.GetComponent<KnightScript>().ReceiveAttack(damage);
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(EnemyBullet, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;    
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Teleport):

                    StartCoroutine(CoolDown());
                    player.GetComponent<KnightScript>().ReceiveAttack(damage);
                    break;
                case (EnemyType.Squirrel):
                    player.GetComponent<KnightScript>().ReceiveAttack(damage);
                    StartCoroutine(CoolDown());
                    break;
            }
        }
    }
    void Teleport()
    {

        StartCoroutine(wait());

        if (!coolDownTeleport && animationEx==true)
        {
            animationEx = false;
            StartCoroutine(CoolDownTP());
            
            if (player.GetComponent<KnightScript>().col == -1)
            {
                transform.position = player.transform.position + space;
            }
            else if (player.GetComponent<KnightScript>().col == 0)
            {
                transform.position = player.transform.position + space2;
            }
            else if (player.GetComponent<KnightScript>().col == 1)
            {
                transform.position = player.transform.position + space3;
            }
            else if (player.GetComponent<KnightScript>().col == 2)
            {
                transform.position = player.transform.position + space4;
            }
            else if (player.GetComponent<KnightScript>().col == 3)
            {
                transform.position = player.transform.position + space1;
            }
            else if (player.GetComponent<KnightScript>().col == 4)
            {
                transform.position = player.transform.position + space1;
            }
            else if (player.GetComponent<KnightScript>().col == 5)
            {
                transform.position = player.transform.position + space2;
            }
            else if (player.GetComponent<KnightScript>().col == 6)
            {
                transform.position = player.transform.position + space3;
            }
            else if (player.GetComponent<KnightScript>().col == 7)
            {
                transform.position = player.transform.position + space1;
            }
            else if (player.GetComponent<KnightScript>().col == 8)
            {
                transform.position = player.transform.position + space1;
            }
            else if (player.GetComponent<KnightScript>().col == 9)
            {
                transform.position = player.transform.position + space4;
            }
        }


    }
    public void Die()
    {
        if (player.GetComponent<KnightScript>().king)
        {
            player.GetComponent<CoinCounter>().ExpeditionMoneyChanger(2);
        }
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(ghost);

    }
    public void RecieveDamage(float damage)
    {
        //get the rigid body component
        //rb = GetComponent<Rigidbody2D>();
        //direction = player.transform.position - transform.position;
        //Vector2 OpositeDirection = direction * -1;
        //do this for 1 second and then reset the velocity
        //rb.AddForce(-direction * 10, ForceMode2D.Impulse);
        //player.GetComponent<Rigidbody2D>().AddForce(direction * 10, ForceMode2D.Impulse);

        //call the event PlayFeedback of the script feedback
        //GetComponent<Knockback>().PlayFeedback(player, GetComponent<Rigidbody2D>());


        life -= damage;
        this.GetComponent<Knockback>().PlayFeedback(player , this.gameObject.GetComponent<Rigidbody2D>());
        //Debug.Log("Recibo da�o");
        if (life <= 0)
        {
            currState = EnemyState.Die;
        }

        if (!blinking)
        {
            StartCoroutine(Blink());
        }

    }

    private IEnumerator Blink()
    {
        blinkDuration = 0.05f; ;
        blinkNumber = 3;
        blinking = true;

        // Almacenar el color original del material
        Color colorOriginal = enemyMaterial.color;

        // Cambiar el color a rojo durante el parpadeo
        for (int i = 0; i < blinkNumber; i++)
        {
            enemyMaterial.color = Color.red;
            yield return new WaitForSeconds(blinkDuration);

            enemyMaterial.color = colorOriginal;
            yield return new WaitForSeconds(blinkDuration);
        }

        blinking = false;
    }



}
