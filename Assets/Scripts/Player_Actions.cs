using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Actions : MonoBehaviour {

    //PLAYER ACTION to skrypt w którym powinny być:
    //Akcje wykonane po kliknięciu przycisków klawiatury i myszki

    public GameObject spriteUsed;
    public LineRenderer shootLine;
    public GameObject shootParticles;
    public WeaponHolder weaponHolder;

    private Transform gunPoint;
    private float timeOfNextShot = 0;


    void Start () {
		//Zapisz miejsce lufy w naszym "sprajcie", bo player strzela z lufy a nie oczu
		gunPoint = spriteUsed.transform.GetChild(0).transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)){
			Debug.Log("Pressing left click.");
            weaponHolder.leftClick();
		}
            
        
        if (Input.GetMouseButtonDown(1)){
			Debug.Log("Pressed right click.");
		}
            
        
        if (Input.GetMouseButtonDown(2)){
			Debug.Log("Pressed middle click.");
		}
            
    }
	
}
