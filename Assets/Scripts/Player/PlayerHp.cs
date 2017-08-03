using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHp : MonoBehaviour {

    public bool GodMode = true;

    
    public float maxHp = 200f;
    public float hp = 200f;

    public GameObject canvas;
    public Image hpBar;

    // Use this for initialization
    void Start () {
        hp = maxHp;
	}

    public void TakeDamage(float amount)
    {
        if (GodMode)
        {
            return;
        }
        //Odejmujemy hp od życia
        canvas.SetActive(true);
        hp -= amount;
        hpBar.fillAmount = hp / maxHp;

        //Umiera jak dostał śmiertelny cios
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SingleTon.instance.EndGame();
    }
}
