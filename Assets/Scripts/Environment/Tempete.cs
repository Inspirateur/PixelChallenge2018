using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tempete : MonoBehaviour {

	[Header("Vent")]
	public Transform ventPrefab;

	[Header("Eclairs du background")]
	public Transform eclairPrefab;
	public float frequenceEclairMinInitial;
	public float frequenceEclairMaxInitial;
	public float ratioAugmentationFrequenceEclair;
	public AudioClip sonEclair;
	public float delaisMinEntreTonnerre;
	
	[Header("Eclairs du passage de niveau")]
	public Transform grosEclairPrefab;
	public AudioClip sonGrosEclair;

	private GameObject player;
	private static Tempete instance;
	private Rigidbody rb;
	private AudioSource audioSource;
	private int indiceCercleActuel;
	private float timerEclair;
	private float frequenceEclairMin;
	private float frequenceEclairMax;
	private float timerSonEclair;

	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player");
		instance = this;
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		indiceCercleActuel = 0;
		frequenceEclairMin = frequenceEclairMinInitial;
		frequenceEclairMax = frequenceEclairMaxInitial;
		timerEclair = Time.time + frequenceEclairMin;
	}

	// Use this for initialization
	void Start () {
		initNewCercle();
	}
	
	// Update is called once per frame
	void Update () {

		gererEclairBackground();

		if(Input.GetKeyUp(KeyCode.V)){
			ajouterVent();
		}

		if(Input.GetKeyUp(KeyCode.B)){
			supprimerVent();
		}

		if(Input.GetKeyUp(KeyCode.R)){
			augmenterVitesseRotation();
		}

		if(Input.GetKeyUp(KeyCode.T)){
			diminuerVitesseRotation();
		}

		if(Input.GetKeyUp(KeyCode.D)){
			resetTempete();
		}

		if(Input.GetKeyUp(KeyCode.S)){
			startNextCercle();
		}
	}

    public static Tempete getInstance(){
		return instance;
	}

	public void ajouterVent(){

		Vector3 toPlayer = (this.transform.position - player.transform.position).normalized;

		Quaternion quat = Quaternion.LookRotation((-toPlayer-2.0f*player.transform.right).normalized, Vector3.forward);

		Instantiate(ventPrefab, player.transform.position, quat, this.transform.GetChild(indiceCercleActuel));
	}

	public void augmenterVitesseRotation(){
		rb.AddTorque(0.0f, 0.0f, 5.0f, ForceMode.Acceleration);
	}

	public void supprimerVent(){
		Transform t = this.transform.GetChild(indiceCercleActuel);
		if(t.childCount > 0){
			Destroy(t.GetChild(t.childCount-1).gameObject);
		}
	}

	public void diminuerVitesseRotation(){
		rb.AddTorque(0.0f, 0.0f, -5.0f, ForceMode.Acceleration);
	}

	public void resetTempete(){
		rb.angularVelocity = new Vector3(0f,0f,0f);
		for(int i=this.transform.childCount-1; i>=0; i--){
			Destroy(this.transform.GetChild(i).gameObject);
		}
		indiceCercleActuel = 0;
		frequenceEclairMin = frequenceEclairMinInitial;
		frequenceEclairMax = frequenceEclairMaxInitial;
		initNewCercle();
	}

	public void startNextCercle(){
		indiceCercleActuel++;
//		player.transform.Translate(new Vector3(10.0f, 0.0f, 0.0f));
		initNewCercle();
		frequenceEclairMin *= 1.0f - ratioAugmentationFrequenceEclair;
		frequenceEclairMax *= 1.0f - ratioAugmentationFrequenceEclair;
		lancerGrosEclair();
	}

	private void initNewCercle(){
		GameObject cercle = new GameObject("Cercle_" + indiceCercleActuel);
		cercle.transform.position = this.transform.position;
		cercle.transform.parent = this.transform;
	}

	public void nextEclair(){

		if(Time.time >= timerSonEclair){
			audioSource.pitch = 1.2f + (Random.value - 0.5f) * 0.3f;
			audioSource.PlayOneShot(sonEclair, 0.1f);
			timerSonEclair = Time.time + delaisMinEntreTonnerre;
		}

		timerEclair = Time.time + frequenceEclairMin + (frequenceEclairMax - frequenceEclairMin) * Random.value;

		DigitalRuby.LightningBolt.LightningBoltScript eclair = 
			Instantiate(eclairPrefab, player.transform.position, Quaternion.identity, this.transform)
			.gameObject.GetComponent<DigitalRuby.LightningBolt.LightningBoltScript>();

		eclair.StartPosition = this.transform.position
			+ new Vector3(10.0f, 0.0f, 0.0f) * (Random.value - 0.5f)
			+ new Vector3(0.0f, 10.0f, 0.0f) * (Random.value - 0.5f);

		eclair.EndPosition =
			Quaternion.Euler(0.0f, 0.0f, 80.0f * (Random.value - 0.5f))
			* (player.transform.position + player.transform.forward * 10.0f * (Random.value - 0.5f));
	}

    private void gererEclairBackground()
    {
        if(Time.time >= timerEclair){
			nextEclair();
		}
    }

	private void lancerGrosEclair(){

		audioSource.pitch = 1.0f;
		audioSource.PlayOneShot(sonGrosEclair, 1.0f);

		DigitalRuby.LightningBolt.LightningBoltScript eclair = 
			Instantiate(grosEclairPrefab, player.transform.position, Quaternion.identity, this.transform)
			.gameObject.GetComponent<DigitalRuby.LightningBolt.LightningBoltScript>();

		eclair.StartPosition = this.transform.position;

		eclair.EndPosition = player.transform.position;
	}
}
