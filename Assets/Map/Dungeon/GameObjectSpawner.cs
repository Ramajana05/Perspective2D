using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{

    public GameObject[] objects;

    public int spawnRatioPercent;


    /// <summary>
    /// Startet beim Start des Skripts.
    /// Iniziiert Objekte an den Orten der SpawnLocations mit der gegebenen Wahrscheinlichkeit
    /// </summary>
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (Random.Range(0, 100) <= spawnRatioPercent)
            {
                int rand = Random.Range(0, objects.Length);
                Instantiate(objects[rand], child.position, Quaternion.identity);
            }
        }
    }
}
