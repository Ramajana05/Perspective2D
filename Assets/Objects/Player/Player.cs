using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using UnityEngine.SceneManagement;
using DataBase;
using System.Data;

public class Player : MonoBehaviour
{
    private new Renderer renderer;
    private GameObject[] allEnemies;

    public int maxHealth = 100;
    public int currentHealth;

    public int maxMana = 100;
    public int currentMana = 100;

    public HealthBar healthBar;
    public ManaBar manaBar;

    public int attackDamage = 10;
    public float meeleRange = 4;

    public int currentWeapon = 1;

    public float musicVolume;
    public float effectVolume;

    /// <summary>
    /// Wird beim Start ausgeführt
    /// Startet die LoadPlayer()-Funktion
    /// Setzt benötigte Variablen
    /// </summary>
    void Awake()
    { 
        LoadPlayer();

        currentHealth = GetComponent<LoadPlayer>().playerHealth;
        healthBar.setHealth(currentHealth);
        currentMana = GetComponent<LoadPlayer>().playerMana;
        manaBar.setMana(currentMana);
        renderer = GetComponent<Renderer>();
        InvokeRepeating("SavePlayer", 10, 10);
    }

    /// <summary>
    /// Fügt den gegnern in Reichweite Schaden zu
    /// </summary>
    public void dealDamage()
    {        
        Vector3 player = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Renderer>().bounds.center;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (allEnemies != null)
        {
            foreach (GameObject enemy in allEnemies)
            {           
                if (Vector2.Distance(player, enemy.GetComponent<Renderer>().bounds.center) <= meeleRange)
                {
                   enemy.GetComponent<Dragon>().takeDamage(attackDamage);
                }
            }          
        }
    }

    /// <summary>
    /// Spieler nimmt Schaden
    /// Falls Spieler dadurch stirbt, wird der Startort nach dem Tot in die Datenbank geschrieben  
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;

            healthBar.setHealth(currentHealth);
        }
        else
        {
            LoadingScreen.Instance.SpawnPosition = new Vector2(0, 0);

            SqliteHelper gameDataBase = new SqliteHelper();
            IDbCommand dbcmd = gameDataBase.db_connection.CreateCommand();
            dbcmd.CommandText = "UPDATE PlayerStatus SET " +
                                    "Healthbar=100, " +
                                    "Manabar=100, " +
                                    "Location_x= -81, " +
                                    "Location_y= 25.4," +
                                    "Current_Scene='Patrick' " +
                                    "WHERE rowid in (select rowid FROM PlayerStatus LIMIT 1)";
            dbcmd.ExecuteNonQuery();
            dbcmd.Dispose();
            dbcmd = null;
            LoadingScreen.Instance.LoadScene("Patrick");
        }
    }

    /// <summary>
    /// Spieler gibt mana aus
    /// </summary>
    /// <param name="mana"></param>
    public void TakeMana(int mana)
    {
        if (currentMana > 0)
        {
            currentMana -= mana;
            manaBar.setMana(currentMana);
        }
        else
        {
            manaBar.setMana(currentMana);
        }
    }

    /// <summary>
    /// Spieler heilt sich
    /// </summary>
    /// <param name="amount"></param>
    public void healPlayer(int amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.setHealth(currentHealth);
        }
        else if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            healthBar.setHealth(currentHealth);            
        }       
    }

    /// <summary>
    /// Spieler bekommt Mana
    /// </summary>
    /// <param name="amount"></param>
    public void healMana(int amount)
    {
        if (currentMana + amount > maxMana)
        {
            currentMana = maxMana;
            manaBar.setMana(currentMana);
        }
        else if (currentMana < maxMana)
        {
            currentMana += amount;
            manaBar.setMana(currentMana);
        }
    }

    /// <summary>
    /// Startet die save-Methode im SavePlayer-Skript
    /// </summary>
    private void SavePlayer()
    {
        Debug.Log("Player saved");
        GetComponent<SavePlayer>().Save();
    }

    /// <summary>
    /// Startet die load-Methode im LoadPlayer-Skript
    /// </summary>
    private void LoadPlayer()
    {
        GetComponent<LoadPlayer>().load();
    }
}
