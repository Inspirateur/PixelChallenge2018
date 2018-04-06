using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{

    public CharacterMovementData Data;
    public float AngularVelocity = 0f;

    private Rigidbody2D rb;
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
            AngularVelocity *= Data.JumpVelocityLossCurve.Evaluate(0f);

            // basic impulse force
            rb.AddRelativeForce(Vector3.up * Data.JumpImpulseAcceleration * rb.mass, ForceMode2D.Impulse);
        }

        // still in jump frames
        if (Input.GetButton("Jump") && jumping && (Time.time - firstJumpTime) < Data.JumpButtonDuration)
        {
            AngularVelocity = preJumpVelocity * Data.JumpVelocityLossCurve.Evaluate((Time.time - firstJumpTime) / Data.JumpButtonDuration);
        }

        AngularVelocity = Mathf.Min(Data.MaxSpeed, AngularVelocity + Data.Acceleration * Time.deltaTime);
        transform.RotateAround(Vector3.zero, Vector3.forward, AngularVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (jumping && !(Input.GetButton("Jump") && (Time.time - firstJumpTime) < Data.JumpButtonDuration))
            {
                jumping = false;
                AngularVelocity = preJumpVelocity;
            }

            grounded++;
        }

        LightUp lightUp = collision.collider.GetComponent<LightUp>();

        if (lightUp != null)
        {
            lightUp.SwitchOn();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded--;
            preJumpVelocity = AngularVelocity;
        }

        LightUp lightUp = collision.collider.GetComponent<LightUp>();

        if (lightUp != null)
        {
            lightUp.SwitchOff();
        }
    }
}
