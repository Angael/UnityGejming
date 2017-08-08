using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//NARAZIE TEN SKRYPT JEST BEZUŻYTECZNY
public class SingleTon : MonoBehaviour {

    /*If one gamemaster script already exists, then this one is not needed, so delete it*/
    public static SingleTon instance;

    public GameObject EndGameUI;
    public bool isEnded = false;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        //Find and bind all objects in the scene...
        //W różnych scenach może nie być połączone na starcie więc warto to zrobić na starcie sceny w sumie
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

    //nasza funkcja, może się nazywać jakkolwiek
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        
        Time.timeScale = 1;
        isEnded = false;
    }

    public void EndGame()
    {
        EndGameUI.SetActive(true);
        isEnded = true;
        Time.timeScale = 0;
    }
}
