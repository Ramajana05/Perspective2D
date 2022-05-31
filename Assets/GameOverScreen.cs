using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    /// <summary>
    /// Diese Methode ist dazu da das Objekt zu aktivieren falls notwendig
    /// </summary>
    public void activate()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// Diese Methode ist dazu da um aus der Szene raus zu gehen wenn der Exit Button gedrueckt wird
    /// </summary>
    public void exitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
