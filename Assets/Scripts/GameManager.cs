using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public CharacterMovementController player;

    public CircleGenerator[] Circles;
    private int currentCircle = 0;

	private float magnitudeVitessePrecedent;

	private Tempete tempete;

	public float augmentationNbVent;
	public float augmentationVitesse;

	private float diviseurAugmentationNbVent;
	private float diviseurAugmentationVitesse;

	private float diviseurActuelAugmentationNbVent;
	private float diviseurActuelAugmentationVitesse;

	private int compteurActuelNbVent;
	private int compteurActuelVitesse;

	private float timer;



	// Use this for initialization
	void Start () {
		tempete = Tempete.getInstance ();
		initVariable ();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentCircle <2){

			Debug.Log (player.AngularVelocity);

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


			}

			//Color c = Color.HSVToRGB(getVitesseJoueur() / player.Data.MaxSpeed, 1, 1);
			//Circles[currentCircle].CircleColor = c;

			if((this.player.Data.MaxSpeed * 0.98)<=this.getVitesseJoueur() && Time.time>timer){
				timer = Time.time+2;
				Debug.Log ("END");
				skipLevel ();

			}
		}


	}

	private void skipLevel(){
		player.AngularVelocity = 0.2f;
		tempete.startNextCercle ();
		initVariable ();
        Circles[currentCircle].gameObject.SetActive(false);
        currentCircle++;
	}


	private float getVitesseJoueur(){
		return player.AngularVelocity;
	}


	//ajout du vent
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

	//ajout de la vitesse du vent
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

	//rénitialisation des variables
	private void initVariable(){
		compteurActuelNbVent=0;
		compteurActuelVitesse = 0;
		magnitudeVitessePrecedent = 0;
		diviseurAugmentationNbVent = this.player.Data.MaxSpeed / augmentationNbVent;
		diviseurAugmentationVitesse = this.player.Data.MaxSpeed / augmentationVitesse;
	}


}
