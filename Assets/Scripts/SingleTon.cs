using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleTon : MonoBehaviour {

    /*If one gamemaster script already exists, then this one is not needed, so delete it*/
    public static SingleTon instance;
    
    public Text AmmoText;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            AmmoText.text = "Ammo: -/-";
        }
        else
            Destroy(gameObject);

        //Find and bind all objects in the scene...
        //W różnych scenach może nie być połączone na starcie więc warto to zrobić na starcie sceny w sumie
    }

    public void UpdateAmmoUI(int inMag, int inReserve)
    {
        AmmoText.text = string.Format("Ammo: {0}/{1}", inMag, inReserve );
    }

    //Nieżej są funkcje związane ze zmienianiem scen
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        
        AmmoText = GameObject.Find("UI_Ammo").GetComponent<Text>();
    }
}
