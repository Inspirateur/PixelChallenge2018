using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject MainMenuContainer;
    public GameObject CreditsMenuContainer;

    public void OnStartGamePressed()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void OnCreditsPressed()
    {
        MainMenuContainer.SetActive(false);
        CreditsMenuContainer.SetActive(true);
    }

    public void OnBackToMainMenuPressed()
    {
        CreditsMenuContainer.SetActive(false);
        MainMenuContainer.SetActive(true);
    }

    public void OnExitPressed()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
