using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{

    public enum Container 
    {
        TRASH,
        GLASS,
        PLASTIC,
        PAPER
    };

    public Container container;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw(InteractibleObject objContainer)
    {
        if(objContainer.container == container)
        {
            //Throws object to trash
            objContainer.Throw();
        }
    }
}
