using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 3f;
    private Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    [SerializeField] private DynamicJoystick joystick;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);
        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (movingSpeed * Time.fixedDeltaTime));
    }
}
