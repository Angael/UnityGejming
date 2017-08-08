using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    public float damagePerHit = 5f;
    public float penetration = 0.5f;
    public float hitsPerSecond = 1f;
    public float singleAttackDuration = 0.05f;

    [Space]
    [Header("Attack effects")]
    public GameObject attackParticles; //pojawiają się przy celu


    private float timeOfNextHit = 0f;
    private float timeOfAttackEnd = 0f; //Kiedy atak przestanie działać na wrogów
    private WeaponHolder weaponHolder;
    private bool isAttacking = false;
    private bool isAttackingThisUpdate = false;

    void Awake()
    {
        weaponHolder = transform.parent.GetComponent<WeaponHolder>();
        if (1/hitsPerSecond < singleAttackDuration)
        {
            Debug.LogError("AttackDuration is longer than 1/HitsPerSecond");
        }
    }

    public void useMain()
    {
        
        //Sprawdzamy czy już można oddać następny atak
        if (Time.time < timeOfNextHit)
        {
            //jeśli jeszcze nie to kończymy funkcję
            return;
        }
        else
        {
            //jeśli tak to ustalamy czas w którym będzie można wykonać następny strzał
            timeOfNextHit = Time.time + 1 / hitsPerSecond;
            timeOfAttackEnd = Time.time + singleAttackDuration;
        }
    }

    //Wzywane poprzez skrypt MeleeCollider.cs
    //W skrócie, jak player może zaatakować to na każdy collider testuje czy to wróg i zadaje dmg jeśli tak
    public void trigger(Collider2D col)
    {
        //Debug.Log(col +" "+col.gameObject.name);
        if (timeOfAttackEnd > Time.time) { 
            Debug.Log(col.gameObject.name);
            //Test if we hit enemy
            if (col.gameObject.tag != "Enemy")
            {
                //Debug.Log(col.gameObject.name);
                return;
            }
            //parent parent, bo wykrywa knife/rifle itp, więc parent(weaponHolder).parent(player)
            Enemy enemyScript = col.transform.parent.GetComponent<Enemy>();

            if (enemyScript == null)
                return;

            enemyScript.TakeDamage(damagePerHit, penetration);
            
        }
    }

    void OnEnable()
    {
        weaponHolder.UpdateAmmoUI(1, 1);
    }
    void OnDisable()
    {
    }
    void LateUpdate()
    {
        
    }
}
