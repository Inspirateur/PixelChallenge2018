using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour {

	public PlayerScoring playerScoring;

	private string gameDataFileName = "data.json";



	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		playerScoring = new PlayerScoring();
		LoadGameData();



	}

	// Update is called once per frame
	void Update () {

	}

	public void submitPlayerScoring(int newScore){
		if(newScore > playerScoring.highScore)
		{
			playerScoring.highScore = newScore;
			SaveGameData ();
		}
	}

	public void LoadGameData(){
		string filePath = Application.dataPath + gameDataFileName;

		if(File.Exists(filePath))
		{

			string dataAsJson = File.ReadAllText(filePath); 

			PlayerScoring loadedData = JsonUtility.FromJson<PlayerScoring>(dataAsJson);

			playerScoring.highScore = loadedData.highScore;
			//Debug.Log (playerScoring.highScore);
		}
		else
		{
			string dataAsJson = JsonUtility.ToJson (playerScoring);
			File.WriteAllText (filePath, dataAsJson);
		}
	}

	private void SaveGameData()
	{

		string dataAsJson = JsonUtility.ToJson (playerScoring);
		string filePath = Application.dataPath + gameDataFileName;
		File.WriteAllText (filePath, dataAsJson);

	}


}
