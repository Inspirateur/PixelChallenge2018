using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{

    public CharacterMovementData Data;

    private Rigidbody2D rb;
    private float angularVelocity = 0f;
    private int grounded = 0;
    private bool jumping = false;
    private float firstJumpTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        firstJumpTime = -Data.JumpInputTime;
    }
	
	void FixedUpdate ()
    {

        if (Input.GetButtonDown("Jump") && grounded > 0 && !jumping)
        {
            // first jump frame from the ground
            firstJumpTime = Time.time;
            jumping = true;
            angularVelocity = Mathf.Max(angularVelocity - Data.JumpVelocityLoss, 0f);
        }
        if (Time.time < firstJumpTime + Data.JumpInputTime)
        {
            // still in jump frames
            if (Input.GetButtonUp("Jump"))
            {
                // stop jumping because the player said so
                jumping = false;
            }
        }
        else
        {
            // stop jumping because the jump input time is over
            jumping = false;
        }

        if (jumping)
        {
            rb.AddRelativeForce(Vector3.up * Data.JumpImpulseAcceleration * rb.mass);
        }

        angularVelocity = Mathf.Min(Data.MaxSpeed, angularVelocity + Data.Acceleration * Time.deltaTime);
        transform.RotateAround(Vector3.zero, Vector3.forward, angularVelocity);
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
}
