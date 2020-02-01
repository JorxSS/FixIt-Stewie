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
    public Canvas canvas;
    public GameObject progressBarPrefab;
    private GameObject pBar;
    private float progressTime;

    public ChoresProgres choresProgres;

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
        if (typeOfObject == TypeOfObject.MOVABLE)
        {
            Destroy(gameObject);
            return;
        }
        pBar = Instantiate(progressBarPrefab, canvas.transform);
        Vector3 screen = Camera.main.WorldToScreenPoint(transform.position);
        RectTransform rectTransform = pBar.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = screen;
        player.GetComponent<PlayerMovement>().disableMovement();
        progressTime = 0;
        StartCoroutine(WaitForActionDestroyable());
    }

    void IdleToRepaired()
    {
        pBar = Instantiate(progressBarPrefab, canvas.transform);
        Vector3 screen = Camera.main.WorldToScreenPoint(transform.position);
        RectTransform rectTransform = pBar.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = screen;
        player.GetComponent<PlayerMovement>().disableMovement();
        progressTime = 0;
        StartCoroutine(WaitForActionReparable());
    }

    void IdleToAttached() {
        if (!player.GetComponent<PlayerController>().SetCarriedGO(this))
            return;
        transform.parent = player.transform;
        transform.localPosition = new Vector3(0.01f, 0, 0);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        NavMeshObstacle navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;
    }

    public void Throw(InteractibleObject carriedGO)
    {
        if(carriedGO != null && carriedGO.container == container)
        {
            carriedGO.IdleToDestroyed();
            choresProgres.ChoreCompleted(typeOfObject);
        }
    }

    public void Place()
    {
        transform.position = player.transform.GetChild(1).transform.position;
        transform.parent = null;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;
        NavMeshObstacle navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = true;
        player.GetComponent<PlayerController>().SetCarriedGO(null);
    }

    IEnumerator WaitForActionDestroyable()
    {
        while (progressTime < 1.5f)
        {
            yield return null;
            pBar.GetComponent<ProgressBar>().SetProgress(progressTime / 1.5f);
            progressTime += Time.deltaTime;
            //yield on a new YieldInstruction that waits for 1.5f seconds.
        }
        choresProgres.ChoreCompleted(typeOfObject);
        player.GetComponent<PlayerMovement>().enableMovement();
        Destroy(pBar);
        Destroy(gameObject);
    }

    IEnumerator WaitForActionReparable()
    {
        while (progressTime < 1.5f)
        {
            yield return null;
            pBar.GetComponent<ProgressBar>().SetProgress(progressTime / 1.5f);
            progressTime += Time.deltaTime;
            //yield on a new YieldInstruction that waits for 1.5f seconds.
        }
        choresProgres.ChoreCompleted(typeOfObject);
        player.GetComponent<PlayerMovement>().enableMovement();
        player.GetComponent<PlayerController>().removeObjectInFocus();
        gameObject.GetComponent<MeshFilter>().mesh = reparedGO;
        gameObject.tag = "Repaired";
        SwitchHighlight(false);
        Destroy(this);
        Destroy(pBar);
    }
}
