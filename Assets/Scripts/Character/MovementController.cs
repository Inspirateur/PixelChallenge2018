using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public CharacterMovementData MovementData;

    private Rigidbody2D rb;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        float accelerationFactor = MovementData.AccelerationCurve.Evaluate(rb.velocity.magnitude / MovementData.MaxSpeed);
        rb.velocity += new Vector2(transform.right.x, transform.right.y) * MovementData.Acceleration * accelerationFactor * Time.deltaTime;
        Debug.Log(rb.velocity.magnitude + " ; " + accelerationFactor);
	}
}
