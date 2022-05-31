using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    /// <summary>
    /// Liste der möglichen Raum Layouts 
    /// ID: 0 steht für Ausgänge nach Links und Rechts
    /// ID: 1 steht für Ausgänge nach Links, Rechts und Unten
    /// ID: 2 steht für Ausgänge nach Links, Rechts und Oben
    /// ID: 3 steht für Ausgänge nach Links, Rechts, Unten und Oben
    /// </summary>
    public GameObject[] rooms; 

    private int direction;
    public float moveAmount;

    public int[,] roomLayout = new int[8, 8];
    public float minX;
    public float maxX;
    public float minY;
    private bool stopGeneration;


    //private float timeBTWRoom;
    //public float startTimeBtwRoom = 0.2f;

    /// <summary>
    /// Startet beim Start des Skripts.
    /// Setzt den ersten Raum für die Generation des Wegs durch den Zufallgenerierten Dungeon
    /// </summary>
    void Start()
    {     
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (true)
                {                   
                    roomLayout[x, y] = -1;
                }
            }
        }

        int randStartingPos = 3;
        transform.position = startingPositions[randStartingPos].position;
        int rand = 2;//Random.Range(0, rooms.Length);

        direction = Random.Range(1, 5);
        
        roomLayout[Mathf.RoundToInt((transform.position.x - 5.0f) / 10.0f), Mathf.RoundToInt((transform.position.y - 15.0f) / -10.0f)] = rand;
        Instantiate(rooms[rand], transform.position, Quaternion.identity);
        //printArray();

        while (!stopGeneration)
        {
            Move();
        }
    }

    /// <summary>
    /// Befüllt die fehlen Felder der Karte, die nicht für den Weg/ die Lösung des Dungeon benötigt wurden
    /// </summary>
    private void fillMap()
    {
        for(int x = 0; x < 8; x++)
        {
            for(int y = 0; y < 8; y++)
            {
                if(roomLayout[x,y] == -1)
                {
                    int rand = Random.Range(0, rooms.Length);
                    
                    if ((x == 3 && y == 7) || (x ==4 && y==7) || (x == 6 && y == 0) || (x == 2 && y == 0) || (x == 0 && y == 6))
                    {
                        rand = 3;
                    }
                    roomLayout[x, y] = rand;
                    Vector2 newPos = new Vector2(x * 10 + 5, y*-10+15);
                    transform.position = newPos;

                    Instantiate(rooms[rand], transform.position, Quaternion.identity);
                }
            }
        }
    }

    /// <summary>
    /// In jeder Move()-Iteration wird ein Raum an die mögliche Lösung angefügt
    /// </summary>
    private void Move()
    {
        if (direction == 1 || direction == 2)
        { // Move right

            if (transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
                int x = Mathf.RoundToInt((transform.position.x - 5.0f) / 10.0f);
                int y = Mathf.RoundToInt((transform.position.y - 15.0f) / -10.0f);

                int rand = Random.Range(0, rooms.Length);
                
                if ((x == 3 && y == 7) || (x == 4 && y == 7) || (x == 6 && y == 0) || (x == 2 && y == 0) || (x == 0 && y == 6))
                {
                    rand = 3;
                }

                direction = Random.Range(1, 6);
                if(direction == 3)
                {
                    direction = 2;
                }else if (direction == 4)
                {
                    direction = 5;
                }
                
                if(direction == 5 || transform.position.x >= maxX)
                {
                    rand = Random.Range(1, 3);
                    if ((x == 3 && y == 7) || (x == 4 && y == 7) || (x == 6 && y == 0) || (x == 2 && y == 0) || (x == 0 && y == 6) || rand == 2)
                    {
                        rand = 3;
                    }
                                     
                }
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                roomLayout[x, y] = rand;
                //printArray();

            }
            else
            {
                direction = 5;
            }
            
        }else if (direction == 3 || direction == 4)
        { // Move Left

            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
                int x = Mathf.RoundToInt((transform.position.x - 5.0f) / 10.0f);
                int y = Mathf.RoundToInt((transform.position.y - 15.0f) / -10.0f);

                int rand = Random.Range(0, rooms.Length);                
                if ((x == 3 && y == 7) || (x == 4 && y == 7) || (x == 6 && y == 0) || (x == 2 && y == 0) || (x == 0 && y == 6))
                {
                    rand = 3;
                }

                direction = Random.Range(3, 6);

                if (direction == 5 || transform.position.x <= minX)
                {
                    rand = Random.Range(1, 3);                   
                    if ((x == 3 && y == 7) || (x == 4 && y == 7) || (x == 6 && y == 0) || (x == 2 && y == 0) || (x == 0 && y == 6) || rand == 2)
                    {
                        rand = 3;
                    }                    
                }         
                
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                roomLayout[x, y] = rand;
                //printArray();
            }
            else
            {
                direction = 5;
            }
            
        }else if (direction == 5)
        { // Move Down
            if(transform.position.y > minY)
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;
                int x = Mathf.RoundToInt((transform.position.x - 5.0f) / 10.0f);
                int y = Mathf.RoundToInt((transform.position.y - 15.0f) / -10.0f);

                int rand = Random.Range(2, 4);
                
                if ((x == 3 && y == 7) || (x == 4 && y == 7) || (x == 6 && y == 0) || (x == 2 && y == 0) || (x == 0 && y == 6))
                {
                    rand = 3;
                }

                direction = Random.Range(1, 6);

                if ((direction == 1 || direction == 2) && transform.position.x >= maxX)
                {
                    direction = Random.Range(3, 6);

                }
                else if ((direction == 3 || direction == 4) && transform.position.x <= minX)
                {
                    if (direction == 3)
                    {
                        direction = 2;
                    }
                    else if (direction == 4)
                    {
                        direction = 5;
                    }
                }

                if (direction == 5 || transform.position.x >= maxX || transform.position.x <= minX)
                {
                    rand = Random.Range(2, 4);                   
                    
                    if ((x == 3 && y == 7) || (x == 4 && y == 7) || (x == 6 && y == 0) || (x == 2 && y == 0) || (x == 0 && y == 6))
                    {
                        rand = 3;
                    }

                    if (transform.position.x >= maxX && rand == 2)
                    {
                        direction = 3;
                    }else if (transform.position.x <= minX && rand == 2)
                    {
                        direction = 1;
                    }else if (rand == 2)
                    {
                        direction = 2;
                    }
                 
                }

                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                roomLayout[x, y] = rand;
                //printArray();

            }
            else
            {
                // Stop 
                stopGeneration = true;
                fillMap();
            }
        }
    }
}
