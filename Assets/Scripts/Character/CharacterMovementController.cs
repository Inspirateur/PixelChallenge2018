using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{

    public CharacterMovementData Data;
    public CustomGravityData GravityData;
    [HideInInspector]
    public float AngularVelocity = 0.0f;
    [HideInInspector]
    public float AngularVelocityMax;
    [HideInInspector]
    public float AccelerationMax;

    public LightFlickerData flickering;
    public CapsuleCollider2D StandUpCollider;
    public CapsuleCollider2D SlidingCollider;
    public LightFlicker[] bodyParts;
    private float secondesIntouchable = 1.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private GameManager gm;

    private int grounded = 0;
    private bool isJumping = false;
    private bool isSliding = false;
    private float timerFinSlide;
    private float timeEndInv;
    private LightFlickerData normal_flicker_save;
    private float timerNextJump;
    private float velocityAvantSlide;
    private bool comboEnable;

    private void Awake()
    {
        comboEnable = true;
        normal_flicker_save = bodyParts[0].Data;
        timeEndInv = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AngularVelocityMax = Data.MaxSpeed;
        AccelerationMax = Data.Acceleration;
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
    }
	
	void FixedUpdate ()
    {
        if(!gm.gameover){
            FixedUpdateGame();
        } else if(gm.victory){
            FixedUpdateEndGameVictory();
        } else {
            FixedUpdateEndGameLose();
        }
        if(timeEndInv < Time.time)
        {
            foreach(LightFlicker bodypart in bodyParts) {  
                bodypart.Data = normal_flicker_save;
            }
        }
    }

    public void HitWall()
    {
        if (timeEndInv < Time.time) {
            invulnerable();
            AngularVelocity -= 2 / transform.position.magnitude;
        }

        AudioSource.PlayClipAtPoint(Data.HitSound, transform.position);
    }

    public void invulnerable(){
        timeEndInv = Time.time + secondesIntouchable;
        foreach (LightFlicker bodypart in bodyParts)
        {
            bodypart.Data = flickering;
            bodypart.forceOff();
        }
    }

    private void FixedUpdateGame(){
        
        if (Input.GetButton("Jump") && comboEnable)
        {
            if(grounded > 0 && !isJumping && !isSliding)
            {
                AudioSource.PlayClipAtPoint(Data.JumpSound, transform.position);

                isJumping = true;
                animator.SetBool("IsJumping", true);
                
                rb.AddRelativeForce(Vector3.up * Data.JumpImpulseAcceleration * rb.mass, ForceMode2D.Impulse);
                timerNextJump = Time.fixedTime + 0.2f;
            }
            else if(grounded > 0 && !isJumping && isSliding && comboEnable){
                comboEnable = false;
                isSliding = false;
                StandUpCollider.enabled = true;
                SlidingCollider.enabled = false;
                animator.SetBool("IsSliding", false);

                AudioSource.PlayClipAtPoint(Data.JumpSound, transform.position);
                
                isJumping = true;
                animator.SetBool("IsJumping", true);
                
                rb.AddRelativeForce(Vector3.up * Data.JumpImpulseAcceleration * rb.mass, ForceMode2D.Impulse);
                timerNextJump = Time.fixedTime + 0.2f;
                animator.Play("decolage");
            }
        }
        

        if (Input.GetButton("Slide") && comboEnable)
        {

            if(grounded > 0 && !isJumping && !isSliding)
            {
                AudioSource.PlayClipAtPoint(Data.SlideSound, transform.position);

                rb.velocity *= 0.8f;

                velocityAvantSlide = AngularVelocity;
                AngularVelocity = Mathf.Max((AngularVelocityMax * 0.9f), AngularVelocity);

                timerFinSlide = Time.time + Data.SlideMaxDuration;
                isSliding = true;
                SlidingCollider.enabled = true;
                StandUpCollider.enabled = false;
                animator.SetBool("IsSliding", true);
            }
            else if(isJumping && !isSliding && comboEnable)
            {
                comboEnable = false;
                AudioSource.PlayClipAtPoint(Data.SlideSound, transform.position);

                rb.velocity = Vector2.zero;

                velocityAvantSlide = AngularVelocity;
                AngularVelocity = Mathf.Max((AngularVelocityMax * 0.9f), AngularVelocity);

                timerFinSlide = Time.time + Data.SlideMaxDuration;
                isSliding = true;
                isJumping = false;
                SlidingCollider.enabled = true;
                StandUpCollider.enabled = false;
                animator.SetBool("IsSliding", true);
                animator.SetBool("IsJumping", false);
                animator.Play("slide");
            }

        }

        if(isSliding)
        {
            Vector3 exteriorVector = (transform.position - new Vector3(GravityData.Center.x, GravityData.Center.y)).normalized;

            Vector2 forceToApply = exteriorVector * GravityData.Force * rb.mass;
            if (!GravityData.IsAttraction || gm.gameover)
            {
                forceToApply *= -1f;
            }

            rb.AddForce(forceToApply);
        }

        if (isSliding && Time.time >= timerFinSlide)
        {
            isSliding = false;
            StandUpCollider.enabled = true;
            SlidingCollider.enabled = false;
            animator.SetBool("IsSliding", false);
            AngularVelocity = velocityAvantSlide;
        }

        animator.SetFloat("VerticalVelocity", Vector3.Project(rb.velocity, transform.up).magnitude * Mathf.Sign(Vector3.Dot(rb.velocity, transform.up)));
        animator.SetFloat("RunningSpeed", AngularVelocity * transform.position.magnitude / 2.5f);

        if (!(isSliding || isJumping))
        {
            AngularVelocity =
                Mathf.Min(AngularVelocityMax,
                AngularVelocity + AccelerationMax * Data.AccelerationFactorOverSpeed.Evaluate(AngularVelocity / AngularVelocityMax) * Time.deltaTime);
        }
        transform.RotateAround(Vector3.zero, Vector3.forward, AngularVelocity);
    }

    private void FixedUpdateEndGameVictory(){
        
        
    }

    private void FixedUpdateEndGameLose(){
        
        transform.RotateAround(Vector3.zero, Vector3.forward, 2.0f*Mathf.PI/3.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (isJumping && Time.fixedTime >= timerNextJump)
            {
                isJumping = false;
                comboEnable = true;
                animator.SetBool("IsJumping", false);
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
        }

        LightUp lightUp = collision.collider.GetComponent<LightUp>();

        if (lightUp != null)
        {
            lightUp.SwitchOff();
        }
    }
}
