using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovementInputSystem : MonoBehaviour
{
    private Rigidbody2D player_rb;
    [SerializeField] public float speed = 8.0f;
    [SerializeField] private float smoothness = 0.3f;
    [SerializeField] private GameObject attack;
    public int nivel = 0;

    //animacion personaje
    public Animator characterAnimator;

    //audio source
    [SerializeField] private AudioSource AttackSoundEffect1;
    [SerializeField] private AudioSource AttackSoundEffect2;

    [SerializeField] private AudioSource WalkSoundEffect;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        this.gameObject.SetActive(true);
        //this.gameObject.transform.position = new Vector2(0.0f, -2.0f);
    }
    private void Update()
    {
        //Actualiza las posiciones que le decimos mediante el input
        //Move
        direction = playerInputActions.Player.Move.ReadValue<Vector2>();
        //if animation dont have attack tag
        if (player_rb.velocity != Vector2.zero && !WalkSoundEffect.isPlaying)
        {
            WalkSoundEffect.Play();
        } else if (player_rb.velocity == Vector2.zero)
        {
            WalkSoundEffect.Stop();
        }
        //make the walksoundeffect volume proportional to the player's velocity
        WalkSoundEffect.volume = player_rb.velocity.magnitude / 10;

        if (direction.y < 0 && !characterAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            characterAnimator.Play("WalkFront");
        }
        else if (direction.y > 0 && !characterAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            characterAnimator.Play("WalkBack");
        }
        else if (direction.x < 0 && !characterAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            characterAnimator.Play("WalkLeft");
        }
        else if (direction.x > 0 && !characterAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //set the animator to play the "walkRight" animation
            characterAnimator.Play("WalkRight");
        }
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
            if (!this.enabled) return;
            attackDirection = playerInputActions.Player.Attack.ReadValue<Vector2>();

            //Redondeamos para los controles de moviles
            attackDirection.x = Mathf.Round(attackDirection.x);
            attackDirection.y = Mathf.Round(attackDirection.y);
            //play audio source for attack
            //get a random number between 1 and 2
            int randomNum = Random.Range(1, 3);
            if (randomNum == 1) { 
                AttackSoundEffect1.Play();
            }
            else { 
                AttackSoundEffect2.Play();
            }


            //Solo dispara si se ha llevado el joystick suficientemente lejos
            if (attackDirection.magnitude == 1)
            {
                if (this != null)
                {
                    Instantiate(attack, transform.position, transform.rotation);
                    shotRateTime = Time.time + shoteRate;
                }
                if (attackDirection.y == -1)
                {
                    if (this != null)
                    {
                        characterAnimator.Play("AttackFront");
                        shotRateTime = Time.time + shoteRate;
                    }
                }
                if (attackDirection.y == 1)
                {
                    if (this != null)
                    {
                        characterAnimator.Play("AttackBack");
                        shotRateTime = Time.time + shoteRate;
                    }

                }
                if (attackDirection.x == -1)
                {
                    if (this != null)
                    {
                        characterAnimator.Play("AttackLeft");
                        shotRateTime = Time.time + shoteRate;
                    }

                }
                if (attackDirection.x == 1)
                {
                    if (this != null)
                    {
                        characterAnimator.Play("AttackRight");
                        shotRateTime = Time.time + shoteRate;
                    }

                }
            }

        }
    }
}
