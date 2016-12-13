using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public int playerID = 0;

    private float fltMoveSpeed, fltGrabSpeed, fltJumpAccel, fltJumpStart;

    public float fltGravity, fltMaxJump;
    public bool blnIsJumping, blnGrounded, blnCanJump, blnHolding, blnThrow, blnCanGrab;

    public GameObject goProjectile;
    Vector3 vecGravityCalc, vecThrowDirection, vecProjectileOffset;
    Rigidbody rgdPlayer;

    // Use this for initialization
    void Start()
    {
        fltMoveSpeed = 10.0f;
        fltMaxJump = 3f;
        fltJumpAccel = 12.0f;
        fltGravity = -15.0f;
        vecGravityCalc = new Vector3(0, fltGravity, 0);
        blnIsJumping = blnHolding = blnGrounded = blnThrow = blnCanJump = blnCanGrab = false;
        rgdPlayer = GetComponent<Rigidbody>();
        vecThrowDirection = new Vector3(25, 0, 0);
        vecProjectileOffset = new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (blnHolding)
            goProjectile.transform.position = (this.transform.position + vecProjectileOffset);  //new Vector3 (this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z); 
    }

    void FixedUpdate()
    {
        rgdPlayer.AddForce(vecGravityCalc);
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + fltMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            vecThrowDirection = new Vector3(25, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - fltMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            vecThrowDirection = new Vector3(-25, 0, 0);
        }
        /*if (Input.GetKey (KeyCode.S)) {//maybe put a stomp in there
			if (blnIsJumping)
				transform.position = new Vector3 (transform.position.x, transform.position.y - fltMoveSpeed*1.5f, transform.position.z);
				//rgdPlayer.AddForce(new Vector3(0, -30, 0));
		}*/

        if (Input.GetKeyDown(KeyCode.W) && blnGrounded && !blnIsJumping)
        { //Jumping
            fltJumpStart = transform.position.y;
            blnIsJumping = true;
            blnGrounded = false;
        }

        if (Input.GetKey(KeyCode.W) && blnIsJumping)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !blnHolding) //Grabbing
            BossMan.GetInstance.GrabProjectile();
        else if (Input.GetKeyDown(KeyCode.Space) && blnHolding)
        { //TODO make it so that you can't pick up another object in the same frame you throw an object
            StartCoroutine(Throw());
        }
    }

    void Jump()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + fltJumpAccel * Time.deltaTime, fltJumpStart, fltMaxJump), transform.position.z);
        //if (Input.GetKey (KeyCode.W) && blnIsJumping && transform.position.y >= (fltJumpStart + fltMaxJump)) {
        //	fltMaxJump = 4.0f;
        //	fltJumpAccel = 10.0f;
        //}
        //if (transform.position.y >= (fltJumpStart + fltMaxJump) || Input.GetKeyUp(KeyCode.W)) {
        //	StartCoroutine (JumpCooldown ());
        //	fltMaxJump = 3.0f;
        //	fltJumpAccel = 12.0f;
        //}
    }

    IEnumerator JumpCooldown()
    {
        blnIsJumping = false;
        yield return new WaitForSeconds(1.5f);
        blnCanJump = true;
    }

    IEnumerator Throw()
    {
        if (BossMan.GetInstance.currentProjectile != null)
        {
            blnHolding = false;
            BossMan.GetInstance.ThrowProjectile(BossMan.GetInstance.updateProjectile.GetComponent<Projectile>(), vecThrowDirection);
        }
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator GrabCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        blnCanGrab = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            blnGrounded = true;
        }

        if (col.collider.CompareTag("Projectile"))
        {

        }
    }
}
