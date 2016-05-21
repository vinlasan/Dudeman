using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	float fltMoveSpeed;
	float fltGrabSpeed;
	float fltMaxJump;
	float fltJumpAccel;
	float fltJumpStart;
	public float fltGravity;
	Vector3 vecGravityCalc, vecThrowDirection;

	public bool blnIsJumping, blnGrounded, blnHolding;

	public GameObject goProjectile;
	Rigidbody rgdPlayer;

	// Use this for initialization
	void Start () {
		fltMoveSpeed = 0.1f;
		fltMaxJump = 2.5f;
		fltJumpAccel = 0.25f;
		fltGravity = -15.00f;
		vecGravityCalc = new Vector3 (0, fltGravity, 0);
		blnIsJumping = blnHolding = blnGrounded = false;
		rgdPlayer = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		
		GetInput ();
		if(blnIsJumping)
			StartCoroutine (Jump ());
		rgdPlayer.AddForce(vecGravityCalc);
		if(blnHolding)
			goProjectile.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z); 
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
		/*if (Input.GetKey (KeyCode.W) && blnIsJumping) { //Unintended behavior causing the player to be able to jump only twice before cancelling their ability to jump
				transform.position = new Vector3 (transform.position.x, transform.position.y + fltJumpAccel, transform.position.z);

			if (transform.position.y >= (fltJumpStart + fltMaxJump)) {
				StartCoroutine (Jump ());
			}
		}*/

		if (Input.GetKey (KeyCode.Space))
			BossMan.GetInstance.GrabProjectile ();

		if (Input.GetKey (KeyCode.Space) && blnHolding) {
			BossMan.GetInstance.ThrowProjectile (BossMan.GetInstance.updateProjectile.GetComponent<Projectile>(), vecThrowDirection);
		}
	}



	IEnumerator Jump()//TODO: create delay between jumps
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y + fltJumpAccel, transform.position.z);

		if (transform.position.y >= (fltJumpStart + fltMaxJump)) {
			blnIsJumping = false;
		}
		yield return new WaitForSeconds (1.0f);

	}

	void OnCollisionEnter(Collision col){
		if(col.collider.CompareTag("Ground")){
			blnGrounded = true;
		}

		if (col.collider.CompareTag ("Projectile")) { 
			
		}

	}
}