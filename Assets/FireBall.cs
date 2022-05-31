using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 20f;

    public Rigidbody2D rb;

    /// <summary>
    /// In dieser Methode wird die Richtung und die Geschwindigkeit des Feuerballs eingestellt basierend auf die Richtung des Spielers
    /// </summary>
    void Start()
    {
        Vector2 temp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().movement.normalized;
        if (0 == temp.sqrMagnitude)
        {
            temp = new Vector2(0, -1);
        }
        rb.velocity = temp * speed;

    }
    /// <summary>
    /// In dieser Methode wird der Feuerball nach 2 Sekunden zerstoert falls er nicht kollidiert
    /// </summary>
    void Update()
    {

        Destroy(gameObject, 2f);
    }
    /// <summary>
    /// In dieser Methode wird gecheckt ob der Feuerball einen Gegner getroffen hat, falls es so ist kriegt der Gegner 50 damage und der Feuerball wird zerstoert
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(50);
            Destroy(gameObject);
        }

    }
}
