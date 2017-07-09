using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponMelee : MonoBehaviour {

    public float DamagePerHit = 5f;
    public float HitsPerSecond = 1f;

    private float timeOfNextHit = 0f;

    public EdgeCollider2D dmgRange;
    

    void OnTriggerStay2D(Collider2D col)
    {
        //other.attachedRigidbody.AddForce(-0.1F * other.attachedRigidbody.velocity);
        //Debug.Log(other.gameObject);
        

        if (Time.time > timeOfNextHit)
        {

            //Debug.Log(col.gameObject.name);
            if (col.gameObject.tag != "Player")
            {
                //Debug.Log(col.gameObject.name);
                return;
            }
                
            //parent parent, bo wykrywa knife/rifle itp, więc parent(weaponHolder).parent(player)
            PlayerHp playerHealthScript = col.transform.parent.parent.GetComponent<PlayerHp>(); 

            if (playerHealthScript == null)
                return;

            playerHealthScript.TakeDamage(DamagePerHit);

            //Jeśli wykonany został cios to dajemy czas w którym będzie możliwy następny
            timeOfNextHit = Time.time + 1 / HitsPerSecond;
            
        }
    }
}
