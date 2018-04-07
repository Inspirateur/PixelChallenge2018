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
				SpriteRenderer rend = t.GetComponent<SpriteRenderer>();
				if (rend != null)
				{
					rend.material.SetColor("_Color", value);
				}
				else{
					ParticleSystem sys = t.GetComponent<ParticleSystem>();
					if (sys != null)
					{
						sys.GetComponent<Renderer>().material.SetColor("_Color", value);
					}
				}
				if (t.tag != "Ground")
				{
					t.GetComponent<Neon>().CircleColor = value;
				}
			}
		}
	}
}
