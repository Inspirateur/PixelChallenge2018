using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float maginitudeVitesseJoueurTemp;

	public float magnitudeAugmentationAChaqueNiveau;
	public float magnitudeVitesseObjectif;
	public float magnitudeVitessePrecedent;
	public float ecartVitesseDebutAObjectif;

	private Tempete tempete;

	public float augmentationNbVent;
	public float augmentationVitesse;

	public float diviseurAugmentationNbVent;
	public float diviseurAugmentationVitesse;

	public float diviseurActuelAugmentationNbVent;
	public float diviseurActuelAugmentationVitesse;

	public int compteurActuelNbVent;
	public int compteurActuelVitesse;



	// Use this for initialization
	void Start () {
		tempete = Tempete.getInstance ();
		magnitudeVitesseObjectif = 0;
		initVariable ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.U)) {
			maginitudeVitesseJoueurTemp +=0.1f;
		}

		if(getVitesseJoueur()>magnitudeVitessePrecedent){
			Debug.Log ("up");
			float variation = getVitesseJoueur () - magnitudeVitessePrecedent;

			diviseurActuelAugmentationNbVent += (variation / diviseurAugmentationNbVent);
			spawnVent ((int)diviseurActuelAugmentationNbVent);
			diviseurActuelAugmentationNbVent -= (int)diviseurActuelAugmentationNbVent;

			diviseurActuelAugmentationVitesse += (variation / diviseurAugmentationVitesse);
			addVitesseVent ((int)diviseurActuelAugmentationVitesse);
			diviseurActuelAugmentationVitesse -= (int)diviseurActuelAugmentationVitesse;


			magnitudeVitessePrecedent = getVitesseJoueur ();
		}

		if(this.magnitudeVitesseObjectif<=this.getVitesseJoueur()){
			skipLevel ();
		}

	}

	private void skipLevel(){
		Debug.Log ("SkipLevel");
		tempete.startNextCercle ();
		initVariable ();

	}


	private float getVitesseJoueur(){
		return maginitudeVitesseJoueurTemp;
	}

	private void spawnVent(int nb){
		compteurActuelNbVent+=nb;
		if(compteurActuelNbVent>augmentationNbVent){
			nb -= compteurActuelNbVent - (int)augmentationNbVent;
		}
		Debug.Log ("dddddd"+nb);
		for(int i=0;i<nb;i++){
			tempete.ajouterVent ();
		}
	}

	private void addVitesseVent(int nb){
		compteurActuelVitesse+=nb;
		if(compteurActuelNbVent>augmentationNbVent){
			nb -= compteurActuelVitesse - (int)augmentationVitesse;
		}

		for(int i=0;i<nb;i++){
			tempete.augmenterVitesseRotation ();
		}
	}

	private void initVariable(){
		compteurActuelNbVent=0;
		compteurActuelVitesse = 0;
		magnitudeVitessePrecedent = magnitudeVitesseObjectif;
		this.magnitudeVitesseObjectif = magnitudeVitessePrecedent + magnitudeAugmentationAChaqueNiveau;
		this.ecartVitesseDebutAObjectif = this.magnitudeVitesseObjectif - magnitudeVitessePrecedent;
		diviseurAugmentationNbVent = ecartVitesseDebutAObjectif / augmentationNbVent;
		diviseurAugmentationVitesse = ecartVitesseDebutAObjectif / augmentationVitesse;
	}


}
