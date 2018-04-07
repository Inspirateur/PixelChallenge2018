using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {



	public CharacterMovementController player;

    public GameObject ExplosionPrefab;

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
	private bool gameover;

	private Camera camera;

	public float time;
	private GameController gameController;



	// Use this for initialization
	void Start () {
		gameover = false;
		tempete = Tempete.getInstance ();
		initVariable ();
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		time = 0;

	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log(player.AngularVelocity);

		if(!gameover){
			time += Time.deltaTime;
			UpdateGame();
		} else {
			UpdateEndGame();
		}

	}

	private void UpdateGame(){
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

        if ((this.player.AngularVelocityMax * 0.98)<=this.getVitesseJoueur() && Time.time>timer){
			timer = Time.time+2;
			//Debug.Log ("END");
			skipLevel ();

		}    
        
        // set circles color
        for (int i = 0; i < Circles.Length; ++i)
        {
            if (i < currentCircle)
            {
                Color c = Color.HSVToRGB(((float)i + 1) / Circles.Length, 1, 1);
                Circles[i].CircleColor = c;
            }
            else if (i == currentCircle)
            {
                Color c = Color.HSVToRGB(((float)i) / Circles.Length + getVitesseJoueur() / player.Data.MaxSpeed / Circles.Length, 1, 1);
                Circles[i].CircleColor = c;
            } else
            {
                Color c = Color.HSVToRGB(((float)i) / Circles.Length, 1, 1);
                Circles[i].CircleColor = c;
            }
        }
	}

	private void UpdateEndGame(){
		
	}

	private void skipLevel(){

		// on passe au prochain cercle car il existe
		if(currentCircle + 1 < Circles.Length){
			tempete.startNextCercle ();
			
			Circles[currentCircle].ObjectNbr /= 4;
			Circles[currentCircle].Object = ExplosionPrefab;
			Circles[currentCircle].CleanWalls();

			modifierVitesseAngulaireMaxCouranteAcceleration();
			initVariable();
			currentCircle++;
		}
		else {
			endGameVictory();
		}
	}


	public float getVitesseJoueur(){
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
		augmentationNbVent *= 2.0f;
		diviseurAugmentationNbVent = player.AngularVelocityMax / augmentationNbVent;
		diviseurAugmentationVitesse = player.AngularVelocityMax / augmentationVitesse;
	}
	private void modifierVitesseAngulaireMaxCouranteAcceleration(){
		if(currentCircle + 1 < Circles.Length){
			float rayonPrecedent = Circles[currentCircle].ObjectDistance;
			float rayonSuivant = Circles[currentCircle+1].ObjectDistance;

			float difRayon = rayonSuivant - rayonPrecedent;

			float rapport = 1.0f + difRayon / rayonPrecedent;

			player.AngularVelocityMax /= rapport;
			player.AngularVelocityMax *= 1.3f;

			player.AccelerationMax /= rapport;
			
			player.AngularVelocity *= 0.2f;

			player.gameObject.GetComponent<Rigidbody2D>().AddForce(player.transform.up * -4.0f + player.transform.right * 2.0f, ForceMode2D.Impulse);
		}
	}

	public CircleGenerator getCircleCourant(){
		return Circles [currentCircle];
	}

	public Vector3 getPosPlayer(){
		return player.gameObject.transform.position;
	}

	public float getPercent(){
		return getVitesseJoueur () / player.AngularVelocityMax;
	}

	private void endGameVictory(){
		gameover = true;
		tempete.lancerGrosEclair();
		Circles[currentCircle].ObjectNbr /= 4;
		Circles[currentCircle].Object = ExplosionPrefab;
		Circles[currentCircle].CleanWalls();
		player.gameObject.GetComponent<Rigidbody2D>().AddForce(player.transform.up * -4.0f + player.transform.right * 2.0f, ForceMode2D.Impulse);
	}

	public void setScore(){
		gameController.submitPlayerScoring ((int)time); 
	}
   

}
