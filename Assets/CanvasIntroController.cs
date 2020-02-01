using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasIntroController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject house;
    RawImage imageHouseDay;
    bool fading;
    void Start()
    {
        imageHouseDay = house.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fading == true)
        {
            //Fully fade in Image (1) with the duration of 2
            imageHouseDay.CrossFadeAlpha(1, 2.0f, false);
        }
        if (fading == false)
        {
            imageHouseDay.CrossFadeAlpha(0, 2.0f, false);
        }
    }
    void OnGUI()
    {
        //Fetch the Toggle's state
        fading = GUI.Toggle(new Rect(0, 0, 100, 30), fading, "Fade In/Out");
    }
}
