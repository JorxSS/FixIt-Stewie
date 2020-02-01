using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DrunkAgent : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    bool imLeaving = false;
    public Transform exit;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (imLeaving)
            CheckDestinationReached();
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
