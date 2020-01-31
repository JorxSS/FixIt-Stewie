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

    void Idle2Attached()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        transform.parent = player.transform;
        transform.position = new Vector3(1, 0, 0);
        state = State.ATTACHED;
    }

    void UpdateIDLE(){
        if (Input.GetKeyDown(KeyCode.M))
        {
            Idle2Attached();
        }
    }

    void UpdateATTACHED(){}

    void UpdateDESTROYED(){}

    void UpdateREPAIRED(){}
}
