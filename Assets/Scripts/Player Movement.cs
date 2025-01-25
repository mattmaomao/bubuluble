using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool moving = false;
    [SerializeField] float moveSpeed = 5f;

    void Update()
    {
        if (moving)
            autoMove();
    }

    void autoMove()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    // trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            other.GetComponent<Bubble>().trigger();
        }
    }
}
