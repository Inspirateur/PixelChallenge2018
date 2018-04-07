using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	private GameManager gm;

	private float velocity = 0.0f ;
	private float distanceFocusDesiree;
	private float initial;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		initial = this.transform.position.z;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate(){
		transform.up = Vector3.up;
		distanceFocusDesiree = (gm.getCircleCourant ().ObjectDistance * 0.5f) + gm.getPosPlayer().z;
	
		Vector3 pos = this.transform.position;

		pos.z = Mathf.SmoothDamp (this.transform.position.z, gm.getPercent () * (-distanceFocusDesiree) + initial, ref velocity, 0.3f);

		this.transform.position = pos;

	}


}

//Camera.main.transform.position = Vector3.SmoothDamp (Camera.main.transform.position, pos, ref velocity, 0.01f,dureeAcces);