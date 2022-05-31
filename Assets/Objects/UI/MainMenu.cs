using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DataBase;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Pausiert Spiel
    /// </summary>
    public void PauseGame()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Beendet die Pause
    /// </summary>
    public void ResumeGame()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        Time.timeScale = 1f;
    }
    
    /// <summary>
    /// Startet den Character Creator oder das Spiel
    /// Nimmt den aktuellen Spielstand von der Datenbank
    /// </summary>
    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        Time.timeScale = 1f;        

        SqliteHelper gameDataBase = new SqliteHelper();        
        IDbCommand dbcmd = gameDataBase.db_connection.CreateCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'PlayerStatus' ('Healthbar' INTEGER, " +
            "'Manabar' INTEGER, 'Outfit_Upper' TEXT, 'Outfit_Middle' TEXT, 'Outfit_Lower' TEXT, 'Potion_Heal' INTEGER, 'Potion_Mana' INTEGER, 'Location_x' NUMERIC, 'Location_y' INTEGER, 'Current_Scene' TEXT, 'QuestProgress' INTEGER)";
        dbcmd.ExecuteScalar();


        dbcmd = gameDataBase.db_connection.CreateCommand();
        dbcmd.CommandText = "SELECT COUNT(*) FROM PlayerStatus";

        var count = (Int64)dbcmd.ExecuteScalar();

        if (count == 0){
            SceneManager.LoadScene("UIAnimated");
        } else {
            SceneManager.LoadScene("Patrick");
        }
        gameDataBase.close();
    }

    public void SettingsMenu() //TBdeleted
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    /// <summary>
    /// Schließt das Spiel
    /// </summary>
    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        Application.Quit();
    }

    /// <summary>
    /// Löscht den Spielstand in der Datenbank und Lokal
    /// </summary>
    public void DeleteProgress()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        SqliteHelper gameDataBase = new SqliteHelper();
        gameDataBase.deleteAllData();
    }
}
