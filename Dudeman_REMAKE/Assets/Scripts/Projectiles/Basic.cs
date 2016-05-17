using UnityEngine;
using System.Collections;

public class Basic : Projectile {

	// Use this for initialization
	void Start () {
		fltThrowSpeed = 2.0f;
		fltDamage = 1.2f;
		blnThrown = false;
	}

	void FixedUpdate () {
		if (blnThrown) {
			this.gameObject.transform.position += (Vector3)vecThrowDirection;
		}
	}
}
