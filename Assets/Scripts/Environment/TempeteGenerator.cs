using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempeteGenerator : MonoBehaviour {

	public int nombreCercle = 5;
	public int incrementVentParCercle = 20;
	public float vitesseRotation = 75.0f;

	private Tempete tempete;

	// Use this for initialization
	void Start () {
		tempete = Tempete.getInstance();
		tempete.setVitesseRotation(vitesseRotation);
		for(int numCercle=0; numCercle<nombreCercle; numCercle++){
			for(int numVent=0; numVent<incrementVentParCercle*(numCercle+1); numVent++){
				tempete.ajouterVentRandom();
			}
			tempete.startNextCercle();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
