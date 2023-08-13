using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    //[SerializeField] GameObject mainMenu;
    //[SerializeField] GameObject OptionsMenu;
    [SerializeField] bool paused = false;
    [SerializeField] bool canPause = true;
    [SerializeField] GameObject LostPanel;
    [SerializeField] GameObject WinPanel;
    private string sceneName;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene ();
        sceneName = currentScene.name;
    }

    void Update()
    {
        if(sceneName != "MainMenu" && canPause)
        {
            if(!paused && Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
            else if(paused && Input.GetKeyDown(KeyCode.Escape))
            {
                Resume();
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void Options()
    {
        //OptionsMenu.SetActive(true);
        //mainMenu.SetActive(false);
    }
    public void LeaveOptions()
    {
        //OptionsMenu.SetActive(false);
        //mainMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        paused = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        canPause = true;
        Scene scene;
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        canPause = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void Lost()
    {
        canPause = false;
        LostPanel.SetActive(true);
    }

    public void Won()
    {
        canPause = false;
        WinPanel.SetActive(true);
    }
}
