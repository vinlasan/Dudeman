using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public int playerID = 0;

    public float fltMoveSpeed, fltGrabSpeed, fltJumpAccel, fltJumpStart;

    public float fltGravity, fltMaxJump, fltZPlane;
    public bool blnIsJumping, blnGrounded, blnCanJump, blnHolding, blnThrow, blnCanGrab;

    public GameObject goProjectile;
    Vector3 vecGravityCalc, vecThrowDirection, vecProjectileOffset, vecVelocity;
    public Vector3 velocity
    {
        get { return vecVelocity; }
        set { vecVelocity = value;}
    }
    Rigidbody rgdPlayer;
    InputManager inManager;

    void Start()
    {
        fltMoveSpeed = 5.0f;
        fltMaxJump = 3f;
        fltJumpAccel = 12.0f;
        fltGravity = -15.0f;
        vecGravityCalc = new Vector3(0, fltGravity, 0);
        blnIsJumping = blnHolding = blnGrounded = blnThrow = blnCanJump = blnCanGrab = false;
        rgdPlayer = GetComponent<Rigidbody>();
        vecThrowDirection = new Vector3(25, 0, 0);
        vecProjectileOffset = new Vector3(0, 0.5f, 0);
        inManager = gameObject.GetComponent<InputManager>();
    }

    //void Update()
    //{
    //    if (blnHolding)
    //        goProjectile.transform.position = (this.transform.position + vecProjectileOffset);  //new Vector3 (this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z); 
    //}

    void FixedUpdate()
    {
        rgdPlayer.MovePosition(rgdPlayer.position + vecVelocity * Time.fixedDeltaTime);

        if(inManager.blnJump)
        {
            
        }
    }

    void GetInput()
    {

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
}
