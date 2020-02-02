using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public bool game_won = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
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
        game_won = true;
        uiManager.FadeBlackScreen();
    }

    public void LoseGame()
    {
        game_won = false;
        uiManager.FadeBlackScreen();
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
