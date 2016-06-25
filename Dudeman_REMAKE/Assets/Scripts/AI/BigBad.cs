using UnityEngine;
using System.Collections;

public class BigBad : NPC_Base
{
	State_Manager<BigBad> StateManager;


	// Use this for initialization
	void Start ()
	{
	
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

