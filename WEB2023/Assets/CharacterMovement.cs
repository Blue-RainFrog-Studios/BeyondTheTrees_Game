using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float smoothness = 0.3f;

    [SerializeField] private Vector2 direction;

    private Rigidbody2D rb;
    private Vector2 velocity;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInputs();
    }

    void FixedUpdate()
    {
        MoveCharacter(direction);
    }

    void GetInputs()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void MoveCharacter(Vector2 d)
    {
        rb.velocity = Vector2.SmoothDamp(rb.velocity, d * speed, ref velocity, smoothness);
    }

}
