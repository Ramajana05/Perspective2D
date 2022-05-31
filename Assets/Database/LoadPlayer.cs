using System.Data;
using UnityEngine;
using System;
using DataBase;

/// <summary>
/// Lädt die Daten des Spielers aus der DB und setzt die entsprechenden Werte im Objekt
/// </summary>
public class LoadPlayer : MonoBehaviour
{
    SqliteHelper gameDataBase;

    private Player player;
    private Rigidbody2D playerBody;

    public int playerHealth;
    public int playerMana;
    public String outfitUpper;
    public String outfitMiddle;
    public String outfitLower;
    public int potionHeal;
    public int potionMana;
    public float locationX;
    public float locationY;
    public String currentScene;
    public int questProgress;

    /// <summary>
    /// Lade die Infos des Spieler aus der DB und setzte sie entsprechend im Player-Objekt
    /// </summary>
    public void load()
    {
        //Finde die Benötigten Komponenten die zum Laden des Spielers benötigt werden
        player = GetComponent<Player>();
        playerBody = GetComponent<Rigidbody2D>();

        //DB Verbindung aufbauen
        gameDataBase = new SqliteHelper();

        IDbCommand cmnd_read = gameDataBase.db_connection.CreateCommand();

        //SQL Befehl konstruieren
        cmnd_read.CommandText = "SELECT " +
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
                            "QuestProgress" +
            " FROM  PlayerStatus";

        IDataReader reader = cmnd_read.ExecuteReader();

        //Daten aus DB lesen
        while (reader.Read())
        {
            //Lade das Leben und Mana des Spieler
            playerHealth = int.Parse(reader[0].ToString());
            playerMana = int.Parse(reader[1].ToString());

            //Lade das passende Aussehen des Spielers
            outfitUpper = reader[2].ToString();
            outfitMiddle = reader[3].ToString();
            outfitLower = reader[4].ToString();

            //Lade Anzahl Health und ManaPotions des Spieler
            potionHeal = int.Parse(reader[5].ToString());
            potionMana = int.Parse(reader[6].ToString());

            //Lade Position des Spieler        
            locationX = float.Parse(reader[7].ToString());
            locationY = float.Parse(reader[8].ToString());

            //lade name der letzen Szene
            currentScene = reader[9].ToString();

            //lade aktuellen Questprogress
            questProgress = int.Parse(reader[10].ToString());
        }

        cmnd_read.Dispose();
        cmnd_read = null;
        gameDataBase.close();
    }
}
