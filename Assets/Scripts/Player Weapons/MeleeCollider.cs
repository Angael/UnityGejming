using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour {

    private Melee meleeScript;
	// Use this for initialization
	void Start () {
        meleeScript = GetComponentInParent<Melee>();
    }
    
    public void OnTriggerStay2D(Collider2D col) {
        meleeScript.trigger(col);
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        meleeScript.trigger(col);
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        meleeScript.trigger(col);
    }

}
