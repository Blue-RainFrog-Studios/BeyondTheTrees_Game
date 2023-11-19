using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpookyTreeController : MonoBehaviour
{
    private int hpTree;
    void Start()
    {
        hpTree = 1000;
        Debug.Log("Tree HP: " + hpTree);
    }

    // Update is called once per frame
    void Update()
    {
        if (hpTree <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Victory");
            
        }
    }
    public void RecieveDamage(int damage)
    {
        hpTree -= damage;
        Debug.Log("Tree HP: " + hpTree + " damage taken: "+damage);
    }
}
