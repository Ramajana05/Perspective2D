using System.Data;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using DataBase;
using System.Globalization;

/// <summary>
/// Speichert die entsprechenden Werte des Spieler-Objekt in der DB
/// </summary>
public class SavePlayer : MonoBehaviour
{
    SqliteHelper gameDataBase;
    private Scene currentScene;
    private Player player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentScene = SceneManager.GetActiveScene();
        gameDataBase = new SqliteHelper();
    }

    /// <summary>
    /// Speichere alle benötigten Informationen über Spieler und Spielstand in die DB
    /// </summary>
    public void Save()
    {
        Debug.Log(currentScene.name);
        IDbCommand dbcmd = gameDataBase.db_connection.CreateCommand();

        // Info: Im Dungeon wird die Position des Spieler nicht gespeichert 
        if (currentScene.name == "Dungeon_Forest" || currentScene.name == "Dungeon_Mountain")
        {
            dbcmd.CommandText = "UPDATE PlayerStatus SET " +
                                    "Healthbar=" + player.currentHealth.ToString() + ", " +
                                    "Manabar=" + player.currentMana.ToString() + ", " +
                                    "Potion_Heal=" + AmountManager.potionAmount.ToString() + ", " +
                                    "Potion_Mana=" + AmountManager.manaAmount.ToString() + ", " +
                                    "QuestProgress=" + player.GetComponent<QuestHandler>().QuestProgress.ToString() + " " +
                                    "WHERE rowid in (select rowid FROM PlayerStatus LIMIT 1)";
        }
        else
        {
            IDbCommand cmd = gameDataBase.db_connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM PlayerStatus";
            var count = (Int64)cmd.ExecuteScalar();
            //Insert der Daten falls die Tabelle in der DB nicht existiert
            if (count == 0)
            {
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
                                        player.currentHealth.ToString() + "," +
                                        player.currentMana.ToString() + "," +
                                        "1" + "," +
                                        "0" + "," +
                                        "1" + "," +
                                        AmountManager.potionAmount.ToString() + "," +
                                        AmountManager.manaAmount.ToString() + "," +
                                        player.GetComponent<Rigidbody2D>().transform.position.x.ToString(CultureInfo.InvariantCulture)
                                        + "," +
                                        player.GetComponent<Rigidbody2D>().transform.position.y.ToString(CultureInfo.InvariantCulture)
                                        + "," +
                                        "'" + currentScene.name + "', " +
                                        player.GetComponent<QuestHandler>().QuestProgress.ToString() + ")";
            }
            //Update der Daten falls die Tabelle in der DB existiert
            else
            {


                dbcmd.CommandText = "UPDATE PlayerStatus SET " +
                                        "Healthbar=" + player.currentHealth.ToString() + ", " +
                                        "Manabar=" + player.currentMana.ToString() + ", " +
                                        "Potion_Heal=" + AmountManager.potionAmount.ToString() + ", " +
                                        "Potion_Mana=" + AmountManager.manaAmount.ToString() + ", " +
                                        "Location_x=" + player.GetComponent<Rigidbody2D>().transform.position.x.ToString(CultureInfo.InvariantCulture) + ", " +
                                        "Location_y=" + player.GetComponent<Rigidbody2D>().transform.position.y.ToString(CultureInfo.InvariantCulture) + ", " +
                                        "Current_Scene='" + currentScene.name + "', " +
                                        "QuestProgress=" + player.GetComponent<QuestHandler>().QuestProgress.ToString() + " " +
                                        "WHERE rowid in (select rowid FROM PlayerStatus LIMIT 1)";
            }
        }

        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
    }
}