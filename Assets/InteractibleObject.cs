using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{

    enum State
    {
        IDLE,
        ATTACHED,
        DESTROYED,
        REPAIRED

    };

    State state;

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

    void UpdateIDLE(){}

    void UpdateATTACHED(){}

    void UpdateDESTROYED(){}

    void UpdateREPAIRED(){}
}
