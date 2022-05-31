
using System.Collections.Generic;
using UnityEngine;

public class CustomeCharacter : MonoBehaviour
{
    //AnimatedOption
    public GameObject CurrentBodyPart;
    public List<GameObject> OptionsListHair = new List<GameObject>();
    public List<GameObject> OptionsListEyes = new List<GameObject>();
    public List<GameObject> OptionsListBody = new List<GameObject>();

    private int i;
    private int j;
    private int k;

    public string outfit;
    public string eyes;
    public string hair;

    /// <summary>
    /// Der zähler der pro Pfeil-Klick durch die Hair-Liste hoch zählt.
    /// Und demetprechend das Gameobject, bei dem es sich grade befindet, auf visible setzt.
    /// </summary>
    public void AnimatedNextOptionHair()
    {
        SetAllOnFalseHair();
        i++;

        if (i >= OptionsListHair.Count)
        {
            i = 0; //Reset if cycled though entire lifetime
        }

        CurrentBodyPart = OptionsListHair[i].gameObject;
        OptionsListHair[i].SetActive(true);
    }
    /// <summary>
	/// Der zähler der pro Pfeil-Klick durch die Hair-Liste runter zählt.
    /// Und demetprechend das Gameobject, bei dem es sich grade befindet, auf visible setzt.
	/// </summary>
    public void AnimatedPreviouseOptionHair()
    {
        SetAllOnFalseHair();
        i--;

        if (i <= -1)
        {
            i = OptionsListHair.Count - 1; ; //Reset if cycled though entire lifetime
        }

        CurrentBodyPart = OptionsListHair[i].gameObject;
        OptionsListHair[i].SetActive(true);
    }
    /// <summary>
	/// Der zähler der pro Pfeil-Klick durch die Eyes-Liste hoch zählt.
    /// Und demetprechend das Gameobject, bei dem es sich grade befindet, auf visible setzt.
	/// </summary>
    public void AnimatedNextOptionEyes()
    {
        SetAllOnFalseEyes();
        j++;

        if (j >= OptionsListEyes.Count)
        {
            j = 0; //Reset if cycled though entire lifetime
        }

        CurrentBodyPart = OptionsListEyes[j].gameObject;
        OptionsListEyes[j].SetActive(true);
    }
    /// <summary>
	/// Der zähler der pro Pfeil-Klick durch die Eyes-Liste runter zählt.
    /// Und demetprechend das Gameobject, bei dem es sich grade befindet, auf visible setzt.
	/// </summary>
    public void AnimatedPreviouseOptionEyes()
    {
        SetAllOnFalseEyes();
        j--;

        if (j <= -1)
        {
            j = OptionsListEyes.Count - 1; ; //Reset if cycled though entire lifetime
        }

        CurrentBodyPart = OptionsListEyes[j].gameObject;
        OptionsListEyes[j].SetActive(true);
    }
    /// <summary>
	/// Der zähler der pro Pfeil-Klick durch die Body-Liste hoch zählt.
    /// Und demetprechend das Gameobject, bei dem es sich grade befindet, auf visible setzt.
	/// </summary>
    public void AnimatedNextOptionBody()
    {
        SetAllOnFalseBody();
        k++;

        if (k >= OptionsListBody.Count)
        {
            k = 0; //Reset if cycled though entire lifetime
        }

        CurrentBodyPart = OptionsListBody[k].gameObject;
        OptionsListBody[k].SetActive(true);

    }
    /// <summary>
	/// Der zähler der pro Pfeil-Klick durch die Body-Liste runter zählt.
    /// Und demetprechend das Gameobject, bei dem es sich grade befindet, auf visible setzt.
	/// </summary>
    public void AnimatedPreviouseOptionBody()
    {
        SetAllOnFalseBody();
        k--;

        if (k <= -1)
        {
            k = OptionsListBody.Count - 1; ; //Reset if cycled though entire lifetime 
        }

        CurrentBodyPart = OptionsListBody[k].gameObject;
        OptionsListBody[k].SetActive(true);
    }
    /// <summary>
	/// Die Methode setzt alle Hair-Objects auf invisible.
	/// </summary>
    public void SetAllOnFalseHair()
    {
        OptionsListHair[0].SetActive(false);
        OptionsListHair[1].SetActive(false);
        OptionsListHair[2].SetActive(false);

    }
    /// <summary>
	/// Die Methode setzt alle Eyes-Objects auf invisible.
	/// </summary>
    public void SetAllOnFalseEyes()
    {
        OptionsListEyes[0].SetActive(false);
        OptionsListEyes[1].SetActive(false);

    }
    /// <summary>
	/// Die Methode setzt alle Body-Objects auf invisible.
	/// </summary>
    public void SetAllOnFalseBody()
    {
        OptionsListBody[0].SetActive(false);
        OptionsListBody[1].SetActive(false);
        OptionsListBody[2].SetActive(false);
        OptionsListBody[3].SetActive(false);
    }
    /// <summary>
	/// Schaut welches Body-GameObject visible ist und schickt eine Meldung and die Console.
    /// Wurde für Testzwecke benutzt.
	/// </summary>
    public void CheckWhichGameObjectIsVisibleBody()
    {
        if (OptionsListBody[0].activeSelf)
        {
            Debug.Log("ArmorOutfit");
        }
        else if (OptionsListBody[1].activeSelf)
        {
            Debug.Log("RetroOutfit");
        }
        else if (OptionsListBody[2].activeSelf)
        {
            Debug.Log("BrownOutfit");
        }
        else
        {
            Debug.Log("Pink outfit");
        }
    }
    /// <summary>
	/// Schaut welches Eyes-GameObject visible ist und schickt eine Meldung and die Console.
    /// Wurde für Testzwecke benutzt.
	/// </summary>
    public void CheckWhichGameObjectIsVisibleEyes()
    {
        if (OptionsListEyes[0].activeSelf)
        {
            Debug.Log("Long eyes");
        }
        else
        {
            Debug.Log("Small eyes");
        }
    }
    /// <summary>
	/// Schaut welches Hair-GameObject visible ist und schickt eine Meldung and die Console.
    /// Wurde für Testzwecke benutzt.
	/// </summary>
    public void CheckWhichGameObjectIsVisibleHair()
    {
        if (OptionsListHair[0].activeSelf)
        {
            Debug.Log("PinkHair");
        }
        else if (OptionsListHair[1].activeSelf)
        {
            Debug.Log("RetroOutfit");
        }
        else if (OptionsListHair[2].activeSelf)
        {
            Debug.Log("BrownOutfit");
        }
        else
        {
            Debug.Log("Pink outfit");
        }
    }
}

