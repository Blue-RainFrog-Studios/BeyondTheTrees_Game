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
         animator.Play("WalkingGnomes");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if(HP<= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void RecieveDamage(int damage)
    {
        HP -= damage;
    }
}
