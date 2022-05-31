using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour, Interactable
{
	public Rigidbody2D rb;
	public Animator animator;
	public bool canMove = false;
	public int leaveOnQuestLevel = 300;

	Vector2 movement;

	private Vector2 startPosition;
	private int interval = 6;
	private float nextTime = 0;
	private RaycastHit2D[] m_Contacts = new RaycastHit2D[100];

	public Dialog[] dialog;
	private Dialog tempDia;

	/// <summary>
    /// Wird beim Start ausgeführt
    /// Setzt die startPositionVariable um die der NPC Läuft
    /// </summary>
	void Start()
	{
		startPosition = rb.position;
	}

	/// <summary>
    /// Prüft, ob der Spieler ein gewisses Questlevel erreicht hat
    /// Falls dem so ist, wird der NPC zerstört
    /// </summary>
	void Update()
	{
		if(leaveOnQuestLevel < GameObject.FindGameObjectWithTag("Player").GetComponent<QuestHandler>().QuestProgress)
        {
			Destroy(gameObject);
        }
	}

	/// <summary>
    /// Interaction zwischen Spieler und NPC
    /// Hierfür bleibt der NPC stehen
    /// Es wird der Dialog gestartet, der für das QuestLevel des Spielers vorgeschrieben ist
    /// </summary>
	public void Interact()
	{
		movement.x = 0; // Input.GetAxisRaw("Horizontal");
		movement.y = 0; //Input.GetAxisRaw("Vertical");

		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetFloat("Speed", movement.sqrMagnitude);


		if(dialog.Length>= 1)
        {
			tempDia = dialog[0];

			foreach (Dialog dial in dialog)
			{

				if ((dial.QuestProgressRequirements > tempDia.QuestProgressRequirements) && (dial.QuestProgressRequirements <= GameObject.FindGameObjectWithTag("Player").GetComponent<QuestHandler>().QuestProgress))
				{

					tempDia = dial;
				}

			}
			if (tempDia.QuestProgressID == GameObject.FindGameObjectWithTag("Player").GetComponent<QuestHandler>().QuestProgress)
			{
				if(2 == GameObject.FindGameObjectWithTag("Player").GetComponent<QuestHandler>().QuestProgress)
                {
					AmountManager.instance.ChangeScore(2);
					Debug.Log("Increase" + 2 + " / "+GameObject.FindGameObjectWithTag("Player").GetComponent<QuestHandler>().QuestProgress);
				}
				

				GameObject.FindGameObjectWithTag("Player").GetComponent<QuestHandler>().IncreaseProgress();
				
			}

			DialogManager.Instance.ShowDialog(tempDia);
		}		
	}

	/// <summary>
    /// Wird genau X mal pro Minute aufgerufen
    /// Movement in Zufallsrichtung mit ändernder Wahrscheinlichkeit, damit der NPC um die StartPosition läuft
    /// </summary>
	void FixedUpdate()
	{
		if(DialogManager.Instance != null)
        {
  			if (DialogManager.Instance.dialogBox.activeSelf == false)
			{
				
				if (Time.time >= nextTime && canMove)
				{
					movement.x = (Random.Range(-1f, 1f) - (rb.position.x - startPosition.x) / 10) / 80;
					movement.y = (Random.Range(-1f, 1f) - (rb.position.y - startPosition.y) / 10) / 80;

					animator.SetFloat("Horizontal", (float)Mathf.Sign(movement.x));
					animator.SetFloat("Vertical", Mathf.Sign(movement.y));
					animator.SetFloat("Speed", 1);

					nextTime += Random.Range(2, interval);
				}
				DialogManager.Instance.ResetDialog();
			}
		}

		var direction = movement.normalized;


		var resultCount = rb.Cast(direction.normalized, m_Contacts, movement.magnitude);

		for (var i = 0; i < resultCount; ++i)
		{
			var contact = m_Contacts[i];
			var distance = contact.distance;

			//Bewegen wir uns?
			if (distance > Mathf.Epsilon)
			{
				//Falls ja, dann geh den ersten Schritt
				rb.MovePosition(rb.position + (direction * distance));
				return;
			}
			//Falls wir gegen das Hinternis kommen, beende die Bewegung
			else if (Vector2.Dot(contact.normal, direction) < 0)
				return;
		}

		//Falls kein Hinternis gefunden wurde, move die volle Distanz
		rb.MovePosition(rb.position + movement);
	}

}