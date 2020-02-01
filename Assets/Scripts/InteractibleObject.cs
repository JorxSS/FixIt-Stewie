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
    private Material outlineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        state = State.IDLE;
        outlineMaterial = GetComponent<MeshRenderer>().material;
    }

    public void SwitchHighlight(bool highlighted)
    {
        float value = highlighted ? 0.1f : 0f;
        outlineMaterial.SetFloat("_Outline", value);
    }

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
        state = State.ATTACHED;
    }
}
