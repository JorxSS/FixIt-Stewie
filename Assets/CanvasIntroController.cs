using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasIntroController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] obj;
    RawImage currentObj = null;
    int index = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("TRYINNNNNNNNNNNNNNNNN");
            if (index < obj.Length)
            {
                Debug.Log("hellowwwwwwwwwwwwwwwwwwwwwwww");
                GameObject current = GetNext();
                ++index;
                currentObj = current.GetComponent<RawImage>();
                currentObj.CrossFadeAlpha(0, 2.0f, false);
            }
        }
    }
    GameObject GetNext()
    {
        return obj[index];
    }
    IEnumerator Fade(RawImage go)
    {
        go.CrossFadeAlpha(1, 3.0f, false);
        yield return new WaitForSeconds(.1f);
    }
}
