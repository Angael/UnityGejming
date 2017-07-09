using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    //Pilnujemy liczby wrogów na mapie, by nie lagowało
    public static int EnemyCount = 0;

    public float hp = 200f;
    public float maxHp = 200f;
    public float armor = 50f; // o ile zmniejsza obrażenia przychodzące

    public GameObject canvas;
    public Image hpBar;

    void Awake()
    {
        EnemyCount++;
        //Debug.Log("EnemyCount: " + EnemyCount);
        hp = maxHp;

        //canvas = transform.GetChild(0).gameObject;
        //hpBar = canvas.transform.Find("HPBG").Find("HP").GetComponent<Image>();
    }

	public void TakeDamage(float amount, float penetration = 1f) //domyślnie: jak nie podamy argumentu to pancerz się nie liczy
    {
        //Odejmujemy hp od życia
        canvas.SetActive(true);
        float realDamage = amount - (armor * (1-penetration)); // 1 penetracji to pełna penetracja, 0 znaczy że nie ma penetracji
        hp -= realDamage;
        hpBar.fillAmount = hp / maxHp;

        //Umiera jak dostał śmiertelny cios
        if (hp <= 0)
        {
            Die();
        }
    }

    //Kiedy umiera usuń tego wroga
    private void Die()
    {
        Destroy(gameObject);
        //Debug.Log("I am dead");
        EnemyCount--;
    }
}
