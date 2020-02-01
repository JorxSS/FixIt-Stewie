using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoresProgres : MonoBehaviour
{

    public Image circleContainer;
    public Image circleDestroy;
    public Image circleRepaired;

    public int totalOfContainer = 2;
    public int totalOfDestroy = 1;
    public int totalOfRepaired = 1;

    public int numberOfContainer = 0;
    public int numberOfDestroy = 0;
    public int numberOfRepaired = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChoreCompleted(InteractibleObject.TypeOfObject typeOfChore)
    {
        switch(typeOfChore)
        {
            case InteractibleObject.TypeOfObject.MOVABLE:
                ++numberOfContainer;
                float amountC = (numberOfContainer/totalOfContainer);
                circleContainer.fillAmount = amountC;
                break;
            case InteractibleObject.TypeOfObject.DESTROYABLE:
                ++numberOfDestroy;
                float amountD = (numberOfDestroy/totalOfDestroy);
                circleDestroy.fillAmount = amountD;            
                break;
            case InteractibleObject.TypeOfObject.REPARABLE:
                ++numberOfRepaired;
                float amountR = (numberOfRepaired/totalOfRepaired);
                circleRepaired.fillAmount = amountR;
                break; 
        }
    }
}
