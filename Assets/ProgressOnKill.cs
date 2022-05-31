using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressOnKill : MonoBehaviour
{
    public int QuestProgresID;

    /// <summary>
    /// Wird das Object zerstört und hat der Spieler das benötigte QuestLevel, erhält er ein QuestLevel für den Kill
    /// </summary>
    void OnDestroy()
    {
        try
        {
            if (QuestProgresID == GameObject.FindGameObjectWithTag("Player").GetComponent<QuestHandler>().QuestProgress)
            {               
                GameObject.FindGameObjectWithTag("Player").GetComponent<QuestHandler>().IncreaseProgress();
            }
        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }
}
