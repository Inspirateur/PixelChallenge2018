using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neon : MonoBehaviour {
	public Color CircleColor
	{
		set
		{
			foreach (Transform t in transform)
			{
                if (t.tag == "Ground")
                {
                    SpriteRenderer rend = t.GetComponent<SpriteRenderer>();
                    if (rend != null) {
                        rend.material.SetColor("_Color", value);
                    }
                } else {
                    ParticleSystem sys = t.GetComponent<ParticleSystem>();
                    if (sys != null)
                    {
                        sys.GetComponent<Renderer>().material.SetColor("_Color", value);
                        sys.GetComponent<Renderer>().material.SetColor("_EmissionColor", 0.4f * value);
                    } else { 
                        Debug.Log(t.tag);
                        t.gameObject.GetComponent<Neon>().CircleColor = value;
                    }
                }
            }
		}
	}
}
