using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    [SerializeField] private Transform attackControler;
    [SerializeField] private GameObject attack;

    private Map playerInputActions;
    private Vector2 direction;

    private void Update()
    {
        //Pulsamos las flechas
        //direction = playerInputActions.Player.Attack.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        //Trigger();
    }

    private void Trigger()
    {
        Instantiate(attack, direction, this.transform.rotation);
    }
}
