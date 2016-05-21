using UnityEngine;
using System.Collections;

public class BossMan : MonoBehaviour {

	private static BossMan instance = null;

	public static BossMan GetInstance {
		get { return instance; }
	}

	private Player player;
	public bool blnCanGrab;
	GameObject currentProjectile = null;
	public GameObject updateProjectile{
		get { return currentProjectile; }
		set { currentProjectile = value; } 
	}

	// Use this for initialization
	void Start () {
		instance = this;
		blnCanGrab = false;
		player = GameObject.FindWithTag("Player").GetComponent<Player> ();
	}

	public void GrabProjectile() //TODO thouroughly comment this shit homie so it's easier to revisit later
	{
		if (blnCanGrab && !player.blnHolding) {
			player.goProjectile = currentProjectile;
			player.blnHolding = true;
			blnCanGrab = false;
		}
	}

	public void ThrowProjectile(Projectile projectile, Vector3 direction)
	{
		projectile.blnThrown = true;
	}
}
