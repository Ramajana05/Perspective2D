using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using System.Data;

public class EnterDungeon : MonoBehaviour
{ 
    public string sceneToLoad;
    public Vector2 SpawnPosition;

    private GameObject player;

    /// <summary>
    /// Startet beim Start des Skripts.
    /// Setzt benötigte Variablen
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Bei einer Collision mit einem Player-Objekt wird die neue Spawn-Location in die Datenbank gespeichert und ein LoadScreen gestartet.
    /// Falls der Fortschritt des Spielers bei Quest 9 ist wird die SetQuestNineSpawnLocation()-Funktion aufgerufen 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<QuestHandler>().QuestProgress == 9)
        {
            SetQuestNineSpawnLocation();
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                LoadingScreen.Instance.SpawnPosition = this.SpawnPosition;
                LoadingScreen.Instance.LoadScene(sceneToLoad);
            }
        }
    }

    /// <summary>
    /// Setzt die Spawn-Location des Spielers auf die Anfangszene vor dem ersten Haus
    /// </summary>
    private void SetQuestNineSpawnLocation()
    {
        LoadingScreen.Instance.SpawnPosition = new Vector2(0, 0);

        SqliteHelper gameDataBase = new SqliteHelper();
        IDbCommand dbcmd = gameDataBase.db_connection.CreateCommand();
        dbcmd.CommandText = "UPDATE PlayerStatus SET " +
                                "Healthbar=100, " +
                                "Manabar=100, " +
                                "Location_x= -0.5, " +
                                "Location_y= -20," +
                                "Current_Scene='Patrick' " +
                                "WHERE rowid in (select rowid FROM PlayerStatus LIMIT 1)";
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;

        LoadingScreen.Instance.LoadScene("Patrick");
    }
}
