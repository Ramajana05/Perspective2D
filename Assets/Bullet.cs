using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 20;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }


     void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Dragon dragon = hitInfo.GetComponent<Dragon>();

        if(dragon != null)
        {
            dragon.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
