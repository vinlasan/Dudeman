using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float fltMoveSpeed, fltGrabSpeed, fltMaxJump, fltJumpAccel, fltJumpStart;

	public float fltGravity;
	public bool blnIsJumping, blnGrounded, blnHolding, blnThrow;

	public GameObject goProjectile;
	Vector3 vecGravityCalc, vecThrowDirection;
	Rigidbody rgdPlayer;

	// Use this for initialization
	void Start () {
		fltMoveSpeed = 0.1f;
		fltMaxJump = 3f;
		fltJumpAccel = 0.25f;
		fltGravity = -15.00f;
		vecGravityCalc = new Vector3 (0, fltGravity, 0);
		blnIsJumping = blnHolding = blnGrounded = blnThrow = false;
		rgdPlayer = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		GetInput ();
		if(blnIsJumping)
			StartCoroutine (Jump ());
		if(blnHolding)
			goProjectile.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z); 
	}

	void FixedUpdate(){
		rgdPlayer.AddForce(vecGravityCalc);
	}

	void GetInput(){
		if (Input.GetKey (KeyCode.D)) {
			transform.position = new Vector3 (transform.position.x + fltMoveSpeed, transform.position.y, transform.position.z);
			vecThrowDirection = new Vector3 (25, 0, 0);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position = new Vector3 (transform.position.x - fltMoveSpeed, transform.position.y, transform.position.z);
			vecThrowDirection = new Vector3 (-25, 0, 0);
		}
		if (Input.GetKey (KeyCode.S)) {
			if (blnIsJumping)
				rgdPlayer.AddForce(new Vector3(0, -30, 0));
		} 

		if (Input.GetKeyDown (KeyCode.W) && blnGrounded && !blnIsJumping) { 
			fltJumpStart = transform.position.y;
			blnIsJumping = true;
			blnGrounded = false;
		} 

		if (Input.GetKeyDown (KeyCode.Space) && !blnHolding)
			BossMan.GetInstance.GrabProjectile ();
		
		else if (Input.GetKeyDown (KeyCode.Space) && blnHolding) {
				Throw ();
		}
	}



	IEnumerator Jump()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y + fltJumpAccel, transform.position.z);
		if (Input.GetKey (KeyCode.W) && blnIsJumping && transform.position.y >= (fltJumpStart + fltMaxJump)) {
			fltMaxJump = 4.0f;
			fltJumpAccel = 0.1f;
		}

		if (transform.position.y >= (fltJumpStart + fltMaxJump) || Input.GetKeyUp(KeyCode.W)) {
			blnIsJumping = false;
			fltMaxJump = 3.0f;
			fltJumpAccel = 0.25f;
		}
		yield return new WaitForSeconds (1.0f);

	}

	void Throw(){ //TODO figure out how to not throw projectile into oblivion
		//yield return new WaitForSeconds (1.0f);
		blnHolding = false;
		BossMan.GetInstance.ThrowProjectile (BossMan.GetInstance.updateProjectile.GetComponent<Projectile>(), vecThrowDirection);
	}

	void OnCollisionEnter(Collision col){
		if(col.collider.CompareTag("Ground")){
			blnGrounded = true;
		}

		if (col.collider.CompareTag ("Projectile")) { 
			
		}
	}
}