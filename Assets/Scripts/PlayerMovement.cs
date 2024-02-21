using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    float dirX = 0f;
    float dirY = 0f;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        dirY = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirY * moveSpeed, rb.velocity.x);
    }
}
