using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenuController : MonoBehaviour
{

    public GameObject WinMenuContainer;
    public GameObject LossMenuContainer;

    public GameObject CurrentTimeText;
    public GameObject BestTimeText;

    public float FadeInDuration = 2.0f;

    private bool isDisplayed = false;
    private float displayTime;

    void Start ()
    {
    }
	
	void Update ()
    {
        if (isDisplayed && Time.time < displayTime + FadeInDuration)
        {
            GetComponentInChildren<CanvasGroup>().alpha = (Time.time - displayTime) / FadeInDuration;
        }
	}

    public void DisplayWinMenu(float timeSpent, float bestTime)
    {
        WinMenuContainer.SetActive(true);
        CurrentTimeText.GetComponent<TextMeshProUGUI>().text = "Your time: " + ((int) timeSpent / 60).ToString()
            + " minutes, " + Mathf.Floor(timeSpent % 60).ToString() 
            + " seconds, and " + Mathf.Floor((timeSpent - Mathf.Floor(timeSpent)) * 1000.0f).ToString() + " milliseconds";
        BestTimeText.GetComponent<TextMeshProUGUI>().text = "Best time: " + ((int) bestTime / 60).ToString()
            + " minutes, " + Mathf.Floor(bestTime % 60).ToString()
            + " seconds, and " + Mathf.Floor((bestTime - Mathf.Floor(bestTime)) * 1000.0f).ToString() + " milliseconds";
        DisplayMenu();
    }

    public void DisplayLossMenu()
    {
        LossMenuContainer.SetActive(true);
        DisplayMenu();
    }

    public void DisplayMenu()
    {
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GetComponentInChildren<CanvasGroup>().alpha = 0f;
        displayTime = Time.time;
        isDisplayed = true;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
