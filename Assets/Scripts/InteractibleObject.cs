﻿using System.Collections;
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
    public Material reparedMaterial;
    private Material outlineMaterial;
    public Canvas canvas;
    public GameObject progressBarPrefab;
    public float repairBarSpeed = 3.5f;
    private GameObject pBar;
    private float progressTime;

    ParticleSystem repairParticles;
    ParticleSystem particleDone;
    ParticleSystem throwParticle;

    private bool doingSomething = false;

    public ChoresProgres choresProgres;
    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        outlineMaterial = GetComponent<MeshRenderer>().material;
        if (typeOfObject == TypeOfObject.MOVABLE)
        {
            throwParticle = GetComponentInChildren<ParticleSystem>();
        }
        else if (typeOfObject == TypeOfObject.REPARABLE)
        {
            repairParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
            particleDone = transform.GetChild(1).GetComponent<ParticleSystem>();
        }
        else if (typeOfObject == TypeOfObject.DESTROYABLE)
        {
            repairParticles = transform.GetChild(1).GetComponent<ParticleSystem>();
            particleDone = transform.GetChild(2).GetComponent<ParticleSystem>();
        }
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

        if (repairParticles != null)
        {
            repairParticles.Play();
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

    void IdleToAttached()
    {
        if (!player.GetComponent<PlayerController>().SetCarriedGO(this))
            return;
        SoundManager.instance.PlayPick();
        GameObject go = GameObject.Find("grabObject");
        transform.parent = go.transform;
        transform.localPosition = new Vector3(-0.215f, 0.023f, 0.046f);
        transform.localEulerAngles = new Vector3(-165.673f, -17.14301f, 92.38899f);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        NavMeshObstacle navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;
    }

    public void Throw(InteractibleObject carriedGO)
    {
        if(carriedGO != null)
        {
            if (container == Container.TRASH)
            {
                choresProgres.ChoreCompleted(typeOfObject);
                carriedGO.IdleToDestroyed();
				SoundManager.instance.PlayCorrectThrow();
            }
            else if (carriedGO.container == container)
            {
                choresProgres.ChoreCompleted(typeOfObject);
                levelManager.BonusTime();
                carriedGO.IdleToDestroyed();
				SoundManager.instance.PlayCorrectThrow();
            }
            else
            {
				SoundManager.instance.PlayIncorrectThrow();
                StartCoroutine(wrongConainerFeedback());
            }

            if(throwParticle != null)
                throwParticle.Play();
        }
    }

    public void Place()
    {
        SoundManager.instance.PlayDrop();
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
        SoundManager.instance.SwitchMop(true);
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
        SoundManager.instance.SwitchMop(false);
        doingSomething = false;
        player.GetComponent<PlayerMovement>().enableMovement();
        Destroy(pBar);
        Destroy(gameObject);
    }

    IEnumerator WaitForActionReparable()
    {
        SoundManager.instance.SwitchRepairing(true);
        doingSomething = true;
        int dir = 1;
        float objective = Random.Range(0.0f, 1.0f);
        RectTransform rect = pBar.transform.GetChild(2).GetComponent<RectTransform>();
        rect.pivot = new Vector2(objective, 0);
        int times = 3;
        float time = 0.6f;
        while (true)
        {
            yield return null;
            progressTime += dir * Time.deltaTime * repairBarSpeed;
            time += Time.deltaTime;
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
            if (Input.GetButtonDown("Interaction"))
            {
                if (time >= 0.6f)
                {
                    time = 0;
                    if (Mathf.Abs(currProgress - objective) < 0.15f)
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
            }
        }

        repairParticles.Stop();
        particleDone.Play();
        SoundManager.instance.SwitchRepairing(false);
        choresProgres.ChoreCompleted(typeOfObject);
        doingSomething = false;
        player.GetComponent<PlayerMovement>().enableMovement();
        player.GetComponent<PlayerController>().removeObjectInFocus();
        gameObject.GetComponent<MeshFilter>().mesh = reparedGO;
        gameObject.GetComponent<MeshRenderer>().material = reparedMaterial;
        gameObject.tag = "Repaired";
        SwitchHighlight(false);
        Destroy(this);
        Destroy(pBar);
    }

    IEnumerator wrongConainerFeedback()
    {
        float time = 0;
        float speed = 4;
        float duration = 0.1f;
        Vector3 origPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        while (time < duration)
        {
            float step = time / duration;
            if (step <= 0.25)
            {
                transform.position = transform.position + new Vector3(0, Time.deltaTime * speed, 0);
            }
            else if (step <= 0.75)
            {
                transform.position = transform.position - new Vector3(0, Time.deltaTime * speed, 0);
            }
            else
            {
                transform.position = transform.position + new Vector3(0, Time.deltaTime * speed, 0);
            }
            yield return null;
            time += Time.deltaTime;
        }
        transform.position = origPos;
    }
}
