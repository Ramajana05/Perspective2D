using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;
using System;
using DataBase;

public class AmountManager : MonoBehaviour
{

    public static AmountManager instance;
    public TextMeshProUGUI potionText;
    public TextMeshProUGUI manaText;
    public static double potionAmount = 0;
    public static double manaAmount = 0;
    Player player;
    public bool swordEquipped = true;
    public bool weaponEquipped = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (instance == null)
        {
            instance = this;
        }
    }

    void Awake()
    {
        SetPotionScore(GameObject.FindWithTag("Player").GetComponent<LoadPlayer>().potionHeal, GameObject.FindWithTag("Player").GetComponent<LoadPlayer>().potionMana);
    }
    private void Update()
    {
      
    }

    public void ChangeScore(double val)
    {
        if (potionAmount+val <= 20)
        {
            potionAmount += val;
            potionText.text = potionAmount.ToString();
            manaAmount += val;
            manaText.text = manaAmount.ToString();
        }
        else
        {
            potionAmount = 20;
            potionText.text = potionAmount.ToString();
            manaAmount = 20;
            manaText.text = manaAmount.ToString();
        }
        SqliteHelper gameDataBase = new SqliteHelper();
        IDbCommand dbcmd = gameDataBase.db_connection.CreateCommand();
        dbcmd.CommandText = "UPDATE PlayerStatus SET " +
                                    "Healthbar=" + player.currentHealth.ToString() + ", " +
                                    "Manabar=" + player.currentMana.ToString() + ", " +
                                    "Potion_Heal=" + AmountManager.potionAmount.ToString() + ", " +
                                    "Potion_Mana=" + AmountManager.manaAmount.ToString() +" "+                                    
                                    "WHERE rowid in (select rowid FROM PlayerStatus LIMIT 1)";

        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbcmd = null;
    }

    public void SetPotionScore(double heal, double mana)
    {       
        potionAmount = heal;
        potionText.text = potionAmount.ToString();
        manaAmount = mana;
        manaText.text = manaAmount.ToString();
     
    }

    public void equipSword()
    {
        Debug.Log("Schwert ausgerüstet");
        player.currentWeapon = 1;
        swordEquipped = true;
        weaponEquipped = false;
    }

    public void equipWeapon()
    {
        Debug.Log("Waffe ausgerüstet");
        player.currentWeapon = 2;
        swordEquipped = false;
        weaponEquipped = true;
    }

    public void useButton()
    {
        Debug.Log("Benutzen");
    }


    public void healAndUsePotion()
    {      
        player.currentWeapon = 0;
        swordEquipped = false;
        weaponEquipped = false;

        if (potionAmount > 0)
        {
            if (player.currentHealth == player.maxHealth)
            {

            }
            else
            {
                potionAmount -= 1;

                potionText.text = potionAmount.ToString();

                player.healPlayer(25);
            }
        }
    }

    public void healManaAndUsePotion()
    {
        player.currentWeapon = 0;
        swordEquipped = false;
        weaponEquipped = false;

        if (manaAmount > 0)
        {
            if (player.currentMana == player.maxMana)
            {
                
            }
            else
            {
                manaAmount -= 1;

                manaText.text = manaAmount.ToString();

                player.healMana(25);
            }
        }
    }
}



    
