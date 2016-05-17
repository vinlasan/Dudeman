using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public GameObject goPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = new Vector3 (goPlayer.transform.position.x - 0.5f, goPlayer.transform.position.y - 0.5f, -10);
	}
}
