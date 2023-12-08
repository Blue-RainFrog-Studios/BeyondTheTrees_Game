using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelController : MonoBehaviour
{

    [SerializeField]
    private GameObject sq1;

    [SerializeField]
    private GameObject sq2;

    //[SerializeField]
    //private GameObject acorn;

    public int numProtectors = 1;

    public bool ended = false;

    bool c;

    // Start is called before the first frame update
    void Awake()
    {
        c = true;
    }
    public void DestroyAcorn(GameObject acorn)
    {
        if (c)
        {
            c = false;
            Destroy(acorn);
            ended = true;
        }
    }

}
