using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public new Rigidbody2D rigidbody2D;
    public GameObject player;
    public Animator animator;
    public Vector2 target;
    public Vector2 enemyPosition;
    public Transform spawnPoint;
    public float chaseRange;
    public float attackRange;
    public GameObject potionPrefab;

    public RaycastHit2D[] m_Contacts = new RaycastHit2D[100];

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.asleep;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentState != EnemyState.dead)
        {
            checkDistance();
        }
    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn ein Gegner Schaden erhalten soll. Zudem ueberprueft diese Methode, ob ein Gegner stirbt oder nicht.
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

   /// <summary>
   /// Diese Methode ueberpruft die Distanz zwischem den Spieler und dem Gegner. Je nach Groeße der Distanz
   /// wird der Gegner in verschiedene Zustaende versetzt. Zudem wird die Richtung ermittelt und an weitere Funktionen 
   /// uebertragen.
   /// </summary>
    public virtual void checkDistance()
    {
        target = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Renderer>().bounds.center;
        enemyPosition = GetComponent<Renderer>().bounds.center;

        if (Vector2.Distance(target, enemyPosition) <= chaseRange && Vector2.Distance(target, enemyPosition) > attackRange)
        {
           if(currentState == EnemyState.asleep || currentState == EnemyState.walk && currentState != EnemyState.attack)
            {
                Vector2 temp = target - rigidbody2D.position;
                changeAnimation(temp);


                var direction = temp.normalized;

                temp = direction * movementSpeed / 80;

                var resultCount = rigidbody2D.Cast(direction.normalized, m_Contacts, temp.magnitude);

                for (var i = 0; i < resultCount; ++i)
                {
                    var contact = m_Contacts[i];
                    var distance = contact.distance;

                    // Are we actually moving?
                    if (distance > Mathf.Epsilon)
                    {
                        // Yes, so schedule the move.
                        rigidbody2D.MovePosition(rigidbody2D.position + (direction * distance));
                        return;
                    }
                    // If we're moving into a contact then finish as we cannot move.
                    else if (Vector2.Dot(contact.normal, direction) < 0)
                        return;
                }

                // No contact was found so move the full velocity.
                rigidbody2D.MovePosition(rigidbody2D.position + temp);



                changeState(EnemyState.walk);
            }

        }
        else if (Vector2.Distance(target, enemyPosition) <= chaseRange && Vector2.Distance(target, enemyPosition) <= attackRange)
        {
            if (currentState == EnemyState.walk || currentState == EnemyState.asleep)
            {
                Vector2 temp = target - rigidbody2D.position;
                animator.SetBool("wakeUp", true);
                StartCoroutine(AttackCo(temp));
            }
                
        }
        else if(Vector2.Distance(target, enemyPosition) > chaseRange)
        {
            if (currentState != EnemyState.attack && currentState != EnemyState.walk)
            {
                changeState(EnemyState.asleep);
                animator.SetBool("wakeUp", false);
            }
        }
    }

    /// <summary>
    /// Diese Methode wird aufgerufen sobald der Gegner in Angriffsreichweite steht. Diese Methode fuehrt die Angriffsanimation fuer den Gegner aus.
    /// </summary>
    /// <param name="direction">Die Richtung mit x- und y-Werten als Vector2 Variable</param>
    /// <returns>Ein IEnumeratorobjekt, welches fuer eine StartCoroutine()-Methode benoetigt wird.</returns>
    public IEnumerator AttackCo(Vector2 direction)
    {
        currentState = EnemyState.attack;
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1f/attacksPerSecond);
        player.GetComponent<Player>().TakeDamage(baseAttack);
        currentState = EnemyState.walk;
        animator.SetBool("isAttacking", false);
    }

    /// <summary>
    /// Diese Methode wird aufgerufen sobald der Gegner sterben soll. Diese Methode fuehrt die Todesanimation fuer den Gegner aus.
    /// </summary>
    /// <param name="direction">Die Richtung mit x- und y-Werten als Vector2 Variable</param>
    /// <returns>Ein IEnumeratorobjekt, welches fuer eine StartCoroutine()-Methode benoetigt wird.</returns>
    public IEnumerator DieCo(Vector2 direction)
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);

    }

    /// <summary>
    /// Dise Methode aendert die moveX und moveY Variable des Animator.
    /// </summary>
    /// <param name="vector">Der x- und y-Wert für die Richtung als Vetcor2 Variable</param>
    private void setAnimatorFloat(Vector2 vector)
    {
        animator.SetBool("wakeUp", true);
        animator.SetFloat("moveX", vector.x);
        animator.SetFloat("moveY", vector.y);

    }

    /// <summary>
    /// Diese Methode aendert die Richtung in welche die Animation abgespielt werden soll. Dazu wird anhand 
    /// der Richtung des Gegners ein x- und y-Wert uebergeben.
    /// </summary>
    /// <param name="direction">Die Richtung anhand einer Vector2 Variable</param>
    public void changeAnimation(Vector2 direction)
    {

            if(direction.x > 0)
            {
                setAnimatorFloat(Vector2.right);
            }
            else if(direction.x < 0)
            {
                setAnimatorFloat(Vector2.left);
            }

    }

    /// <summary>
    /// Diese Methode dient zur Aenderung des "currentState" des Gegners.
    /// </summary>
    /// <param name="nextState">EnemyState zur welches "currentState" geaendert werden soll</param>
    public void changeState(EnemyState nextState)
    {
        if(currentState != nextState)
        {
            currentState = nextState;
        }
    }

    public void getRendererCenter()
    {

    }

    /// <summary>
    /// Diese Methode wird aufgerufen, wenn der HP-Wert eines Gegners auf 0 faellt. Diese Methode, ob der gestorbene Gegner ein Drache war und laedt
    /// dementsprechend eine neue Szene. Falls es kein Drache ist, wird das GameObjekt geloescht.
    /// </summary>
    public override void Die()
    {
        if (Random.value < 0.2f)
        {
            Instantiate(potionPrefab, transform.position, transform.rotation);
        }

        changeState(EnemyState.dead);
        Vector2 temp = target - rigidbody2D.position;
        StartCoroutine(DieCo(temp));
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
