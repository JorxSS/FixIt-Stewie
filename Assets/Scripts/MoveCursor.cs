using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveCursor : MonoBehaviour
{

    public Image cursor0;
    public Image cursor1;
    public Image cursor2;
    int currentPosition = 0;

    float timeToPulse = 0.25f;
    float currentTime = 0.0f;

    float pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = Input.GetAxisRaw("Vertical");
        if(currentTime >= timeToPulse)
        {
            if(pos != 0)
            {
                ChangeState();
                currentTime = 0.0f;
            }

        }

        if(Input.GetButtonDown("Interaction"))
        {
            switch(currentPosition)
            {
                case 0:
                    GameManager.instance.ChangeScene("Intro");
                    break;
                case 1:
                    UIManager.instance.SwitchControlsVisibility();
                    break;
                case 2:
                    GameManager.instance.EndGame();
                    break;
            }
        }

        currentTime += Time.deltaTime;
        
    }

    void ChangeState()
    {
        currentPosition = (currentPosition - (int)pos + 3) % 3;
        switch(currentPosition)
        {
            case 0:
                cursor0.enabled = true;
                cursor1.enabled = false;
                cursor2.enabled = false;
                break;
            case 1:
                cursor1.enabled = true;
                cursor0.enabled = false;
                cursor2.enabled = false;
                break;
            case 2:
                cursor2.enabled = true;
                cursor0.enabled = false;
                cursor1.enabled = false;
                break;
        }
    }
}
