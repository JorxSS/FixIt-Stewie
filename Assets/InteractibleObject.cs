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
    enum State
    {
        IDLE,
        ATTACHED,
        DESTROYED,
        REPAIRED
    };

    State state;
    public GameObject reparedGO;

    // Start is called before the first frame update
    void Start()
    {
        state = State.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.IDLE:
                UpdateIDLE();
                break;
            case State.ATTACHED:
                UpdateATTACHED();
                break;
            case State.DESTROYED:
                UpdateDESTROYED();
                break;
            case State.REPAIRED:
                UpdateREPAIRED();
                break;
        }
    }
    void UpdateIDLE()
    {
        if (Input.GetKey(KeyCode.M))
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
        state = State.ATTACHED;
    }

    void UpdateATTACHED(){}
    void UpdateDESTROYED(){}
    void UpdateREPAIRED(){}
}
