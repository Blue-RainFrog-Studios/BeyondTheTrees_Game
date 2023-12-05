using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelController : MonoBehaviour
{

    [SerializeField]
    private GameObject sq1;

    [SerializeField]
    private GameObject sq2;

    public int numProtectors = 1;

    // Start is called before the first frame update
    void Awake()
    {
        sq1.GetComponent<ActionsSquirrel>().rol = "Eater";
        sq2.GetComponent<ActionsSquirrel>().rol = "Protector";
    }
}
