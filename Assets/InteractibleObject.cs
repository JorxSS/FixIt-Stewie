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
        state = IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
