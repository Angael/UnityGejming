using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerScream : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
}
