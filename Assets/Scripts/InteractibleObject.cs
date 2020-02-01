using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractibleObject : MonoBehaviour
{
    public GameObject player;
    public enum TypeOfObject
    {
        MOVABLE,
        DESTROYABLE,
        REPARABLE,
        CONTAINER
    };

    public enum Container 
    {
        TRASH,
        GLASS,
        PLASTIC,
        PAPER
    };

    public Container container;
    
    public TypeOfObject typeOfObject;

    public Mesh reparedGO;
    private Material outlineMaterial;


    // Start is called before the first frame update
    void Start()
    {
        outlineMaterial = GetComponent<MeshRenderer>().material;
    }

    public void SwitchHighlight(bool highlighted)
    {
        float value = highlighted ? 0.1f : 0f;
        outlineMaterial.SetFloat("_Outline", value);
    }

    public void TriggerAction(InteractibleObject carriedGO)
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
            case TypeOfObject.CONTAINER:
                Throw(carriedGO);
                break;
        }
    }

    void IdleToDestroyed()
    {
        StartCoroutine(WaitForActionDestroyable());
    }

    void IdleToRepaired()
    {
        //Instantiate(reparedGO, transform.position, transform.rotation);
        gameObject.GetComponent<MeshFilter>().mesh = reparedGO;
        gameObject.tag = "Repaired";
        SwitchHighlight(false);
        Destroy(this);
        //Destroy(gameObject);
    }

    void IdleToAttached() {
        transform.parent = player.transform;
        transform.localPosition = new Vector3(0.01f, 0, 0);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        NavMeshObstacle navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;
        player.GetComponent<PlayerController>().SetCarriedGO(this);
    }

    public void Throw(InteractibleObject carriedGO)
    {
        if(carriedGO != null && carriedGO.container == container)
        {
            carriedGO.IdleToDestroyed();
            carriedGO = null;
        }

    }
    public void Place()
    {
        transform.localPosition = new Vector3(1, 0, 0);
        transform.parent = null;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;
        NavMeshObstacle navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = true;
        player.GetComponent<PlayerController>().SetCarriedGO(null);
    }

    IEnumerator WaitForActionDestroyable()
    {

        //yield on a new YieldInstruction that waits for 1.5f seconds.
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
