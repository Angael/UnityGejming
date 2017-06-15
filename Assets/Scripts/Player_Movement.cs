using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
	//Zmienne widoczne w Unity:
	//przyspieszenie i zwalnianie:
	public int acceleration = 12;
	public int rotationAcceleration = 1;
	private float minimalSpeed = 0f;
	public float xVelocity = 0f;
	public float yVelocity = 0f;
	public float maxSpeed = 5f;
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

		rb.AddForce(new Vector2(maxSpeed*xButton*acceleration, maxSpeed*yButton*acceleration) );
		//xVelocity = Mathf.Lerp(xVelocity, maxSpeed*xButton, Time.deltaTime*acceleration);
		//yVelocity = Mathf.Lerp(yVelocity, maxSpeed*yButton, Time.deltaTime*acceleration);

		//rb.velocity = new Vector2(xVelocity,
		//							yVelocity
		//							);

			Debug.Log(rb.velocity);
	}

	
}
