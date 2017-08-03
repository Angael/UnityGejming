using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuUI : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        //Time.timeScale = 0;
    }

    //Guzik MainMenu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
