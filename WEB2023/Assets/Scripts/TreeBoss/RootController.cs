using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int HP;
    [SerializeField] private Animator animator;
    GameObject Tree;

    int TotalHP;
    private void Start()
    {
        int rand = new System.Random().Next(1, 4);
        if (rand == 1)
            animator.Play("Root1");
        else if (rand == 2)
            animator.Play("Root2");
        else if (rand == 3) { 
            animator.Play("Root3");
        }
        Tree = GameObject.FindGameObjectWithTag("BossTree");
        TotalHP = HP;
    }
    public void RecieveDamage(int damage)
    {
        HP -= damage;
    }
    private void Update()
    {
        if (HP <= 0)
        {
            Tree.GetComponent<SpookyTreeController>().RecieveDamage(200);
            Destroy(this.gameObject);
        }
    }

}
