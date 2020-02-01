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
    Stack<GameObject> obj = new Stack<GameObject>();
    RawImage currentObj = null;

    void Start()
    {
        obj.Push(houseNight);
        obj.Push(house1);
        obj.Push(bocata);
        obj.Push(dad);
        obj.Push(mom);
        obj.Push(house0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            Debug.Log("TRYINNNNNNNNNNNNNNNNN");
            if (obj.Count > 0)
            {
                Debug.Log("hellowwwwwwwwwwwwwwwwwwwwwwww");
                GameObject current = GetNext();
                currentObj = current.GetComponent<RawImage>();
                //currentObj.CrossFadeAlpha(1, 3.0f, false);
                Fade(currentObj);
            }
        }
    }
    GameObject GetNext()
    {
        return obj.Pop();
    }
    IEnumerator Fade(RawImage go)
    {
        go.CrossFadeAlpha(0, 2.0f, false);
        yield return new WaitForSeconds(.1f);
    }
}
