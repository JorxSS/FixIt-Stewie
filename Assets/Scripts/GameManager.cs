using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [Header("Core Managers")]
    public UIManager uiManager;
    public LevelManager levelManager;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void PauseGame()
    {
        uiManager.SwitchPauseScreen(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        uiManager.SwitchPauseScreen(false);
        Time.timeScale = 1;
    }

    public void WinGame()
    {
        uiManager.SwitchWinScreen(true);
    }

    public void LoseGame()
    {
        uiManager.SwitchLoseScreen(true);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
