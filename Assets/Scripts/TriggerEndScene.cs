using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndScene : MonoBehaviour {

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SingleTon.instance.EndGame();
        }
    }
}
