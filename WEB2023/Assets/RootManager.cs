using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator animator;
    void Start()
    {
        animator.Play("RootAttack");
    }

    // Update is called once per frame
    void Update()
    {
        //if animation ended destroy the object
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            Destroy(gameObject);
    }
}
