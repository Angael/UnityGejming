using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour {

    //Ten skrypt obsługuje wszystkie bronie jakie posiada player
    //Woła by strzelały / machały mieczem 

    //TODO: Powinien być tu jakiś array z broniami jakie gracz posiada, dodatkowo coś
    //co pozwala na scrollowanie między nimi


    private List<Transform> weapons = new List<Transform>();
    private int weaponsCount;
    //activeWeaponIndex zaczyna się od 0 a kończy na weaponsCount-1
    private int activeWeaponIndex = 0;

    void Awake()
    {
        //Dodaj wszystkie bronie:

        //Ile player ma w posiadaniu broni
        weaponsCount = transform.childCount;

        for (int i = 0; i < weaponsCount; i++)
        {
            Debug.Log(transform.GetChild(i));
            Debug.Log(i);
            Debug.Log(weaponsCount);
            weapons.Add(transform.GetChild(i));
        }
    }

    //funkcje zmieniające aktywną broń
    //Jedna bierze index za argument i zmienia na broń, bo klikneliśmy np. klawisz "1"
    //Druga bierze stringa scroll = "next" albo "previous", by kółkiem myszy też dało się zmieniać bronie.
    public void ChangeWeapon(int index)
    {
        for (int i = 0; i < weaponsCount; ++i)
        {
            if (index == i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                activeWeaponIndex = i;
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void ChangeWeapon(string scroll)
    {
        if (scroll=="next") {
            //jeśli ostatnia broń jest aktywna
            if (activeWeaponIndex+1 == weaponsCount)
            {
                activeWeaponIndex = 0;
                ChangeWeapon(activeWeaponIndex);
            }
            else
            {
                activeWeaponIndex ++;
                ChangeWeapon(activeWeaponIndex);
            }
        }
        else // scroll==previous
        {
            //jeśli pierwsza broń jest aktywna
            if (activeWeaponIndex == 0)
            {
                activeWeaponIndex = weaponsCount-1;
                ChangeWeapon(activeWeaponIndex);
            }
            else
            {
                activeWeaponIndex--;
                ChangeWeapon(activeWeaponIndex);
            }
        }

    }

    //funkcja wołana z player_actions
    public void leftClick()
    {
        //useMain, bo może w przyszłości bronie będą miały więcej niż 1 atak
        BroadcastMessage("useMain", null, SendMessageOptions.DontRequireReceiver); //działa na "enabled" bronie w weaponHolderze 
    }
}
