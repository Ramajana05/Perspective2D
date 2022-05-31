using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public double potionValue = 0.5;

    /// <summary>
    /// Wird bei einer Collision ausgeführt und triggert die ChangeScore-Methode des AmountManagers
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AmountManager.instance.ChangeScore(potionValue);
            Destroy(gameObject);
        }
    }

}
