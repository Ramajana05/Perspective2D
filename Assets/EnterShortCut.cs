using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterShortCut : MonoBehaviour
{
    public Vector2 SpawnPosition;

    private GameObject player;

    /// <summary>
    /// Setzt die benötigte Player Variable
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Findet eine Collision statt wird der Spieler zur Spawn-Position teleportiert
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.tag == "Player")
        {
            player.transform.position = SpawnPosition;
        }

    }
}