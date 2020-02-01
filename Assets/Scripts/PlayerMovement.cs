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
    Rigidbody myRigidBody;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        myRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (movement.magnitude > 0)
        {
            navMeshAgent.ResetPath();
            myRigidBody.transform.position += movement * speed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }

    public void SetMovement(Vector3 movement)
    {
        this.movement = movement; 
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
