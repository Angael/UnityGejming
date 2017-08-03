using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Actions : MonoBehaviour {

    //PLAYER ACTION to skrypt w którym powinny być:
    //Akcje wykonane po kliknięciu przycisków klawiatury i myszki


    public WeaponHolder weaponHolder;
    

    //Wszystkie przyciski numeryczne 1-9
    private KeyCode[] keyCodes = {
                 KeyCode.Alpha1,
                 KeyCode.Alpha2,
                 KeyCode.Alpha3,
                 KeyCode.Alpha4,
                 KeyCode.Alpha5,
                 KeyCode.Alpha6,
                 KeyCode.Alpha7,
                 KeyCode.Alpha8,
                 KeyCode.Alpha9,
             };
    
	// Update is called once per frame
	void Update () {
        //shooting / slashing/ throwing fireball
		if (Input.GetMouseButton(0)){
			//Debug.Log("Pressing left click.");
            weaponHolder.leftClick();
		}
            
        //not used
        if (Input.GetMouseButtonDown(1)){
			//Debug.Log("Pressed right click.");
		}
            
        //not used
        if (Input.GetMouseButtonDown(2)){
			//Debug.Log("Pressed middle click.");
		}

        if(Input.GetKeyDown(KeyCode.R))
        {
            weaponHolder.r();
            //Debug.Log("r");
        }

        //scroll wheel for selecting weapons
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            weaponHolder.ChangeWeapon("next");
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            weaponHolder.ChangeWeapon("previous");
        }

        //numbers for selecting weapons
        for (int i = 0; i<keyCodes.Length; i ++ ){
            if(Input.GetKeyDown(keyCodes[i])){
                int numberPressed = i + 1;
                weaponHolder.ChangeWeapon(numberPressed - 1);
            }
        }
         // WUCC check for errors. 
         
    }
	
    //Kiedy gracz podniesie amunicje, argumenty: do jakiej broni i ile tej amunicji
    public void pickUpAmmo(ammoDrop AmmoDrop)
    {
        weaponHolder.pickUpAmmo(AmmoDrop); // do dokończenia
        
    }
}
