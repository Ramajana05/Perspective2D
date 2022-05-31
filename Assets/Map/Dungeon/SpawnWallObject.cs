using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWallObject : MonoBehaviour
{
    public GameObject[] walls;

    /// <summary>
    /// Startet beim Start des Skripts.
    /// Iniziiert Objekte an den Orten der SpawnLocations
    /// </summary>
    void Start()
    {
        foreach (Transform child in transform)
        {
            int rand = Random.Range(0, walls.Length);
            Instantiate(walls[rand], child.position, Quaternion.identity);
        }
    }
}
