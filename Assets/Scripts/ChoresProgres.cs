using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoresProgres : MonoBehaviour
{

    public Image circleContainer;
    public Image circleDestroy;
    public Image circleRepaired;
    public Image circleDrunk;

    public int totalOfContainer = 16;
    public int totalOfDestroy = 9;
    public int totalOfRepaired = 9;
    public int totalOfDrunk = 3;

    public int numberOfContainer = 0;
    public int numberOfDestroy = 0;
    public int numberOfRepaired = 0;

    public int numberOfDrunk = 0;

    public void addDestroyChore()
    {
        ++totalOfDestroy;
        float amountD = ((float)numberOfDestroy / totalOfDestroy);
        circleDestroy.fillAmount = amountD;
    }

    public void ChoreCompleted(InteractibleObject.TypeOfObject typeOfChore)
    {
        switch(typeOfChore)
        {
            case InteractibleObject.TypeOfObject.CONTAINER:
                ++numberOfContainer;
                float amountC = ((float)numberOfContainer/totalOfContainer);
                circleContainer.fillAmount = amountC;
                break;
            case InteractibleObject.TypeOfObject.DESTROYABLE:
                ++numberOfDestroy;
                float amountD = ((float)numberOfDestroy/totalOfDestroy);
                circleDestroy.fillAmount = amountD;            
                break;
            case InteractibleObject.TypeOfObject.REPARABLE:
                ++numberOfRepaired;
                float amountR = ((float)numberOfRepaired/totalOfRepaired);
                circleRepaired.fillAmount = amountR;
                break; 
        }
        if (numberOfDestroy >= totalOfDestroy && numberOfContainer >= totalOfContainer && numberOfRepaired >= totalOfRepaired)
        {
            GameManager.instance.WinGame();
        }
    }

    public void DrunkOut()
    {
        //Duplicated code because no time
        ++numberOfDrunk;
        float amountDr = ((float)numberOfDrunk/totalOfDrunk);
        circleDrunk.fillAmount = amountDr;   


        if (numberOfDestroy >= totalOfDestroy && numberOfContainer >= totalOfContainer && numberOfRepaired >= totalOfRepaired)
        {
            GameManager.instance.WinGame();
        }
    }
}
