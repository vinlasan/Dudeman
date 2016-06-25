using UnityEngine;
using System.Collections;

public abstract class NPC_Base : MonoBehaviour {
	public int intHealth;
	public int intDamage;
	public float fltCooldown;

	public bool blnAlive, blnExecBehavior;

	public abstract void TakeDamage(int Amount);
	public abstract IEnumerator DecisionMaker();
}
