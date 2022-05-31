using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
	public Animator anim;
    Vector2 movement;

	public float currentHorizontal = 0f;
	public float currentVertical = 0f;

	public GameObject sword;
    public GameObject swordBehind;
	
    void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

	
		anim.SetFloat("Horizontal", movement.x);
		anim.SetFloat("Vertical", movement.y);
		anim.SetFloat("Speed", movement.sqrMagnitude);

		currentHorizontal = anim.GetFloat("Horizontal");
		currentVertical = anim.GetFloat("Vertical");

    
        if(currentHorizontal == -1 || currentVertical == 1){
                swordBehind.SetActive(true); 
                sword.SetActive(false);
            } else {
				swordBehind.SetActive(false);
			}

     }

	void FixedUpdate() 	{

		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
      
     }
 }


