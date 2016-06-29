using UnityEngine;
using System.Collections;

public class BigBad : NPC_Base
{
	State_Manager<BigBad> StateManager;


	// Use this for initialization
	void Start ()
	{
		intHealth = 10;
		intDamage = 1;
		fltCooldown = 2.5f;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public override void TakeDamage(int Amount){
		intHealth -= Amount;
	}
	public override IEnumerator DecisionMaker(){

		yield return new WaitForSeconds (fltCooldown);
	}
}

