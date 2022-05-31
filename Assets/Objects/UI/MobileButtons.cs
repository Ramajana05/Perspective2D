using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileButtons : MonoBehaviour
{
    public Image swordImage;
    public Image weaponImage;
    public Image attackButton;
    public Image useButton;
    public Image useImage;
    public Image settingsButton;
    public Image settingsImage;
    Player player;

    /// <summary>
    /// Wird beim Start ausgeführt
    /// Setzt benötigte Variablen für PC oder Android
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {       
            if(player.currentWeapon == 1)
            {
                swordImage.enabled = true;
                weaponImage.enabled = false;
            }
            else if(player.currentWeapon == 2)
            {
                swordImage.enabled = false;
                weaponImage.enabled = true;
            }
           
            attackButton.enabled = true;
            useButton.enabled = true;
            useImage.enabled = true;
            settingsButton.enabled = true;
            settingsImage.enabled = true;
        }
        else
        {
            swordImage.enabled = false;
            weaponImage.enabled = false;
            attackButton.enabled = false;
            useButton.enabled = false;
            useImage.enabled = false;
            settingsButton.enabled = false;
            settingsImage.enabled = false;
        }
    }

    /// <summary>
    /// Rüstet das Schwert aus
    /// </summary>
    public void showSword()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        player.currentWeapon = 1;
        swordImage.enabled = true;
        weaponImage.enabled = false;
    }

    /// <summary>
    /// Rüstet die Waffe aus
    /// </summary>
    public void showWeapon()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        player.currentWeapon = 2;
        swordImage.enabled = false;
        weaponImage.enabled = true;
    }
}
