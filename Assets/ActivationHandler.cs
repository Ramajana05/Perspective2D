using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ActivationHandler : MonoBehaviour
{
    private GameObject[] enemys;
    /// <summary>
    /// Wird einmal ausgef�hrt
    /// Setzt ben�tigte Variablen
    /// </summary>
    void Start()
    {
         enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }

    /// <summary>
    /// Pr�ft welcher Enemy aktiviert und welcher deaktiviert sein muss
    /// </summary>
    void Update()
    {
        try
        {
            foreach (GameObject enemy in enemys)
            {
                if (enemy.GetComponent<ProgressOnKill>()?.QuestProgresID == GameObject.FindGameObjectWithTag("Player").GetComponent<QuestHandler>().QuestProgress)
                {
                    enemy.SetActive(true);
                }
                else
                {
                    enemy.SetActive(false);
                }
            }
        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }
}
