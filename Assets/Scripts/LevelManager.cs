using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Timers")]
    public float levelTime = 100f;

    [SerializeField]
    float currentTime = 0f;

    [Header("UI")]
    public ProgressBar timerBar;


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


    private void InitLevel()
    {
        currentTime = 0f;
        timerBar.SetProgress(0);
    }

    private void UpdateLevelTimer()
    {
        if (currentTime >= levelTime)
        {
            GameManager.instance.LoseGame();
        }
        else
        {
            currentTime += Time.deltaTime;
            timerBar.SetProgress(1 - currentTime / levelTime);
        }
    }

    public void BonusTime()
    {
        //Sound for recycling :D
        currentTime -= 2.0f;
    }

}
