using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    /// <summary>
    /// Setzt Variabled im zusammengang mit Health
    /// </summary>
    /// <param name="health"></param>
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    /// <summary>
    /// Setzt die aktuelle Health
    /// </summary>
    /// <param name="health"></param>
    public void setHealth(int health)
    {
        slider.value = health;
    }

}
