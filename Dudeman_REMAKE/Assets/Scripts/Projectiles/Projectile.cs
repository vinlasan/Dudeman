﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected float fltThrowSpeed;
	protected float fltDamage;

	public bool blnThrown;
	public Vector3 vecThrowDirection;

	void Start(){
		blnThrown = false;
	}

	void Update(){
		if (blnThrown) {
			Physics.IgnoreCollision (this.gameObject.GetComponent<Collider> (), BossMan.GetInstance.player.GetComponent<Collider> ());
			this.gameObject.GetComponent<Collider> ().isTrigger = false;
			this.transform.position = (new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z) + vecThrowDirection*Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other){ //On contact with the player alert the BossMan that this object can be grabbed by ther player
		if (other.CompareTag ("Player")) {
			BossMan.GetInstance.updateProjectile = this.gameObject;
			BossMan.GetInstance.blnCanGrab = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			BossMan.GetInstance.blnCanGrab = false;
			BossMan.GetInstance.updateProjectile = null;
		}
	}
}
