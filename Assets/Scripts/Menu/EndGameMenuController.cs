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
            GetComponentInChildren<CanvasRenderer>().SetAlpha((Time.time - displayTime) / FadeInDuration);
        }
	}

    public void DisplayWinMenu(float timeSpent, float bestTime)
    {
        WinMenuContainer.SetActive(true);
        CurrentTimeText.GetComponent<TextMeshProUGUI>().text = "Your time: " + ((int) timeSpent / 60).ToString()
            + " minutes and " + (timeSpent % 60) + " seconds";
        BestTimeText.GetComponent<TextMeshProUGUI>().text = "Best time: " + ((int) bestTime / 60).ToString()
            + " minutes and " + (bestTime % 60) + " seconds";
        DisplayMenu();
    }

    public void DisplayLossMenu()
    {
        LossMenuContainer.SetActive(true);
        DisplayMenu();
    }

    public void DisplayMenu()
    {
        GetComponentInChildren<CanvasRenderer>().SetAlpha(0f);
        displayTime = Time.time;
        isDisplayed = true;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
