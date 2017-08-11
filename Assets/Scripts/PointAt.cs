using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAt : MonoBehaviour {
    public Transform player;
    public bool lookAtMouse = true;

    public float x;
    public float y;
    public float z;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (lookAtMouse)
        {
            //Vector3 mouse_pos = Input.mousePosition;
            //mouse_pos.z = 5.23f; //The distance between the camera and object
            //Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
            //mouse_pos.x = mouse_pos.x - object_pos.x;
            //mouse_pos.y = mouse_pos.y - object_pos.y;
            //float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            ////transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(60, 0, angle)), Time.deltaTime * 5);
            Vector3 mouse_pos = Input.mousePosition;
            Debug.DrawLine(Vector3.zero, Camera.main.ScreenToWorldPoint(new Vector3(mouse_pos.x, mouse_pos.y, /*Camera.main.nearClipPlane*5*/Vector3.Distance(player.position,Camera.main.transform.position) )), Color.yellow);

            Camera.main.WorldToScreenPoint(transform.position);
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
            Debug.DrawLine(Vector3.zero, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)), Color.blue );
            //moje notatki
            //worldtoscreenpoint daje taka wartosc x y pikseli na ekranie, od lewego dolnego rogu, do objektu na kamerze - pamiętaj że jest to naprawde duże, widać na canvas kamerze

            float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            y = angle;
            //transform.rotation = Quaternion.Euler(new Vector3(z, y, z));
            transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(mouse_pos.x, mouse_pos.y, Vector3.Distance(player.position, Camera.main.transform.position) + 1.1f)));
            
        }
        else
        {
            //Vector3 objPos = lookAt.position;
            //objPos.z = -(transform.position.x - lookAt.position.x);
            //Vector3 worldPos = Camera.main.ScreenToWorldPoint(objPos);
            //transform.LookAt(lookAt);
        }
        
    }
}
