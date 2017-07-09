using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCanvasRotation : MonoBehaviour {

    public Transform parent;
    private Vector3 relativeToParent;
	// Use this for initialization
	void Start () {
        parent = transform.parent;
        relativeToParent = transform.position - parent.position;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //Rotate, so it's always on top of enemy ( that means: Don't rotate with enemy)
        //transform.position = (Vector2)parent.position + Vector2.up * 1;
        ////Pasek hp nie powinien być przysłaniany przez wrogów więc:
        //transform.position += Vector3.back*0.1f;

        transform.position = parent.position + relativeToParent;
        transform.rotation = Quaternion.identity;
	}
}
