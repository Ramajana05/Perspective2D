using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicsSettings : MonoBehaviour
{
    public TextMeshProUGUI FullscreenText = null;
    public bool isFullscreen = true;
    public Button fullscreenButton;

    public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    /// <summary>
    /// Wird beim Start ausgeführt
    /// Setzt benötigte Variablen
    /// Entfernt den Fullscreen Knopf auf Android
    /// </summary>
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            fullscreenButton.interactable = false;
        }
        else
        {
            fullscreenButton.interactable = true;
        }

            if (PlayerPrefs.GetInt("Fullscreen") != 0)
        {
            isFullscreen = true;
            Screen.fullScreen = isFullscreen;
            FullscreenText.text = "Vollbild: An";
        }
        else
        {
            isFullscreen = false;
            Screen.fullScreen = isFullscreen;
            FullscreenText.text = "Vollbild: Aus";
        }
    }

    /// <summary>
    /// Wird für jeden Frame ausgeführt
    /// Prüft ob Spiel im Vollbild Modus ist
    /// </summary>
    void Update()
    {
        PlayerPrefs.SetInt("Fullscreen", (isFullscreen ? 1 : 0));
    }

    /// <summary>
    /// Implementiert die Vollbild einstellung
    /// </summary>
    public void FullscreenSetting()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");

        if (PlayerPrefs.GetInt("Fullscreen") != 0)
        {
            isFullscreen = false;
            Screen.fullScreen = isFullscreen;
            FullscreenText.text = "Vollbild: Aus";
        }
        else
        {
            isFullscreen = true;
            Screen.fullScreen = isFullscreen;
            FullscreenText.text = "Vollbild: An";
        }
    }

    /// <summary>
    /// Implementiert die Grafik qualität einstellung
    /// </summary>
    /// <param name="qualityIndex"></param>
    public void SetQuality(int qualityIndex)
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");

        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Methode zum finden der Systemauflösung
    /// Einstellen der Auflösung
    /// </summary>
    /// <param name="resolutionIndex"></param>
    public void SetResolution(int resolutionIndex)
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ButtonClick");

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
