using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	private GameManager gm;

	private float velocity = 0.0f ;
	private Vector3 velocityEnd = Vector3.zero ;
	private float distanceFocusDesiree;
	private float initial;
    private Vector3 posInitial;
    private Quaternion initRot;

    public bool FollowPlayer = false;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		initial = this.transform.position.z;
		posInitial = this.transform.position;
        posInitial.x = 0;
        posInitial.y = 0;
        initRot = transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate(){
		
		if(!gm.gameover){
			UpdateGame();
		} else {
			UpdateEndGame();
		}
	}

	private void UpdateGame(){
        if (FollowPlayer)
        {
            transform.up = Vector3.up;
            distanceFocusDesiree = (gm.getCircleCourant().ObjectDistance * 0.5f) + gm.getPosPlayer().z;

            Vector3 pos = this.transform.position;

            pos.z = Mathf.SmoothDamp(this.transform.position.z, gm.getPercent() * (-distanceFocusDesiree) + initial, ref velocity, 0.3f);

            this.transform.position = pos;
            Vector3 rot = this.transform.eulerAngles;
            rot.z = 0;
            this.transform.rotation = Quaternion.Euler(rot);
        }
        else
        {
            transform.position = posInitial;
            transform.rotation = initRot;
        }
	}

	private void UpdateEndGame(){
		transform.up = Vector3.up;
		distanceFocusDesiree = (gm.getCircleCourant ().ObjectDistance * 4.0f);
	
		Vector3 posDesire = Vector3.zero;
		posDesire.z -= distanceFocusDesiree + initial;

		Vector3 pos = Vector3.SmoothDamp (this.transform.position, posDesire, ref velocityEnd, 1.0f);

		this.transform.position = pos;
	}
}