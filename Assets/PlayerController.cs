using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public int speed;

    Vector2 movement;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue value) => movement = value.Get<Vector2>();
}