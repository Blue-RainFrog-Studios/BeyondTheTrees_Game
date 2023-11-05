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

    //animacion personaje
    public Animator characterAnimator;


    private Map playerInputActions;
    private Vector2 direction;
    public Vector2 attackDirection;
    private Vector2 velocity;
    public float shoteRate = 0.5f;

    private float shotRateTime = 0;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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


        //if the input action recieved is "s"
        if (direction.y < 0)
        {
            //set the animator to play the "walkRight" animation
            characterAnimator.Play("WalkFront");
        }
        else if (direction.y > 0)
        {
            //set the animator to play the "walkRight" animation
            characterAnimator.Play("WalkBack");
        }
        else if (direction.x < 0)
        {
            //set the animator to play the "WalkLeft" animation
            characterAnimator.Play("WalkLeft");
        }
        else if (direction.x > 0)
        {
            //set the animator to play the "walkRight" animation
            characterAnimator.Play("WalkRight");
        }
        else if (direction.x == 0 && direction.y == 0 && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkRight") && player_rb.velocity.x < 0.3 && player_rb.velocity.y < 0.3)
        {
            //play the animation stopright
            characterAnimator.Play("StopRight");

        }
        else if (direction.x == 0 && direction.y == 0 && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkLeft") && player_rb.velocity.x < 0.3 && player_rb.velocity.y < 0.3)
        {
            //play the animation stopright
            characterAnimator.Play("StopLeft");

        }
        else if (direction.x == 0 && direction.y == 0 && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkFront") && player_rb.velocity.x < 0.3 && player_rb.velocity.y < 0.3)
        {
            //play the animation stopright
            characterAnimator.Play("StopFront");

        }
        else if (direction.x == 0 && direction.y == 0 && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("WalkBack") && player_rb.velocity.x < 0.3 && player_rb.velocity.y < 0.3)
        {
            //play the animation stopright
            characterAnimator.Play("StopBack");

        }
        //if the player is going right

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

        //El disparo tiene Cooldown
        if (Time.time > shotRateTime) 
        {
            //Leemos la entrada del usuario
            attackDirection = playerInputActions.Player.Attack.ReadValue<Vector2>();
            //Redondeamos para los controles de moviles
            attackDirection.x = Mathf.Round(attackDirection.x);
            attackDirection.y = Mathf.Round(attackDirection.y);
            //Solo dispara si se ha llevado el joystick suficientemente lejos
            if (attackDirection.magnitude == 1)
            {
                if (this != null) { 
                Instantiate(attack, transform.position, transform.rotation);
                shotRateTime = Time.time + shoteRate;
            }
            }

        }

    }
}
