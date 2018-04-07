﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{

    public CharacterMovementData Data;
    public float AngularVelocity = 0f;
    public float AngularVelocityMax;

    public CapsuleCollider2D StandUpCollider;
    public CapsuleCollider2D SlidingCollider;

    private Rigidbody2D rb;
    private Animator animator;

    private int grounded = 0;
    private bool isJumping = false;
    private bool isSliding = false;
    private float firstJumpTime;
    private float firstSlideTime;
    private float preJumpVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AngularVelocityMax = Data.MaxSpeed;
    }
	
	void FixedUpdate ()
    {

        if (Input.GetButtonDown("Jump") && grounded > 0 && !isJumping && !isSliding)
        {
            // first jump frame from the ground
            firstJumpTime = Time.time;
            isJumping = true;
            AngularVelocity *= Data.JumpVelocityLossCurve.Evaluate(0f);
            animator.SetBool("IsJumping", true);

            // basic impulse force
            rb.AddRelativeForce(Vector3.up * Data.JumpImpulseAcceleration * rb.mass, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Slide") && grounded > 0 && !isJumping && !isSliding)
        {
            firstSlideTime = Time.time;
            isSliding = true;
            SlidingCollider.enabled = true;
            StandUpCollider.enabled = false;
            animator.SetBool("IsSliding", true);
        }

        if (isSliding && (Input.GetButtonUp("Slide") || (Time.time - firstSlideTime > Data.SlideMaxDuration)))
        {
            isSliding = false;
            StandUpCollider.enabled = true;
            SlidingCollider.enabled = false;
            animator.SetBool("IsSliding", false);
        }

        // still in jump frames
        if (Input.GetButton("Jump") && isJumping && (Time.time - firstJumpTime) < Data.JumpButtonDuration)
        {
            AngularVelocity = preJumpVelocity * Data.JumpVelocityLossCurve.Evaluate((Time.time - firstJumpTime) / Data.JumpButtonDuration);
        }

        animator.SetFloat("VerticalVelocity", Vector3.Project(rb.velocity, transform.up).magnitude * Mathf.Sign(Vector3.Dot(rb.velocity, transform.up)));

        if (!(isSliding || isJumping))
        {
            AngularVelocity = Mathf.Min(AngularVelocityMax, AngularVelocity + Data.Acceleration * Time.deltaTime);
        }
        transform.RotateAround(Vector3.zero, Vector3.forward, AngularVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (isJumping && !(Input.GetButton("Jump") && (Time.time - firstJumpTime) < Data.JumpButtonDuration))
            {
                isJumping = false;
                animator.SetBool("IsJumping", false);
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
