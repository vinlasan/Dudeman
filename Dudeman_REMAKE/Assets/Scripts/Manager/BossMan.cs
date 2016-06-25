using UnityEngine;
using System.Collections;

/// <summary>
/// BossMan acts as a messenger mainly between the Player and other objects
/// </summary>
public class BossMan : MonoBehaviour {

	private static BossMan instance = null;

	/// <summary>
	/// Provides global access to what should be a singular instance of BossMan
	/// </summary>
	public static BossMan GetInstance { 
		get { return instance; }
	}

	public Player player;
    public GameObject currentProjectile = null; 
	/// <summary>
	/// Use to update the current projectile to be used by the player
	/// </summary>
	/// <value>The update projectile.</value>
	public GameObject updateProjectile{ 
		get { return currentProjectile; }
		set { currentProjectile = value; } 
	}

	public bool blnCanGrab;

	// Use this for initialization
	void Start () {
		instance = this;
		blnCanGrab = false;
		player = GameObject.FindWithTag("Player").GetComponent<Player> ();
	}

	public void GrabProjectile() 
	{
		if (blnCanGrab && !player.blnHolding) { //Povided the player isn't holding something already, initiate grab
			player.goProjectile = currentProjectile;
			player.blnHolding = true;
			blnCanGrab = false;
		}
	}

	public void ThrowProjectile(Projectile projectile, Vector3 direction)
	{
		projectile.blnThrown = true;
		projectile.vecThrowDirection = direction;
		Physics.IgnoreCollision (projectile.gameObject.GetComponent<Collider> (), BossMan.GetInstance.player.GetComponent<Collider> ());
		projectile.gameObject.GetComponent<Collider> ().isTrigger = false;
	}
}
