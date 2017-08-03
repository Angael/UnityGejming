using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour {

    //Ten skrypt obsługuje wszystkie bronie jakie posiada player
    //Woła by strzelały / machały mieczem 

    //TODO: Powinien być tu jakiś array z broniami jakie gracz posiada, dodatkowo coś
    //co pozwala na scrollowanie między nimi


    private List<Transform> weapons = new List<Transform>();
    private int weaponsCount;
    //activeWeaponIndex zaczyna się od 0 a kończy na weaponsCount-1

    private int activeWeaponIndex = 0;

    public Image reloadAnimation;
    public Text AmmoText;

    void Awake()
    {
        //Dodaj wszystkie bronie:

        //Ile player ma w posiadaniu broni
        weaponsCount = transform.childCount;

        for (int i = 0; i < weaponsCount; i++)
        {
            //Debug.Log(transform.GetChild(i));
            //Debug.Log(i);
            //Debug.Log(weaponsCount);
            weapons.Add(transform.GetChild(i));
        }
    }

    public void ReloadAnimationUpdate(float proc) // proc to wartość 0-1
    {
        reloadAnimation.gameObject.SetActive(true);
        reloadAnimation.fillAmount = 1-proc; // odwracam ta by 0.3 dawało 0.7,  DLA ŁADNEGO WYGLĄDU TYLKO
    }
    public void ReloadAnimationHide()
    {
        reloadAnimation.gameObject.SetActive(false);
        reloadAnimation.fillAmount = 0;
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

    public void r()
    {
        //useMain, bo może w przyszłości bronie będą miały więcej niż 1 atak
        BroadcastMessage("R", null, SendMessageOptions.DontRequireReceiver); //działa na "enabled" bronie w weaponHolderze 
        //Debug.Log("rr");
    }

    public void pickUpAmmo(ammoDrop AmmoDrop)
    {
        // do dokończenia
        //bronie nie mogą mieć ammo w sobie, musi być to przechowywane w weaponholderze, ZRÓB JAKĄŚ KLASĘ NA BRONIE I ICH TYPY
        //Protip: każda broń implementuje interfejs weaponInterface -> posiada guziki R lmb rmb ...
        //https://forum.unity3d.com/threads/adding-scripts-to-a-list-an-attempt-at-a-modular-spell-system.138945/

        //2nd try:
        /*WHen pickup, enable all weapons, send adding ammo and disable all unused*/
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            Debug.Log(child.gameObject);
        }

        BroadcastMessage("AddAmmo", AmmoDrop, SendMessageOptions.DontRequireReceiver);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(activeWeaponIndex).gameObject.SetActive(true);
    }

    public void UpdateAmmoUI(int inMag, int inReserve)
    {
        AmmoText.text = string.Format("Ammo: {0}/{1}", inMag, inReserve);
    }
}
