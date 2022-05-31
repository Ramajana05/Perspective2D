using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuBackground;
    public GameObject pauseMenu;

    public static bool isPaused;

    /// <summary>
    /// Wird beim Start ausgef�hrt
    /// Setzt ben�tigte Variablen
    /// </summary>
    void Start()
    {
        pauseMenu.SetActive(false);        
        isPaused = false;
    }

    /// <summary>
    /// Wird f�r jeden Frame ausgef�hrt
    /// Pr�ft ob Spiel pausiert ist
    /// </summary>
    void Update()
    {            
        ShowPauseScreen();

        if (Input.GetKeyDown(KeyCode.Escape) && (Application.platform != RuntimePlatform.Android))
        {
            FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
            if (isPaused)
            {
                isPaused = false;
            }
            else
            {
                isPaused = true;
            }
        }
    }

    /// <summary>
    /// Pr�ft ob Spiel pausiert ist
    /// F�hrt Methoden zum pausieren/pause beenden aus
    /// </summary>
    public void ShowPauseScreen()
    {
        if (isPaused != true)
        {
            ResumeGame();       
        }
        else
        {
            PauseGame();        
        }
    }

    /// <summary>
    /// Toggle f�r den Pause Screen
    /// </summary>
    public void TogglePauseMenu()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        if (Application.platform == RuntimePlatform.Android)
        {
            if (isPaused)
            {
                isPaused = false;
            }
            else
            {
                isPaused = true;
            }
        }
    }

    /// <summary>
    /// Pausiert das Spiel
    /// </summary>
    public void PauseGame()
    {     
        pauseMenuBackground.SetActive(true);
        pauseMenu.SetActive(true);        
        Time.timeScale = 0f;       
    }

    /// <summary>
    /// Beendet die Pause
    /// </summary>
    public void ResumeGame()
    {
        pauseMenuBackground.SetActive(false);
        pauseMenu.SetActive(false);        
        Time.timeScale = 1f;    
    }

    /// <summary>
    /// Wechselt in das Hauptmen�
    /// Setzt Variabled damit das Hauptmen� funktioniert
    /// </summary>
    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Spiel schlie�en
    /// </summary>
    public void Quit()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        Application.Quit();
    }
}
