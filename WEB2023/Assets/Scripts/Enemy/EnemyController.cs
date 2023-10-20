using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyState
{
    Idle,

    Wander,

    Follow, 
    
    Die
};
public class EnemyController : MonoBehaviour
{

    public GameObject ghost;
    GameObject player;
    public EnemyState currState = EnemyState.Idle;

    public float range;
    public float speed;

    private bool chooseDir = false;
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
            case (EnemyState.Die):
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

    public void Death()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<KnightScript>().ReceiveAttack(damage);
        }
    }
}
