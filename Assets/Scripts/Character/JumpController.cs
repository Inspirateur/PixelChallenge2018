using UnityEngine;
using UnityEditor;

public class JumpController : MonoBehaviour
{
    public JumpData Data;

    private Rigidbody2D rb;
    private int grounded = 0;
    private bool jumping = false;
    private float firstJumpTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        firstJumpTime = -Data.JumpInputTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            jumping = false;
            grounded++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded--;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && grounded > 0 && !jumping)
        {
            // first jump frame from the ground
            firstJumpTime = Time.time;
            jumping = true;
        }
        if (Time.time < firstJumpTime + Data.JumpInputTime)
        {
            // still in jump frames
            if (Input.GetButtonUp("Jump"))
            {
                // stop jumping because the player said so
                jumping = false;   
            }
        } else
        {
            // stop jumping because the jump input time is over
            jumping = false;
        }

        if (jumping)
        {
            rb.AddRelativeForce(transform.up * Data.JumpImpulseAcceleration * rb.mass);
        }
    }
}