using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyState
{
    asleep,
    walk,
    attack,
    stagger,
    dead
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public int hp;
    public float movementSpeed;
    public string enemyName;
    public int baseAttack;
    public float attacksPerSecond;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn ein Gegner Schaden erhalten soll. Zudem ueberprueft diese Methode, ob ein Gegner stirbt oder nicht.
    /// </summary>
    /// <param name="amount">Die Größe des Schadens, die der Gegner erhalten soll</param>
    public void TakeDamage(int amount)
    {
        hp -= amount;
        
        if(hp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn der HP-Wert eines Gegners auf 0 faellt. Diese Methode, ob der gestorbene Gegner ein Drache war und laedt
    /// dementsprechend eine neue Szene. Falls es kein Drache ist, wird das GameObjekt geloescht.
    /// </summary>
    public virtual void Die()
    {
        string tag = gameObject.tag;
        Destroy(gameObject);
        if (tag == "Dragon")
        {
            SceneManager.LoadScene("EndScreen");
        }
    }
}
