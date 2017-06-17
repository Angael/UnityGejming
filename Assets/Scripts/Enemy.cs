using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float hp = 200f;
	public void TakeDamage(float amount)
    {
        //Odejmujemy hp od życia
        hp -= amount;
        Debug.Log("Hp equals: " + hp);

        //Umiera jak dostał śmiertelny cios
        if(hp <= 0)
        {
            Die();
        }
    }

    //Kiedy umiera usuń tego wroga
    private void Die()
    {
        Destroy(gameObject);
        Debug.Log("I am dead");
    }
}
