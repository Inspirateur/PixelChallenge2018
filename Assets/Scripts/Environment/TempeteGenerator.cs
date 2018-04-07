using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempeteGenerator : MonoBehaviour {

	public int nombreCercle = 5;
	public int incrementVentParCercle = 20;
	public float vitesseRotation = 750.0f;

	private Tempete tempete;
	private int numCercle;
	private int numVent;
	private int maxVent;

	// Use this for initialization
	void Start () {

		tempete = Tempete.getInstance();
		tempete.setVitesseRotation(vitesseRotation);
		numCercle=0;
		numVent=0;
		maxVent = incrementVentParCercle*(numCercle+1);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(numVent<maxVent){
			tempete.ajouterVentRandom();
			numVent++;
		} else if(numCercle < nombreCercle){
			tempete.startNextCercleScripte();
			numCercle++;
			numVent = 0;
			maxVent = incrementVentParCercle*(numCercle+1);
		}
	}
}
