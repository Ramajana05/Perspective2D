using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public float wait_time = 28f;

    /// <summary>
    /// Wird beim Start ausgeführt
    /// Startet einen timer für die Cutscene
    /// </summary>
    void Start()
    {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// Setzt den timer für die Cutscene
    /// Wechselt die Szene nach der Cutscene
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait_time);

        SceneManager.LoadScene("EndScreen");
    }
}
