using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoDrop : MonoBehaviour {

    //Dzięki temu mamy rodzaje amunicji w prostej liście zwijanej w unity w inspector
    public enum AmmoTypes { rifle, pistol, machine, moreToBeAdded};

    public AmmoTypes ammoType = AmmoTypes.rifle;
    public int count = 1;
    

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.transform.name);
        Player_Actions playerActions = col.transform.GetComponent<Player_Actions>();
        
        //Sprawdzamy czy to jest player, poprzez sprawdzenie czy posiada skrypt Player_Actions
        if (playerActions != null)
        {
            playerActions.pickUpAmmo(ammoType, count);

            Destroy(this.gameObject);
        }
        
    }
}
