﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	float fltMoveSpeed;
	float fltGrabSpeed;
	float fltMaxJump;
	float fltJumpAccel;
	float fltJumpStart;
	public float fltGravity;
	Vector3 vecGravityCalc, vecThrowDirection;

	public bool blnIsJumping, blnGrounded;
	bool blnHolding, blnInteractable, blnGrab;

	public GameObject goProjectile;
	Rigidbody rgdPlayer;

	// Use this for initialization
	void Start () {
		fltMoveSpeed = 0.1f;
		fltMaxJump = 2f;
		fltJumpAccel = 0.25f;
		fltGravity = -5.00f;
		vecGravityCalc = new Vector3 (0, fltGravity, 0);
		blnIsJumping = blnHolding = blnGrounded = blnGrab = blnInteractable = false;
		rgdPlayer = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		
		GetInput ();
		rgdPlayer.AddForce(vecGravityCalc);
	}

	void GetInput(){
		if (Input.GetKey (KeyCode.D)) {
			transform.position = new Vector3 (transform.position.x + fltMoveSpeed, transform.position.y, transform.position.z);
			vecThrowDirection = new Vector3 (5, 0, 0);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position = new Vector3 (transform.position.x - fltMoveSpeed, transform.position.y, transform.position.z);
			vecThrowDirection = new Vector3 (-5, 0, 0);
		}
		if (Input.GetKey (KeyCode.S)) {
			if (blnIsJumping)
				fltGravity = -20.0f;
		}

		if (Input.GetKeyDown (KeyCode.W) && blnGrounded && !blnIsJumping) { //TODO: make it so that jump can be used only once
			fltJumpStart = transform.position.y;
			blnIsJumping = true;
			blnGrounded = false;
		} 
		if (Input.GetKey (KeyCode.W) && blnIsJumping) { //Unintended behavior causing the player to be able to jump only twice before cancelling their ability to jump
				transform.position = new Vector3 (transform.position.x, transform.position.y + fltJumpAccel, transform.position.z);

			if (transform.position.y >= (fltJumpStart + fltMaxJump)) {
				StartCoroutine (Jump ());
			}
		}

		if (Input.GetKey (KeyCode.Space) && blnHolding) {

		}
	}



	IEnumerator Jump()//TODO: create delay between jumps
	{
		yield return new WaitForSeconds (1.0f);
		blnIsJumping = false;
	}

	void OnCollisionEnter(Collision col){
		if(col.collider.CompareTag("Ground")){
			blnGrounded = true;
		}

		if (col.collider.CompareTag ("Projectile")) { // Change to check if they are colliding with hands
			if (Input.GetKey (KeyCode.Space)) 
				goProjectile = col.gameObject;
		}

	}
}