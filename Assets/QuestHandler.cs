using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestHandler : MonoBehaviour
{

    public static QuestHandler instance;
    public int QuestProgress;

    public Dialog[] dialog = new Dialog[18];

    /// <summary>
    /// Wird beim Start ausgeführt
    /// Setzt die static Instance und läd das ProgressLevel der Datenbank
    /// </summary>
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        ActivateStoryText();
        QuestProgress = GetComponent<LoadPlayer>().questProgress;      
    }

    /// <summary>
    /// Startet das Dialogfenster, falls es nicht schon offen ist. 
    /// Ansosten wird das Dialogfenster geschlossen
    /// </summary>
    public void ShowQuestText()
    {
        if (DialogManager.Instance.CheckActiveDialogbox())
        {
            DialogManager.Instance.ResetDialog();
        }
        else { 
            DialogManager.Instance.ShowDialog(dialog[QuestProgress]); 
        }        
    }

    /// <summary>
    /// Erhöht das QuestLevel und speichert die Änderungen in die Datenbank
    /// </summary>
    public void IncreaseProgress()
    {
        QuestProgress++;
        GetComponent<SavePlayer>().Save();    
    }

    /// <summary>
    /// Bereitet die Questtexte für die unterschiedlichen QuestLevel vor
    /// </summary>
    public void ActivateStoryText()
    {      
        for (int i = 0; i < dialog.Length; i++)
        {
            dialog[i].SetqQuestProgressRequirements(i);
            dialog[i].SetQuestProgressID(-1);
        }

        dialog[0].AddLine("Du hast aber lang geschlafen! Es ist bestimmt viel passiert! Schau dich doch etwas um. Mit (e oder dem kleinen Button unten rechts) kannst du mit anderen Menschen Interagieren.");
        dialog[1].AddLine("Irgendetwas ärgert die Dorfbewohner. Du solltest dich im Norden, auf der anderen Seite der Brücke wohl etwas umsehen.");
        dialog[2].AddLine("War das ein Minotaur? Schnell, du solltest jemandem davon erzählen!");
        dialog[3].AddLine("Mach dich auf den Weg in die nächste Stadt.");
        dialog[4].AddLine("Irgendetwas braut sich hier zusammen. Naja, egal. Begieb dich zurückj in dein Dorf. Da ist das Leben noch in Ordnung.");
        dialog[5].AddLine("Das war knapp. Schnell, schau du musst mit jemandem reden.");
        dialog[6].AddLine("Schau dich im Wald etwas um. Die Höle ist der einzige Weg in den Wald. Vielleicht findest du ja etwas neues heraus.");
        dialog[7].AddLine("Das ist eine wichtige Information! Du solltest das schnell Deinem Auftraggeber Melden! ");
        dialog[8].AddLine("Du sollst dich darum kümmern! Töte den Minotauren-Anführer! Wie auch immer du die Sache angehen möchtest... Leicht wird es bestimmt nicht.");
        dialog[9].AddLine("Oh, du hast es geschafft! Das war aber anstrengend. Du solltest nach Hause gehen...und etwas schlafen!");
    }
}
