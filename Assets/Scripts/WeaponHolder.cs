using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour {

	//Ten skrypt obsługuje wszystkie bronie jakie posiada player
    //Woła by strzelały / machały mieczem 

    //TODO: Powinien być tu jakiś array z broniami jakie gracz posiada, dodatkowo coś
    //co pozwala na scrollowanie między nimi

    public void leftClick()
    {
        //useMain, bo może w przyszłości bronie będą miały więcej niż 1 atak
        BroadcastMessage("useMain"); //działa na "enabled" bronie w weaponHolderze 
    }
}
