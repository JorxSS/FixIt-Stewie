using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DrunkAgent : MonoBehaviour
{

    public GameObject vomit;
    public Canvas canvas;
    public ChoresProgres choresProgres;
    public GameObject player;
    NavMeshAgent navMeshAgent;
    bool imLeaving = false;
    bool imVomiting = false;
    public Transform exit;
    float time;
    float totalTime;
    ParticleSystem sleepParticle;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        time = 0;
        totalTime = 0;
        sleepParticle = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (!imVomiting && imLeaving)
        {
            CheckDestinationReached();

            time += Time.deltaTime;
            totalTime += Time.deltaTime;
            if (time >= 0.1)
            {
                time = 0;
                if (totalTime >= 1 && Random.Range(0, 100) < 10)
                {
                    totalTime = 0;
                    Vector3 pos = new Vector3(transform.position.x, 19.9f, transform.position.z);
                    GameObject obj = Instantiate(vomit, pos, transform.rotation);
                    obj.GetComponent<InteractibleObject>().player = player;
                    obj.GetComponent<InteractibleObject>().canvas = canvas;
                    obj.GetComponent<InteractibleObject>().choresProgres = choresProgres;
                    choresProgres.addDestroyChore();
                    imVomiting = true;
                    navMeshAgent.isStopped = true;
                }
            }
        }
        else if (imVomiting)
        {
            totalTime += Time.deltaTime;
            if (totalTime >= 1)
            {
                totalTime = 0;
                imVomiting = false;
                navMeshAgent.isStopped = false;
            }
        }
    }
    public void LeaveHouse()
    {
        ParticleSystem.EmissionModule em = sleepParticle.emission;
        em.enabled = false;

        Debug.Log("OKAAAAAAY I'M LEAVING NOW");
        transform.rotation = navMeshAgent.transform.rotation;
        imLeaving = true;
        navMeshAgent.SetDestination(exit.transform.position);
    }
    void CheckDestinationReached()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    choresProgres.DrunkOut();
                    Destroy(gameObject);
                }
            }
        }
    }
}
