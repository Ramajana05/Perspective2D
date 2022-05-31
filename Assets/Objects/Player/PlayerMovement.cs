using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Joystick joystick;
    public Image joystickInner;
    public Image joystickOuter;

    public Rigidbody2D rb;

    //Animations sachen
    public Animator animator;
    public Animator pinkHairAnimator;
    public Animator brownHairAnimator;
    public Animator helmetHairAnimator;
    public Animator pinkOutfitAnimator;
    public Animator brownOutfitAnimator;
    public Animator armorOutfitAnimator;
    public Animator retroArmorOutfitAnimator;
    public Animator eyesSmallAnimator;
    public Animator eyesLongAnimator;

    //Schwert Sachen
    public Animator swordAnimator;
    public Animator swordBackAnimator;
    public GameObject sword;
    public GameObject swordBehind;
    public float currentHorizontal = 0f;
    public float currentVertical = 0f;


    public Animator swordAnimatorThis;

    private GameObject[] allBodyParts;
    private CircleCollider2D cc2D;
    public GameObject DialogBox;
    private GameObject Player;

    public Vector2 movement;
    private RaycastHit2D[] m_Contacts = new RaycastHit2D[100];


    /// <summary>
    /// Wird beim Start ausgefuehrt
    /// Variablen werden gesetzt
    /// Player wird auf die Startposition gesetzt
    /// </summary>
    void Start()
    {
        allBodyParts = GameObject.FindGameObjectsWithTag("PlayerBody");

        cc2D = GetComponent<CircleCollider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");

        if (LoadingScreen.Instance.SpawnPosition.x == 0 && LoadingScreen.Instance.SpawnPosition.y == 0)
        {
            //Lade letzte Szene in der sich der Spieler befindet
            if (SceneManager.GetActiveScene().name != GetComponent<LoadPlayer>().currentScene)
            {
                SceneManager.LoadScene(GetComponent<LoadPlayer>().currentScene);
            }

            //Setze den Spieler auf die letzte gespeicherte Postition
            rb.position = (new Vector2(GetComponent<LoadPlayer>().locationX, GetComponent<LoadPlayer>().locationY));
        }
        else
        {
            rb.position = LoadingScreen.Instance.SpawnPosition;
        }
    }

    /// <summary>
    /// Prueft auf Bewegungsinput und setzt die movement Variablen
    /// </summary>
    void Update()
    {
        Animator anim;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            joystickInner.enabled = true;
            joystickOuter.enabled = true;
            if (joystick.Horizontal >= .2f)
            {
                movement.x = 1f;
            }
            else if (joystick.Horizontal <= -.2f)
            {
                movement.x = -1f;
            }

            if (joystick.Vertical >= .2f)
            {
                movement.y = 1f;
            }
            else if (joystick.Vertical <= -.2f)
            {
                movement.y = -1f;
            }
        }
        else
        {
            Destroy(joystick);
            joystickInner.enabled = false;
            joystickOuter.enabled = false;
        }

        foreach (GameObject bodypart in allBodyParts)
        {
            anim = bodypart.GetComponent<Animator>();
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);
        }

        //Animator fuer jedes Objekt
        foreach (GameObject bodypart in allBodyParts)
        {
            if (bodypart.activeSelf == true)
            {
                Animator animb = bodypart.GetComponent<Animator>();
                animb.SetFloat("Horizontal", movement.x);
                animb.SetFloat("Vertical", movement.y);
                animb.SetFloat("Speed", movement.sqrMagnitude);
            }
        }

        //Schwert Sachen
        swordAnimatorThis.SetFloat("Horizontal", movement.x);
        swordAnimatorThis.SetFloat("Vertical", movement.y);
        swordAnimatorThis.SetFloat("Speed", movement.sqrMagnitude);

        swordAnimator.SetFloat("Horizontal", movement.x);
        swordAnimator.SetFloat("Vertical", movement.y);
        swordAnimator.SetFloat("Speed", movement.sqrMagnitude);

        swordBackAnimator.SetFloat("Horizontal", movement.x);
        swordBackAnimator.SetFloat("Vertical", movement.y);


        currentHorizontal = swordAnimator.GetFloat("Horizontal");
        currentVertical = swordAnimator.GetFloat("Vertical");

        if (currentHorizontal == -1 || currentVertical == 1)
        {
            swordBehind.SetActive(true);
            sword.SetActive(false);
        }
        else
        {
            swordBehind.SetActive(false);
        }

        if (movement.x != 0 || movement.y != 0)
        {
            DialogBox.SetActive(false);

        }

        if (Input.GetButtonDown("Fire3"))
        {
            Interact();
        }

    }

    /// <summary>
    /// Fuehrt die Bewegung aus
    /// </summary>
    void FixedUpdate()
    {
        movement.x = movement.x * moveSpeed;
        movement.y = movement.y * moveSpeed;

        var direction = movement.normalized;


        var resultCount = rb.Cast(direction.normalized, m_Contacts, movement.magnitude);

        for (var i = 0; i < resultCount; ++i)
        {
            var contact = m_Contacts[i];
            var distance = contact.distance;

            // Bewegen wir uns?
            if (distance > Mathf.Epsilon)
            {
                // Ja, also gehen wir weiter.
                rb.MovePosition(rb.position + (direction * distance));
                return;
            }
            // Wenn wir in ein Contact laufen dann soll es abbrechen.
            else if (Vector2.Dot(contact.normal, direction) < 0)
                return;
        }
        // Kein Contact wurde gefunden also bewege mit voller Geschwindigkeit.
        rb.MovePosition(rb.position + movement);


    }


    /// <summary>
    /// Wird aufgerufen, falls eine Interaction stattfindet
    /// </summary>
    public void Interact()
    {
        var interactPos = transform.position;
        Debug.Log(interactPos);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(interactPos, 1.3f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.transform.tag == "NPC" && collider != null)
            {
                Debug.Log("interactionObject");
                collider.GetComponent<Interactable>()?.Interact();
                break;
            }
        }
    }
}