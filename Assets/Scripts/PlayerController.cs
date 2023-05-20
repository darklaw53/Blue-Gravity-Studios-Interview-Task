using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool canMove = true;

    public Rigidbody2D rb;
    public Animator anim;

    bool lookingRight;
    float currentSpeed;
    Vector2 movement;

    void Update()
    {
        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        currentSpeed = movement.sqrMagnitude;
    }

    private void FlipPlayer()
    {
        lookingRight = !lookingRight;
        if (!lookingRight)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        movement = movement.normalized;
        rb.velocity = movement * moveSpeed;

        if (currentSpeed > 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if ((movement.x > 0 && !lookingRight) || (movement.x < 0 && lookingRight))
        {
            FlipPlayer();
        }
    }
}