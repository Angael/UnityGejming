using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    public float damage;
    public float penetration;
    public GameObject hitEffect;
    public GameObject hitEffectBlood;

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("i hit something with name" + col.gameObject.name);
        //Debug.Log("LENGTH OF COLISIONS" + col.contacts.Length);
        Enemy enemyScript = col.transform.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.TakeDamage(damage, penetration);
            GameObject spawnedEffect = Instantiate(hitEffectBlood, col.contacts[0].point, Quaternion.LookRotation(transform.right));
            Destroy(spawnedEffect, 0.18f);
        }else
        {
            GameObject spawnedEffect = Instantiate(hitEffect, col.contacts[0].point, Quaternion.LookRotation(-transform.right));
            Destroy(spawnedEffect, 0.25f);
        }

        //DEBUG żeby widzieć gdzie są kolizje kul z trafionymi elementami
        foreach (ContactPoint2D contact in col.contacts)
        {
            //print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        //TODO: Zrób raycasthit przy kolizji, by particle po uderzeniu sie tworzyły w dokładnym miejscu trafienia, a nie jakoś tak z dupy

        Destroy(this.gameObject);
        
    }
    
}
