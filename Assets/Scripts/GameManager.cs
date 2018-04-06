using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float maginitudeVitesseJoueurTemp;

	public float magnitudeAugmentationAChaqueNiveau;
	private float magnitudeVitesseObjectif;

	private Tempete tempete;

	// Use this for initialization
	void Start () {
		tempete = Tempete.getInstance ();
		skipLevel ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.U)) {
			maginitudeVitesseJoueurTemp++;
		}

		if(this.magnitudeVitesseObjectif<=this.getVitesseJoueur()){
			skipLevel ();
		}
	}

	private void skipLevel(){
		Debug.Log ("SkipLevel");
		this.magnitudeVitesseObjectif = getVitesseJoueur () + magnitudeAugmentationAChaqueNiveau;
	}


	private float getVitesseJoueur(){
		return maginitudeVitesseJoueurTemp;
	}
}
