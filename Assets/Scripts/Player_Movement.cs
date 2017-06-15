using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
	//Zmienne widoczne w Unity:

	//przyspieszenie i zwalnianie:
	//Aby zmienić prędkość gracza trzeba bawić się 
	//Linear Drag w Rigidbody2D
	//i acceleration:
	public int acceleration = 100;

	//bigger rotationAcceleration = faster rotate
	public int rotationAcceleration = 19;
	public Transform target; //Assign to the object you want to rotate

	//movement private variables
	private Rigidbody2D rb;

	//rotation private variables
	private Vector3 mouse_pos;
	private Vector3 object_pos;
	private float angle;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		RotatePlayer();
		
	}
	void FixedUpdate()
    {
		MovePlayer();
			
	}

	//Obraca gracza w kierunku myszki, wygładzając ruch
	//(skrypt z neta)
	private void RotatePlayer(){
		mouse_pos = Input.mousePosition;
		mouse_pos.z = 5.23f; //The distance between the camera and object
		object_pos = Camera.main.WorldToScreenPoint(transform.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;
		angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
		//transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), Time.deltaTime * rotationAcceleration);
	}

	private void MovePlayer(){

		float xButton = Input.GetAxisRaw("Horizontal");
		float yButton = Input.GetAxisRaw("Vertical");

		Vector2 moveVectorForce = new Vector2(xButton*acceleration, yButton*acceleration);
		rb.AddForce(moveVectorForce);
		//Debug.Log(moveVectorForce);
	}

	
}
