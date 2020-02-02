using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fader : MonoBehaviour
{
    public float timeToFade = 1f;
    public Image blackImage;
    [SerializeField]
    private float currentFadeTime = 0f;
    bool fading = false;
    bool faded_in = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            currentFadeTime += Time.deltaTime;
            float progress = Mathf.Min(currentFadeTime / timeToFade, 1);
            float alpha = faded_in ? progress : 1 - progress;
            blackImage.color = new Vector4(1f, 1f, 1f, alpha);
            if (progress == 1)
            {
                faded_in = !faded_in;
                fading = false;
            }
        }
    }

    public void Fade()
    {
        fading = true;
        currentFadeTime = 0f;
    }
}
