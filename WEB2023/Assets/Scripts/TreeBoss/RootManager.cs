using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator animator;
    void Start()
    {
        int rand=new System.Random().Next(1,3);
        if (rand == 1)
            animator.Play("AttackRoot");
        else if (rand == 2)
        {
            animator.Play("AttackRoot2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if animation ended destroy the object
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<KnightScript>().ReceiveAttack(30);
            collision.GetComponent<Knockback>().PlayFeedback(this.gameObject, collision.GetComponent<Rigidbody2D>());
        }
    }

}
