using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;

    [SerializeField] private GameObject _loadCanvas;
    [SerializeField] public Vector2 SpawnPosition = new Vector2(0,0);
    public Slider slider;

    /// <summary>
    /// Startet nach der Start-Methode
    /// Setzt die static Instance
    /// </summary>
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Zeigt den LoadScreen mit ProgressBar
    /// </summary>
    /// <param name="sceneName"></param>
    public async void LoadScene(string sceneName)
    {     
        var scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        scene.allowSceneActivation = false;

        _loadCanvas.SetActive(true);
        float timer = 0f;
        do
        {
            await Task.Delay(100);
            slider.value = timer;
            timer+= 0.1f;

        } while (scene.progress < 0.9f || timer < 1);

        slider.value = 1f;
        await Task.Delay(50);

        scene.allowSceneActivation = true;
        _loadCanvas.SetActive(false);
    }
}
