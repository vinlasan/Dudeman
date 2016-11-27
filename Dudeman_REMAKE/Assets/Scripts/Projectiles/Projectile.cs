using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected float fltThrowSpeed;
	protected float fltDamage;
    public float despawnTimer;

    public bool blnThrown,
        rip;
	public Vector3 vecThrowDirection;

	void Start(){
		blnThrown = rip = false;
		vecThrowDirection = new Vector3(0, 0, 0);
        despawnTimer = 3.2f;
	}

	protected virtual void Update(){
		if (blnThrown) {
            ApplyDirection();
        }
	}

    public virtual void Setup()
    {
        StartCoroutine(Despawn());
    }

    protected virtual void ApplyDirection()
    {
        this.gameObject.transform.position += vecThrowDirection * fltThrowSpeed * Time.deltaTime;
    }

    protected virtual IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTimer);
        rip = true;
        DestroyImmediate(this.gameObject);
    }

	void OnTriggerEnter(Collider other){ //On contact with the player, alert the BossMan that this object can be grabbed by the player
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

	void OnCollisionEnter (Collision other){
		
	}
}
