using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 5;
    [SerializeField] private float jump_height = 2;
    float horizontalDir = 0;
    bool isStuck = false;
    Vector3 dir;
    bool isGrounded = false;

    Rigidbody2D rb; 

    void Start()
    {
        dir = new Vector3(x:0, y:0, z:0);

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStuck )
        {
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            rb.velocity = new Vector2(x: horizontalDir * speed, rb.velocity.y);
        }
    }

    private void OnMove(InputValue value)
    {
        if (!isStuck)
        {
            Vector2 inputDir = value.Get<Vector2>();
            float inputX = inputDir.x;
            horizontalDir = inputX;
        }


    }

    private void OnJump(InputValue value)
    {
        if (isGrounded)
        {
           rb.velocity = new Vector2(rb.velocity.x, jump_height);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Sticky"))
        {
            isStuck = true;
        }




    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Sticky"))
        {
            isStuck = false;
        }
    }
}
