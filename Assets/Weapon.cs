using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private Transform firePoint;
    public GameObject fireBallPrefab;
    private Player player;

    private GameObject sword;
    private GameObject swordBehind;

    private Animator animator;
    private Animator animator2;
    /// <summary>
    /// Diese Methode ist dazu da um Objekte in Variablen einzuspeichern
    /// </summary>
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //animator = GameObject.FindGameObjectWithTag("SwordFront").GetComponent<Animator>();
        // animator2 = GameObject.FindGameObjectWithTag("SwordBack").GetComponent<Animator>();
        sword = GameObject.FindGameObjectWithTag("SwordFront");
        swordBehind = GameObject.FindGameObjectWithTag("SwordBack");

    }

    /// <summary>
    /// Diese Methode ist dazu da dass zwei Waffen nicht gleichzeitig schiessen koennen
    /// </summary>
    void Update()
    {
        if (player.currentWeapon == 1)
        {
            sword.SetActive(true);
        }
        else
        {
            sword.SetActive(false);
            swordBehind.SetActive(false);
        }
        if (Application.platform != RuntimePlatform.Android)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                CheckWeaponAndShoot();
            }
        }
    }
    /// <summary>
    /// Diese Methode fuehrt einen Angriff aus basierend auf die ausgewaehlte Waffe
    /// </summary>
    public void CheckWeaponAndShoot()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.currentWeapon == 1)//sword is selected
        {
            MeleeAttack();
        }
        else if (player.currentWeapon == 2)
        {
            Shoot();
        }

    }
    /// <summary>
    /// Diese Methode ist dazu da damit der Feuerball abgefeuert werden kann
    /// </summary>
    public void Shoot()
    {
        if (player.currentMana >= 10)
        {
            firePoint = GameObject.FindGameObjectWithTag("Player").transform;
            player.TakeMana(10);

            GameObject new_Fireball = Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
            new_Fireball.layer = 15;

            FindObjectOfType<AudioManager>().PlayOneShot("FireBall");
        }
    }
    /// <summary>
    /// Diese Methode ist dazu da damit man mit dem Schwert angreifen kann
    /// </summary>
    public void MeleeAttack()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("SwordSound");

        //animator.SetTrigger("Attack");
        //animator2.SetTrigger("Attack");

        player.dealDamage();

        //animator.ResetTrigger("Attack");
        //animator2.ResetTrigger("Attack");
    }
}
