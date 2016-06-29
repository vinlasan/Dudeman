using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public GameObject goPlayer;
	public float fltCameraBounds, fltScreenWidth, fltScreenHeight, fltTransitionSpeed;

	// Use this for initialization
	void Start () {
		fltCameraBounds = Screen.width/2;
		fltScreenHeight = Screen.height;
		fltScreenWidth = Screen.width;
		fltTransitionSpeed = 1.5f;
	}

	// Update is called once per frame
	void Update () {
		if (goPlayer.transform.position.x > fltScreenWidth - fltCameraBounds)
			this.transform.position =  new Vector3 (goPlayer.transform.position.x - (fltTransitionSpeed * Time.deltaTime) , goPlayer.transform.position.y, this.transform.position.z);

		if (goPlayer.transform.position.x < fltScreenWidth + fltCameraBounds)
			this.transform.position =  new Vector3 (goPlayer.transform.position.x + (fltTransitionSpeed * Time.deltaTime) , goPlayer.transform.position.y, this.transform.position.z);

		//this.gameObject.transform.position = new Vector3 (goPlayer.transform.position.x - 0.5f, goPlayer.transform.position.y + 1.5f, -10);
	}
}
