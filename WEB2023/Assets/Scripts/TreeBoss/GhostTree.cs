using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTree : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private float HP;
    [SerializeField] private Animator animator;
    private Vector2 direction;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //change the oppacity of the object depending on the distance to the player
        float distance = Vector2.Distance(transform.position, player.transform.position);
        //do it gradually
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 1 - (distance / 20);
        GetComponent<SpriteRenderer>().color = color;



        direction = player.transform.position - transform.position;
        if (direction.x > 0)
            animator.Play("GhostRight 3");
        else
            animator.Play("GhostLeft");

        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void RecieveDamage(int damage)
    {
        GetComponent<Knockback>().PlayFeedback(player, GetComponent<Rigidbody2D>());
        HP -= damage;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<KnightScript>().ReceiveAttack(20);
            collision.GetComponent<Knockback>().PlayFeedback(this.gameObject, collision.GetComponent<Rigidbody2D>());
        }
    }
}
