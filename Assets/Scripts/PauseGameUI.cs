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
                if (!SingleTon.instance.isEnded)
                {
                    Time.timeScale = 1;
                    transform.GetComponent<Canvas>().enabled = false;
                }
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
        Time.timeScale = 1;
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
        Time.timeScale = 1;
    }
}
