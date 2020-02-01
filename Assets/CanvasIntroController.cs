using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasIntroController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject house0;
    public GameObject house1;
    public GameObject houseNight;
    public GameObject dad;
    public GameObject mom;
    public GameObject bocata;
    Stack<GameObject> obj;
    RawImage currentObj;

    bool fading = false;
    void Start()
    {
        obj.Push(house0)
        obj.Push(dad);
        obj.Push(mom);
        obj.Push(bocata);
        obj.Push(houseNight);
   
    }

    // Update is called once per frame
    void Update()
    {
        if (fading == true)
        {
            //Fully fade in Image (1) with the duration of 2
            
            imageHouseDay.CrossFadeAlpha(0, 2.0f, false);
        }
        if (fading == false)
        {
            imageHouseDay.CrossFadeAlpha(1, 2.0f, false);
        }
    }
    void OnGUI()
    {
        //Fetch the Toggle's state
        fading = GUI.Toggle(new Rect(0, 0, 100, 30), fading, "Fade In/Out");
    }
    GameObject GetNext()
    {
        for(int)
    }
}
