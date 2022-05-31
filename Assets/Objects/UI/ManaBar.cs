using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{

    public Slider slider;

    /// <summary>
    /// Setzt Variabled im zusammengang mit Mana
    /// </summary>
    /// <param name="mana"></param>
    public void setMaxMana(int mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }
    
    /// <summary>
    /// Setzt die aktuelle Mana
    /// </summary>
    /// <param name="mana"></param>
    public void setMana(int mana)
    {
        slider.value = mana;
    }
}
