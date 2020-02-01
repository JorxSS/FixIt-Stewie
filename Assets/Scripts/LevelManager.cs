using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
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
        UpdateLevelTimer();
    }

    public void ChoreDone()
    {
        ++currentChoresDone;
        choresCounter.SetDoneChores(currentChoresDone);
        if (currentChoresDone >= numChoresToWin)
        {
            GameManager.instance.WinGame();
        }
    }

    private void InitLevel()
    {
        currentTime = 0f;
        currentChoresDone = 0;
        choresCounter.SetDoneChores(currentChoresDone);
        timerBar.SetProgress(0);
    }

    private void UpdateLevelTimer()
    {
        currentTime += Time.deltaTime;
        timerBar.SetProgress(1 - currentTime / levelTime);

        if (currentTime >= levelTime)
        {
            GameManager.instance.WinGame();
        }
    }

}
