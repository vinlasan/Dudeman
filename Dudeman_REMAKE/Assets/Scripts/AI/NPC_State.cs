using UnityEngine;
using System.Collections;

public abstract class NPC_State<T> {
	public virtual void Enter(T npc){}

	public virtual void Execute(T npc){}

	public virtual void Exit(T npc){}

}
