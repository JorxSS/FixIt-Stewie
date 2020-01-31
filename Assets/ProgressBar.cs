using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public RectTransform fillTransform;

    [SerializeField]
    float barWidth = 0f;

    [SerializeField]
    float currentProgress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barWidth = GetComponent<RectTransform>().rect.width;
    }

    public void SetProgress(float currentProgress)
    {
        this.currentProgress = currentProgress;
        float newRight = (1 - currentProgress) * barWidth;
        fillTransform.offsetMax = new Vector2(-newRight, 0);
    }
}
