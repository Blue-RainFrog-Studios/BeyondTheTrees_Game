using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int HP;
     GameObject Tree;

    int TotalHP;
    private void Start()
    {
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
