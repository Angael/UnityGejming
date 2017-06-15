using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public Transform whatToFollow;
	
	void Start () {
		
	}
	
	void Update () {
		// poniewaz gra 2d to oś Z jest stała
		transform.position = new Vector3(whatToFollow.position.x,whatToFollow.position.y, -10);
	}
}
