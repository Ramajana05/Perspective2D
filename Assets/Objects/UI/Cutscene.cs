using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public float wait_time = 28f;

    /// <summary>
    /// Wird beim Start ausgef�hrt
    /// Startet einen timer f�r die Cutscene
    /// </summary>
    void Start()
    {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// Setzt den timer f�r die Cutscene
    /// Wechselt die Szene nach der Cutscene
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait_time);

        SceneManager.LoadScene("EndScreen");
    }
}
