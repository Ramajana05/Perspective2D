using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{ 
    public GameObject houseInterior;
    public Vector3 houseInteriorOffset;

    private GameObject player;    
  
    /// <summary>
    /// Setzt die benötigte Player Variable
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Findet eine Collision statt wird colliding auf true gesetzt
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.GetComponent<QuestHandler>().QuestProgress == 9)
        {
            SceneManager.LoadScene("Cutscene", LoadSceneMode.Single);
        }
        else
        {
            player.transform.position = houseInterior.transform.position + houseInteriorOffset;
        }
    }
}
