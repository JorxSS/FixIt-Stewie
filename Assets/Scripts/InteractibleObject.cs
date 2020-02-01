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
    public float repairBarSpeed = 3.5f;
    private GameObject pBar;
    private float progressTime;
    private bool doingSomething = false;

    public ChoresProgres choresProgres;

    public LevelManager levelManager;

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
        if (doingSomething)
            return;
        switch (typeOfObject)
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
        if(carriedGO != null)
        {
            choresProgres.ChoreCompleted(typeOfObject);
            if (container == Container.TRASH)
                carriedGO.IdleToDestroyed();
            else if (carriedGO.container == container)
            {
                levelManager.BonusTime();
                carriedGO.IdleToDestroyed();
            }
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
        doingSomething = true;
        Dissolver dissolver = GetComponent<Dissolver>();
        while (progressTime < 1.5f)
        {
            yield return null;

            if (dissolver != null)
                dissolver.SetThreshold(progressTime / 1.5f);

            pBar.GetComponent<ProgressBar>().SetProgress(progressTime / 1.5f);
            progressTime += Time.deltaTime;
        }
        if(typeOfObject == TypeOfObject.DESTROYABLE)
        {
            choresProgres.ChoreCompleted(typeOfObject);
        }
        doingSomething = false;
        player.GetComponent<PlayerMovement>().enableMovement();
        Destroy(pBar);
        Destroy(gameObject);
    }

    IEnumerator WaitForActionReparable()
    {
        doingSomething = true;
        int dir = 1;
        float objective = Random.Range(0.0f, 1.0f);
        RectTransform rect = pBar.transform.GetChild(2).GetComponent<RectTransform>();
        rect.pivot = new Vector2(objective, 0);
        int times = 3;
        while (true)
        {
            yield return null;
            progressTime += dir * Time.deltaTime * repairBarSpeed;
            float currProgress = progressTime / 1.5f;
            pBar.GetComponent<ProgressBar>().SetProgress(currProgress);
            if (progressTime >= 1.5)
            {
                dir = -1;
            }
            else if (progressTime < 0)
            {
                dir = 1;
            }
            if (Input.GetButtonDown("Interaction") && Mathf.Abs(currProgress - objective) < 0.15f)
            {
                if (times == 1)
                    break;
                else
                {
                    --times;
                    objective = Random.Range(0.0f, 1.0f);
                    rect.pivot = new Vector2(objective, 0);
                }
            }
        }
        choresProgres.ChoreCompleted(typeOfObject);
        doingSomething = false;
        player.GetComponent<PlayerMovement>().enableMovement();
        player.GetComponent<PlayerController>().removeObjectInFocus();
        gameObject.GetComponent<MeshFilter>().mesh = reparedGO;
        gameObject.tag = "Repaired";
        SwitchHighlight(false);
        Destroy(this);
        Destroy(pBar);
    }
}
