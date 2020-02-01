using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public int speed = 10;
    NavMeshAgent navMeshAgent;
    Rigidbody rigidbody;

    Vector3 lastMovement;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        lastMovement = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (movement.magnitude > 0)
        {
            navMeshAgent.ResetPath();
            rigidbody.transform.position += movement * speed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }

    public void SetMovement(Vector3 movement)
    {
        lastMovement = this.movement;
        this.movement = movement; 
        
        if (lastMovement.magnitude == 0 && movement.magnitude != 0)
        {
            SoundManager.instance.SwitchFootsteps(true);
        }
        if (lastMovement.magnitude != 0 && movement.magnitude == 0)
        {
            SoundManager.instance.SwitchFootsteps(false);
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        Debug.Log("Hey dude I'm trying to move to this point");
        transform.rotation = navMeshAgent.transform.rotation;
        navMeshAgent.SetDestination(point);
    }
    public void StopAgent()
    {
        navMeshAgent.ResetPath();
    }
}
