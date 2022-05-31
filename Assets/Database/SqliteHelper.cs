using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;

namespace DataBase
{
    /// <summary>
    /// Helferklasse um den Zugriff auf die Datenbank zu vereinfachen
    /// </summary>
    public class SqliteHelper
    {
        private const string Tag = "Riz: SqliteHelper:\t";
        private const string database_name = "PlayerData.db";

        public string db_connection_string;
        public IDbConnection db_connection;
        

        /// <summary>
        /// Helper um den Zugriff auf die Datenbank zu erleichtern.
        /// K�mmert sich um die initialen Verbindungsaufbau zur DB 'PlayerData.db'
        /// </summary>
        public SqliteHelper()
        {
            string filepath;

            //datenbanklocation wenn die app auf dem pc l�uft
            if (Application.platform != RuntimePlatform.Android)
            {
                filepath =  Application.dataPath + "/Database/" + database_name;
                Debug.Log("db_connection_string" + filepath);
            }

            //datenbanklocation wenn die app auf android l�uft
            else
            {
                filepath = Application.persistentDataPath + "/" + database_name;
                
                // check if file exists in Application.persistentDataPath
                if (!File.Exists(filepath))
                {                  
                    WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + database_name);  

                    while (!loadDB.isDone) { }
                    File.WriteAllBytes(filepath, loadDB.bytes);
                }               
            }

            var connection = "URI=file:" + filepath;
            db_connection = new SqliteConnection(connection);
            db_connection.Open();
        }

        /// <summary>
        /// Aufr�umen, falls SQLite Helper zerst�rt wird
        /// </summary>
        ~SqliteHelper()
        {
            db_connection.Close();
        }

        /// <summary>
        /// L�scht allle Daten in der Tabelle PlayerStatus
        /// </summary>
        public virtual void deleteAllData()
        {
            //delete oldest/first db entry
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DELETE FROM PlayerStatus";
            dbcmd.ExecuteNonQuery();
            dbcmd.Dispose();
            dbcmd = null;
        }

        /// <summary>
        /// Datenbank verbindung schlie�en
        /// </summary>
        public void close()
        {
            db_connection.Close();
        }
    }
}

