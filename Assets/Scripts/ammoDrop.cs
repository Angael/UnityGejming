using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoDrop : MonoBehaviour {

    //Dzięki temu mamy rodzaje amunicji w prostej liście zwijanej w unity w inspector
    public enum AmmoTypes { light, medium, heavy, arrows, rocket, scroll, rocks, pancakes };

    public AmmoTypes ammoType = AmmoTypes.light;
    public int count = 1;
    

    void OnCollisionEnter2D(Collision2D col)
    {
        
        Player_Actions playerActions = col.transform.GetComponent<Player_Actions>();
        
        //Sprawdzamy czy to jest player, poprzez sprawdzenie czy posiada skrypt Player_Actions
        if (playerActions != null)
        {
            playerActions.pickUpAmmo(this);

            Destroy(this.gameObject);
        }
        
    }
}
//public class ammoDropObject
//{

//    ammoDropObject()
//    {

//    }
//}
