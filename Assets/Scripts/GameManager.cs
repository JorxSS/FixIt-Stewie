using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Core Managers")]
    public UIManager uiManager;

    [Header("Timers")]
    public float levelTime = 100f;

    [SerializeField]
    float currentTime = 0f;

    [Header("Chores")]
    public int numChoresToWin = 10;

    [SerializeField]
    int currentChoresDone = 0;

    [Header("UI")]
    public ProgressBar timerBar;
    public ChoresCounter choresCounter;

    // Start is called before the first frame update
    void Start()
    {
        InitLevel();
    }

    // Update is called once per frame
    void Update()
    {
        GetDebugInput();
        UpdateLevelTimer();
    }

    public void ChoreDone()
    {
        ++currentChoresDone;
        choresCounter.SetDoneChores(currentChoresDone);
        if (currentChoresDone >= numChoresToWin)
        {
            uiManager.SwitchWinScreen(true);
        }
    }

    private void InitLevel()
    {
        currentTime = 0f;
        currentChoresDone = 0;
        choresCounter.SetDoneChores(currentChoresDone);
        timerBar.SetProgress(0);
    }

    private void GetDebugInput()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ChoreDone();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    
    private void UpdateLevelTimer()
    {
        currentTime += Time.deltaTime;
        timerBar.SetProgress(1 - currentTime / levelTime);

        if (currentTime >= levelTime)
        {
            uiManager.SwitchLoseScreen(true);
        }
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

    public void EndGame()
    {
        Application.Quit();
    }
}
