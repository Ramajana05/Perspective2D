using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressOnKill : MonoBehaviour
{
    public int QuestProgresID;

    /// <summary>
    /// Wird das Object zerst�rt und hat der Spieler das ben�tigte QuestLevel, erh�lt er ein QuestLevel f�r den Kill
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
