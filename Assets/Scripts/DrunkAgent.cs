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
    public Transform exit;
    float time;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        time = 0;
    }
    private void Update()
    {
        if (imLeaving)
        {
            CheckDestinationReached();

            time += Time.deltaTime;
            if (time >= 0.1)
            {
                time = 0;
                if (Random.Range(0, 100) < 10)
                {
                    Vector3 pos = new Vector3(transform.position.x, 19.9f, transform.position.z);
                    GameObject obj = Instantiate(vomit, pos, transform.rotation);
                    obj.GetComponent<InteractibleObject>().player = player;
                    obj.GetComponent<InteractibleObject>().canvas = canvas;
                    obj.GetComponent<InteractibleObject>().choresProgres = choresProgres;
                    choresProgres.addDestroyChore();
                }
            }
        }
    }
    public void LeaveHouse()
    {
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
                    Debug.Log("BYEEEE");
                    Destroy(gameObject);
                }
            }
        }
    }
}
