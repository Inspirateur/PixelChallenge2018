using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{

    public CustomGravityData GravityData;
    
    private Rigidbody2D rb;
    
	void Start ()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        Vector2 forceToApply = transform.position * GravityData.ForceValue * rb.mass * Time.deltaTime;
        if (GravityData.IsAttraction)
        {
            forceToApply *= -1f;
        }

        rb.AddForce(forceToApply);

        if (GravityData.IsRotatedTowardsCenter)
        {
            transform.Rotate(Vector3.forward, Vector3.SignedAngle(transform.up, -1f * transform.position, Vector3.forward));
        }
	}
}
