using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameUI : MonoBehaviour {

    void Start()
    {
        transform.GetComponent<Canvas>().enabled = false;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                transform.GetComponent<Canvas>().enabled = false;
            }
            else
            {
                Time.timeScale = 0;
                transform.GetComponent<Canvas>().enabled = true;
            }
        }
    }

    //Guzik MainMenu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
