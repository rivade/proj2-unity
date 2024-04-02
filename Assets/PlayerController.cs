using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public float rotationAngleIncrement = 45f;

    Vector2 movement;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RotatePlayer();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void RotatePlayer()
{
    if (movement.magnitude > 0)
    {
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        angle -= 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

}