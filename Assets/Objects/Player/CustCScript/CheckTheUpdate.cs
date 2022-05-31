using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System.IO;
using DataBase;
using Mono.Data.Sqlite;

public class CheckTheUpdate : MonoBehaviour
{

    public GameObject character;

    //for saving
    public string outfit = "";
    public string eyes = "";
    public string hair = "";

    //ObjectsLists
    public List<GameObject> OptionsListHair = new List<GameObject>();
    public List<GameObject> OptionsListEyes = new List<GameObject>();
    public List<GameObject> OptionsListBody = new List<GameObject>();
    /// <summary>
    /// Wird beim AnimatedPlayer aufgerufen
    /// Methoden werden aufgerufen
    /// Es wird nach den Objekten geschaut, die der Spieler in der Szene UIAnimated ausgewaehlt hat
    /// </summary>
    public void CheckAll(){
        CheckWhichGameObjectIsVisibleHair();
        CheckWhichGameObjectIsVisibleBody();
        CheckWhichGameObjectIsVisibleEyes();  
        WriteString();
    }
    /// <summary>
	/// Hier wird ueberprüft welches GameObject-Outfit in der Szene UIAnimated sichtbar ist und dann mit dem dazu gehoerigem
    /// String in der Datenbank gespeichert. 
	/// </summary>
    public void CheckWhichGameObjectIsVisibleBody()
    {
        if (OptionsListBody[0].activeSelf) {
           outfit = "ArmorOutfit";
        } else if (OptionsListBody[1].activeSelf) {
            outfit = "RetroOutfit";
        } else if (OptionsListBody[2].activeSelf) {
            outfit = "BrownOutfit";
        } else {
            outfit = "PinkOutfit";
        }
    }
    /// <summary>
	/// Hier wird ueberprüft welches GameObject-Eyes in der Szene UIAnimated sichtbar ist und dann mit dem dazu gehoerigem
    /// String in der Datenbank gespeichert. 
	/// </summary>
    public void CheckWhichGameObjectIsVisibleEyes()
    {
        if (OptionsListEyes[0].activeSelf) {
            eyes = "SmallEyes";
        } else {
            eyes = "LongEyes";
        }
    }
    /// <summary>
    /// Hier wird ueberprüft welches GameObject-Hair in der Szene UIAnimated sichtbar ist und dann mit dem dazu gehoerigem
    /// String in der Datenbank gespeichert. 
    /// </summary> 
    public void CheckWhichGameObjectIsVisibleHair()
    {
        if (OptionsListHair[0].activeSelf) {
            hair = "ArmorHelmet";
        } else if (OptionsListHair[1].activeSelf) {
            hair = "BrownHair";
        } else if (OptionsListHair[2].activeSelf) {
            hair = "PinkHair";
        } 
    }
    /// <summary>
    /// Die Methode ist für die Datenbank da.
    /// </summary> 
    public void WriteString()
    {

        SqliteHelper gameDataBase = new SqliteHelper();
        IDbCommand dbcmd = gameDataBase.db_connection.CreateCommand();

        dbcmd.CommandText = "INSERT INTO PlayerStatus (" +
                                        "Healthbar, " +
                                        "Manabar, " +
                                        "Outfit_Upper, " +
                                        "Outfit_Middle, " +
                                        "Outfit_Lower, " +
                                        "Potion_Heal, " +
                                        "Potion_Mana, " +
                                        "Location_x, " +
                                        "Location_y," +
                                        "Current_Scene," +
                                        "QuestProgress) " +
                                    "VALUES (" +
                                        "100" + "," +
                                        "100" + "," +
                                        "'" + hair + "'," +
                                        "'" + eyes + "'," +
                                        "'" + outfit + "'," +
                                        "0" + "," +
                                        "0" + "," +
                                        "-81" + "," +
                                        "25" + "," +
                                        "'" + "Patrick" + "', " +
                                        "0" + ")";

        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;       
    }
}
