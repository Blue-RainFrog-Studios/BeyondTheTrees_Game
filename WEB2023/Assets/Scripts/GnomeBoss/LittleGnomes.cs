using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGnomes : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private float HP;
    [SerializeField] private Animator animator;
     GameObject player;
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");
        animator.Play("WakeLittleGnomes");
    }

    // Update is called once per frame
    void Update()
    {
        //if the animation "WakeLittleGnomes" has finished and the walking animation has not started
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WalkingGnomes"))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void RecieveDamage(int damage)
    {
        HP -= damage;
        GetComponent<Knockback>().PlayFeedback(player, GetComponent<Rigidbody2D>());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<KnightScript>().ReceiveAttack(10);
            collision.GetComponent<Knockback>().PlayFeedback(this.gameObject, collision.GetComponent<Rigidbody2D>());
        }
    }
}
