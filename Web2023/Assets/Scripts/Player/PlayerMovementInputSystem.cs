using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInputSystem : MonoBehaviour
{
    private Rigidbody2D player_rb;
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float smoothness = 0.3f;
    [SerializeField] private GameObject attack;

    private Map playerInputActions;
    private Vector2 direction;
    public Vector2 attackDirection;
    private Vector2 velocity;
    public float shoteRate = 0.5f;

    private float shotRateTime = 0;
    private void Awake()
    {
        player_rb = GetComponent<Rigidbody2D>();
        playerInputActions = new Map();
        playerInputActions.Enable();

        //playerInputActions.Player.Move.performed += Move;
        playerInputActions.Player.Attack.performed += Attack;
    }
    private void Update()
    {
        //Actualiza las posiciones que le decimos mediante el input
        //Move
        direction = playerInputActions.Player.Move.ReadValue<Vector2>();
        
    }
    private void FixedUpdate()
    {
        //Pintamos el movimiento del personaje con la interpolacion para que sea
        //mas visual
        player_rb.velocity = Vector2.SmoothDamp(player_rb.velocity, direction * speed, ref velocity, smoothness);
    }
    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Me estoy moviendo en: " + context.phase);
        Vector2 inputVector = context.ReadValue<Vector2>();
        player_rb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode2D.Force);
        
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (Time.time > shotRateTime) 
        {
            attackDirection = playerInputActions.Player.Attack.ReadValue<Vector2>();
            Instantiate(attack, transform.position, transform.rotation);
            shotRateTime = Time.time + shoteRate;
        }

    }
}
