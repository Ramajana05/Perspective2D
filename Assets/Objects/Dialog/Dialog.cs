using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Dialog
{
    [SerializeField] List<string> lines;
    [SerializeField] int questProgressRequirements;
    [SerializeField] int questProgressID;

    public List<string> Lines
    {
        get { return lines; }
    }
    public int QuestProgressRequirements
    {
        get { return questProgressRequirements; }
    }

    public int QuestProgressID
    {
        get { return questProgressID; }
    }

    public void AddLine(string text)
    {
        lines.Add(text);
    }

    public void SetqQuestProgressRequirements(int i)
    {
        questProgressRequirements = i;
    }

    public void SetQuestProgressID(int i)
    {
        questProgressID = i;
    }
}
