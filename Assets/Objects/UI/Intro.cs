using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float wait_time = 8f;

    /// <summary>
    /// Wird beim Start ausgef�hrt
    /// Startet einen timer f�r das Intro
    /// </summary>
    void Start()
    {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// Setzt den timer f�r das Intro
    /// Wechselt die Szene nach dem Intro
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait_time);

        SceneManager.LoadScene("MainMenu");
    }
}
