using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("GameState Windows")]
    public GameObject blackScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchWinScreen(bool enabled)
    {
        winScreen.SetActive(enabled);
    }

    public void SwitchLoseScreen(bool enabled)
    {
        loseScreen.SetActive(enabled);
    }

    public void SwitchPauseScreen(bool enabled)
    {
        pauseScreen.SetActive(enabled);
    }

    public void FadeBlackScreen()
    {
        StartCoroutine(FadeBlackScreenCoroutine());
    }

    IEnumerator FadeBlackScreenCoroutine()
    {
        blackScreen.GetComponent<Fader>().Fade();
        yield return new WaitForSeconds(1);
        if (GameManager.instance.game_won)
        {
            StartCoroutine(WinCoroutine());
        }
        else
        {
            StartCoroutine(LoseCoroutine());
        }
    }
    IEnumerator WinCoroutine()
    {
        SwitchWinScreen(true);
        yield return new WaitForSeconds(2);
        GameManager.instance.ChangeScene("MainMenu");
    }
      IEnumerator LoseCoroutine()
    {
        SwitchWinScreen(true);
        yield return new WaitForSeconds(2);
        GameManager.instance.ChangeScene("MainMenu");
    }
    
}
