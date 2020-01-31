using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoresCounter : MonoBehaviour
{
    public Text totalChoresText;
    public Text currentChoresText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDoneChores(int doneChores)
    {
        currentChoresText.text = doneChores.ToString();
    }

    public void SetTotalChores(int totalChores)
    {
        totalChoresText.text = "/ " + totalChores.ToString();
    }
}
