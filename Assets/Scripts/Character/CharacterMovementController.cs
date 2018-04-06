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
    private float preJumpVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
	void FixedUpdate ()
    {

        if (Input.GetButtonDown("Jump") && grounded > 0 && !jumping)
        {
            // first jump frame from the ground
            firstJumpTime = Time.time;
            jumping = true;
            angularVelocity *= Data.JumpVelocityLossCurve.Evaluate(0f);

            // basic impulse force
            rb.AddRelativeForce(Vector3.up * Data.JumpImpulseAcceleration * rb.mass, ForceMode2D.Impulse);
        }

        // still in jump frames
        if (Input.GetButton("Jump") && jumping && (Time.time - firstJumpTime) < Data.JumpButtonDuration)
        {
            angularVelocity = preJumpVelocity * Data.JumpVelocityLossCurve.Evaluate((Time.time - firstJumpTime) / Data.JumpButtonDuration);
        }

        angularVelocity = Mathf.Min(Data.MaxSpeed, angularVelocity + Data.Acceleration * Time.deltaTime);
        transform.RotateAround(Vector3.zero, Vector3.forward, angularVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (jumping && !(Input.GetButton("Jump") && (Time.time - firstJumpTime) < Data.JumpButtonDuration))
            {
                jumping = false;
                angularVelocity = preJumpVelocity;
            }

            grounded++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded--;
            preJumpVelocity = angularVelocity;
        }
    }
}
