using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{

    public GameObject PauseMenu;
    public GameObject Player;

    private Slider volumeSlider;
    private Text volumeValueText;

    void Start()
    {
        volumeSlider = PauseMenu.GetComponentInChildren<Slider>();
        volumeValueText = volumeSlider.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (PauseMenu.activeInHierarchy == false)
            {
                LoadPauseMenu();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void LoadPauseMenu()
    {
        // Display menu
        PauseMenu.SetActive(true);

        // Show cursor
        Cursor.visible = true;

        // Stop time
        Time.timeScale = 0;

        // Disable controls for the players
        Player.GetComponent<CharacterMovementController>().enabled = false;

        // Correctly load the volume slider
        volumeSlider.value = (int) (AudioListener.volume * 100.0f);
        volumeValueText.text = volumeSlider.value.ToString();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);

        // Hide cursor
        Cursor.visible = false;

        Time.timeScale = 1;

        Player.GetComponent<CharacterMovementController>().enabled = true;
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void UpdateVolume()
    {
        AudioListener.volume = volumeSlider.value / 100.0f;
        volumeValueText.text = volumeSlider.value.ToString();
    }
}
