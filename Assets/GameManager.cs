using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Timers")]
    public float levelTime = 100f;

    [SerializeField]
    float currentTime = 0f;

    [Header("Chores")]
    public int numChoresToWin = 10;

    [SerializeField]
    float currentChoresDone = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        GetDebugInput();
        currentTime += Time.deltaTime;
        if (currentTime >= levelTime)
        {
            Debug.Log("You lose!");
        }

        if (currentChoresDone >= numChoresToWin)
        {
            Debug.Log("You win!");
        }
    }

    public void ChoreDone()
    {
        ++currentChoresDone;
    }

    private void GetDebugInput()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ChoreDone();
        }
    }
}
