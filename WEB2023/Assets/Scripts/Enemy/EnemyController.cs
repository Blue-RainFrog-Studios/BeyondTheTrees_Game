using BehaviourAPI.UnityToolkit.Demos;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;


public enum EnemyState
{
    Idle,

    Follow,

    Attack,

    //Die,

    Teleport,

    Heal,

    Run,

    GoHeal
};
public enum EnemyType
{
    Melee,

    Ranged,
    
    Teleport

};
public class EnemyController : Enemy
{

    
    public Collider2D healCol;
    public GameObject ghost;
    GameObject acorn;
    protected GameObject player;
    public GameObject EnemyBullet;
    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;
    public AudioSource audioSource;
    [SerializeField] private AudioClip fireClip;
    public AudioClip ghostClip;
    public AudioClip teleportHit;
    
  
 
    
    Rigidbody2D rb;

    //public float coolDown;
    public float coolDownTp;
    public float speed;

    private bool healTime = true;
    private bool animationEx = false;
    //private bool coolDownAttack = false;
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
    private Vector3 runAway;
    public Animator animator;
    public GameObject IAmAGhost;
    protected Vector2 direction;
    public float iLife;
    public bool can = false;
    // Start is called before the first frame update

    
    private void Awake()
    {
        Idle();
        
    }
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        
        ghost = this.gameObject;

        

        room = GameObject.FindGameObjectWithTag("RoomController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
    public bool isPlayerInRangeTeleport(float rangeTeleport)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= rangeTeleport;
    }



    public  void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position,speed * Time.deltaTime);
    }

    public void Run()
    {
        runAway = transform.position - player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, transform.position+runAway, speed * Time.deltaTime);
    }
    public IEnumerator WaitHeal()
    {
        healCol.enabled = true;
        GetComponent<ParticleSystem>().Play();
        healTime = false;
        room.GetComponent<RoomController>().healing = true;
        room.GetComponent<RoomController>().posHealer = transform.position+new Vector3(1,1,0);
        yield return new WaitForSeconds(20);
        room.GetComponent<RoomController>().fheal = true;
        room.GetComponent<RoomController>().healing = false;
        GetComponent<ParticleSystem>().Stop();
        healCol.enabled = false;
    }
    public void Heal()
    {
        if (healTime)
        {
            StartCoroutine(WaitHeal());
        }   
    }
    public void GoHeal()
    {
        transform.position = Vector2.MoveTowards(transform.position, room.GetComponent<RoomController>().posHealer, speed * Time.deltaTime);
    }

    public void Idle()
    {
    }
    public IEnumerator Wait()
    {
        can = true;
        yield return new WaitForSeconds(5);
        can = false;
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
    public void Attack()
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
                    audioSource.PlayOneShot(fireClip);
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Teleport):
                    StartCoroutine(CoolDown());
                    player.GetComponent<KnightScript>().ReceiveAttack(damage);
                    break;

            }
        }
    }
    public void Teleport()
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
   
 
    

    
}
