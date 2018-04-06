using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{

    public CustomGravityData GravityData;
    
    private Rigidbody2D rb;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        Vector3 centerVector = (new Vector3(GravityData.Center.x, GravityData.Center.y) - transform.position).normalized;

        Vector2 forceToApply = centerVector * GravityData.Force * rb.mass;
        if (!GravityData.IsAttraction)
        {
            forceToApply *= -1f;
        }

        rb.AddForce(forceToApply);

        if (GravityData.IsRotatedTowardsCenter)
        {
            transform.Rotate(Vector3.forward, Vector2.SignedAngle(transform.up, centerVector));
        }
	}
}
