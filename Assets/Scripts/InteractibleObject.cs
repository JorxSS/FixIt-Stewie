using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{
    public enum TypeOfObject
    {
        MOVABLE,
        DESTROYABLE,
        REPARABLE
    };
    
    public TypeOfObject typeOfObject;

    public GameObject reparedGO;

    public ContainerScript.Container container;

    // Start is called before the first frame update
    public void TriggerAction()
    {
        switch(typeOfObject)
        {
            case TypeOfObject.MOVABLE:
                IdleToAttached();
                break;
            case TypeOfObject.DESTROYABLE:
                IdleToDestroyed();
                break;
            case TypeOfObject.REPARABLE:
                IdleToRepaired();
                break;
        }
    }

    void IdleToDestroyed()
    {
        Destroy(gameObject);
    }

    void IdleToRepaired()
    {
        Instantiate(reparedGO, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void IdleToAttached(){
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        transform.parent = player.transform;
        transform.position = new Vector3(1, 0, 0);
        player.GetComponent<PlayerController>().SetCarriedGO(this);
    }

    public void Throw()
    {
        //player.GetComponent<PlayerController>().SetCarriedGO(null);
        IdleToDestroyed();
    }
}
