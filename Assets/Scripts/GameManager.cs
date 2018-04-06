using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public CharacterMovementController player;

    public CircleGenerator[] Circles;
    private int currentCircle = 0;

	public float magnitudeAugmentationAChaqueNiveau;
	private float magnitudeVitesseObjectif;
	private float magnitudeVitessePrecedent;
	private float ecartVitesseDebutAObjectif;

	private Tempete tempete;

	public float augmentationNbVent;
	public float augmentationVitesse;

	private float diviseurAugmentationNbVent;
	private float diviseurAugmentationVitesse;

	private float diviseurActuelAugmentationNbVent;
	private float diviseurActuelAugmentationVitesse;

	private int compteurActuelNbVent;
	private int compteurActuelVitesse;



	// Use this for initialization
	void Start () {
		tempete = Tempete.getInstance ();
		magnitudeVitesseObjectif = 0;
		initVariable ();
	}
	
	// Update is called once per frame
	void Update () {


		if(getVitesseJoueur()>magnitudeVitessePrecedent){
			//Debug.Log ("up");
			float variation = getVitesseJoueur () - magnitudeVitessePrecedent;

			diviseurActuelAugmentationNbVent += (variation / diviseurAugmentationNbVent);
			spawnVent ((int)diviseurActuelAugmentationNbVent);
			diviseurActuelAugmentationNbVent -= (int)diviseurActuelAugmentationNbVent;

			diviseurActuelAugmentationVitesse += (variation / diviseurAugmentationVitesse);
			addVitesseVent ((int)diviseurActuelAugmentationVitesse);
			diviseurActuelAugmentationVitesse -= (int)diviseurActuelAugmentationVitesse;


			magnitudeVitessePrecedent = getVitesseJoueur ();

            Color c = Color.HSVToRGB(getVitesseJoueur() / magnitudeVitesseObjectif, 1, 1);
            Circles[currentCircle].CircleColor = c;
        }

		if(this.magnitudeVitesseObjectif<=this.getVitesseJoueur()){
			skipLevel ();
		}

	}

	private void skipLevel(){
		Debug.Log ("SkipLevel");
		tempete.startNextCercle ();
		initVariable ();
        Circles[currentCircle].gameObject.SetActive(false);
        currentCircle++;
	}


	private float getVitesseJoueur(){
		return player.AngularVelocity;
	}

	private void spawnVent(int nb){
		compteurActuelNbVent+=nb;
		if(compteurActuelNbVent>augmentationNbVent){
			nb -= compteurActuelNbVent - (int)augmentationNbVent;
		}
		//Debug.Log ("Augmentation Nombre vent"+nb);
		for(int i=0;i<nb;i++){
			tempete.ajouterVent ();
		}
	}

	private void addVitesseVent(int nb){
		compteurActuelVitesse+=nb;
		if(compteurActuelNbVent>augmentationNbVent){
			nb -= compteurActuelVitesse - (int)augmentationVitesse;
		}
		//Debug.Log ("Augmentation Vitesse Vent"+nb);
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
