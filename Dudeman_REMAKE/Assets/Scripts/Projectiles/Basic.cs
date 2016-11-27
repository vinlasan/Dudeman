using UnityEngine;
using System.Collections;

public class Basic : Projectile {

	// Use this for initialization
	void Start () {
		fltThrowSpeed = 0.85f;
		fltDamage = 1.2f;
		blnThrown = false;
        despawnTimer = 4.0f;
	}

	protected override void Update () {
        base.Update();
	}
}
