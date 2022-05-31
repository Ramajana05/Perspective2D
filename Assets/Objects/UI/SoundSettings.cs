using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSettings : MonoBehaviour
{
    public TextMeshProUGUI MusicText = null;
    public bool Music = true;
    public Slider SliderEffect;
    public Slider SliderMusic;

    /// <summary>
    /// Wird beim Start ausgef�hrt
    /// Setzt ben�tigte Variablen
    /// </summary>
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        //To be fixed
        if (true)
        {
            Music = true;
            MusicText.text = "Musik: An";
        }
        else
        {
            Music = false;
            MusicText.text = "Musik: Aus";
        }
    }

    /// <summary>
    /// Wird f�r jeden Frame ausgef�hrt
    /// Pr�ft ob Musik an oder aus sein soll
    /// </summary>
    void Update()
    {
        PlayerPrefs.SetInt("Music", (Music ? 1 : 0));
    }

    /// <summary>
    /// An/Aus Schalter f�r Musik
    /// </summary>
    public void MusicSetting()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");
        if (PlayerPrefs.GetInt("Music") != 0)
        {
            Music = false;
            MusicText.text = "Musik: Aus";

            GameObject.FindGameObjectWithTag("AudioHotFix").GetComponent<AudioHotFix>().effectVolume = 0;
            GameObject.FindGameObjectWithTag("AudioHotFix").GetComponent<AudioHotFix>().musicVolume = 0;
        }
        else
        {
            Music = true;
            MusicText.text = "Musik: An";

            GameObject.FindGameObjectWithTag("AudioHotFix").GetComponent<AudioHotFix>().effectVolume = SliderEffect.value;
            GameObject.FindGameObjectWithTag("AudioHotFix").GetComponent<AudioHotFix>().musicVolume = SliderMusic.value;
        }
    }

    /// <summary>
    /// Sound Effekte Lautst�rke regler
    /// </summary>
    /// <param name="volumeSlider"></param>
    public void SetEffectVolume(Slider volumeSlider)
    {      
        GameObject.FindGameObjectWithTag("AudioHotFix").GetComponent<AudioHotFix>().effectVolume = volumeSlider.value;
    }

    /// <summary>
    /// Musik Lautst�rke regler
    /// </summary>
    /// <param name="volumeSlider"></param>
    public void SetMusicVolume(Slider volumeSlider)
    {
        GameObject.FindGameObjectWithTag("AudioHotFix").GetComponent<AudioHotFix>().musicVolume = volumeSlider.value;
    }

}
