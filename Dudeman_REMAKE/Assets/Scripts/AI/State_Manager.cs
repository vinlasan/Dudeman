using UnityEngine;
using System.Collections;

public class State_Manager<T> {

	T owner;
	NPC_State<T> currState, prevState, glblState;

	public State_Manager(T npc){
		owner = npc;
		currState = null;
		prevState = null;
		glblState = null;
	}

	public void UpdateState(){
		if (glblState != null)
			glblState.Execute (owner);
		if (currState != null)
			currState.Execute (owner);
	}

	public void ChangeState(NPC_State<T> newState){
		if (currState == null)
			currState = newState;

		if (newState != null) {
			prevState = currState;
			currState.Exit (owner);
			currState = newState;
			currState.Enter (owner);
		}
	}

	public void RevertPrevState(){
		ChangeState (prevState);
	}
}

