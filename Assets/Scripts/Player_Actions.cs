using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Actions : MonoBehaviour {

public GameObject spriteUsed;

private Transform gunPoint;

//PLAYER ACTION to skrypt w którym powinny być:
//Akcje wykonane po kliknięciu przycisków klawiatury i myszki
//
	// Use this for initialization
	void Start () {
		//Zapisz miejsce lufy w naszym "sprajcie"
		gunPoint = spriteUsed.transform.GetChild(0).transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)){
			Debug.Log("Pressing left click.");
			useWeapon();
		}
            
        
        if (Input.GetMouseButtonDown(1)){
			Debug.Log("Pressed right click.");
		}
            
        
        if (Input.GetMouseButtonDown(2)){
			Debug.Log("Pressed middle click.");
		}
            
    }

	//W przyszłości useWeapon ma móc obsłużyć więcej niż jedną broń
	//np. machanie mieczem, strzelanie, rzucanie fireballem, idk
	void useWeapon(){
		//Strzelanie, nie aplikuje sie do mieczy, magii itp.
		RaycastHit2D hit = Physics2D.Raycast(gunPoint.position, gunPoint.TransformDirection(Vector2.right), 5);
		float distance =0f;
        if (hit.collider != null) {
            distance = Vector2.Distance(gunPoint.transform.position, hit.point);
			Debug.Log("Hit something with tag: " + hit.transform.tag);
        }

		//DEBUG DRAW - pozwala widziec jak wygladal strzal
		Vector3 forward = gunPoint.TransformDirection(Vector2.right) * distance;
        Debug.DrawRay(gunPoint.position, forward, Color.green, 2);
	}
	
}
