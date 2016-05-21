using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected float fltThrowSpeed;
	protected float fltDamage;

	public bool blnThrown;
	protected Vector2 vecThrowDirection;

	void Start(){
		blnThrown = false;
	}

	void Update(){
		if (blnThrown)
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z) + (Vector3)vecThrowDirection;
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			print ("player detected");
			BossMan.GetInstance.updateProjectile = this.gameObject;
			BossMan.GetInstance.blnCanGrab = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player"))
			BossMan.GetInstance.blnCanGrab = false;
	}
}
