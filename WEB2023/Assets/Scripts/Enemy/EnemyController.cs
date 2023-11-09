using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyState
{
    Idle,

    Wander,

    Follow, 

    Attack,
    
    Die
};
public enum EnemyType
{
    Melee,

    Ranged
};
public class EnemyController : MonoBehaviour
{

    public GameObject ghost;
    GameObject player;
    public GameObject EnemyBullet;
    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;
    public float range;
    public float attackRange;
    Rigidbody2D rb;

    public float coolDown;
    public float speed;
    public float life;

    private bool coolDownAttack = false;
    private bool chooseDir = false;
    public float bulletSpeed;
    private bool dead=false;
    private Vector3 randomDir;
    public Animator animator;
    Vector2 direction;
    public int damage = 20;
    // Start is called before the first frame update

    public bool notInRoom = false; 
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Idle();
        ghost = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState)
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
        }

        if (!notInRoom)
        {
            if (isPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (!isPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Wander;
            }
            if(Vector3.Distance(transform.position,player.transform.position) < attackRange) {
                currState = EnemyState.Attack;
            }
        }
        else
        {
            currState  = EnemyState.Idle;
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

    }

    private bool isPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir=true;
        yield return new WaitForSeconds(Random.Range(2f,8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
       // Quaternion nextRotation = Quaternion.Euler(randomDir);
        //transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir= false;
    }
    void Wander()
    {
        if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if (isPlayerInRange(range))
        {
            currState= EnemyState.Follow;
        }

    }
    void Follow()
    {
        transform.position=Vector2.MoveTowards(transform.position,player.transform.position,speed * Time.deltaTime);
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
    void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType)
            {
                case(EnemyType.Melee):
                    player.GetComponent<KnightScript>().ReceiveAttack(damage);
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet=Instantiate(EnemyBullet,transform.position,Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    
                    StartCoroutine(CoolDown());
                    break;
            }
        }
    }
    public void Die()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(ghost);
        
    }
    public void RecieveDamage(float damage)
    {
        //get the rigid body component
        rb = GetComponent<Rigidbody2D>();
        direction = player.transform.position - transform.position;
        //Vector2 OpositeDirection = direction * -1;
        //do this for 1 second and then reset the velocity
        //rb.AddForce(-direction * 10, ForceMode2D.Impulse);
        player.GetComponent<Rigidbody2D>().AddForce(direction * 10, ForceMode2D.Impulse);

        //call the event PlayFeedback of the script feedback
        //GetComponent<Knockback>().PlayFeedback(player, GetComponent<Rigidbody2D>());


        life -= damage;
        Debug.Log("Recibo da�o");
        if (life <= 0)
        {
            currState = EnemyState.Die;
        }
    }



}
