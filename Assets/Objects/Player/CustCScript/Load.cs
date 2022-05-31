using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load : MonoBehaviour
{
    public GameObject character;

    //ObjectsLists
    public List<GameObject> OptionsListHair = new List<GameObject>();
    public List<GameObject> OptionsListEyes = new List<GameObject>();
    public List<GameObject> OptionsListBody = new List<GameObject>();

    /// <summary>
	/// Erst werden alle GameObjects vom AnimatedPlayer auf invisible gesetzt und dann wird in der Datebank überprüft, welches
    /// GameObject man auf visible setzten soll. Mithilfe von einem String, welcher in dem CheckTheUpdate Script gesetz wird. 
	/// </summary>
    void Start()
    {
        SetAllOnFalseBody();
        SetAllOnFalseHair();
        SetAllOnFalseEyes();

        TurnAllGameObjectsOn();
    }
    /// <summary>
	/// Hier wird überprüft welche Strings zu dem GameObject CustomeCharacter in der Datenbank gespeichert wurde und dann wird das dazugehörige GameObject auf "visible" gesetzt.
	/// </summary>
    public void TurnAllGameObjectsOn()
    {

        switch (GetComponent<LoadPlayer>().outfitLower)
        {
            case "ArmorOutfit":
                OptionsListBody[0].SetActive(true);
                break;
            case "RetroOutfit":
                OptionsListBody[1].SetActive(true);
                break;
            case "BrownOutfit":
                OptionsListBody[2].SetActive(true);
                break;
            default:
                OptionsListBody[3].SetActive(true);
                break;
        }

        switch (GetComponent<LoadPlayer>().outfitMiddle)
        {
            case "SmallEyes":
                OptionsListEyes[0].SetActive(true);
                break;
            default:
                OptionsListEyes[1].SetActive(true);
                break;
        }

        switch (GetComponent<LoadPlayer>().outfitUpper)
        {
            case "ArmorHelmet":
                OptionsListHair[0].SetActive(true);
                break;
            case "BrownHair":
                OptionsListHair[1].SetActive(true);
                break;
            default:
                OptionsListHair[2].SetActive(true);
                break;
        }
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

}
