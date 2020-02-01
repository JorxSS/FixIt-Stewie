using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public int speed = 10;
    NavMeshAgent agent;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    public void MoveWASD()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (movement.magnitude > 0)
        {
            agent.ResetPath();
        }
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
        //transform.rotation = Quaternion.LookRotation(movement);
        //rb.transform.position += movement * speed * Time.deltaTime;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
    public void MoveToPoint(Vector3 point)
    {
        transform.rotation = agent.transform.rotation;
        agent.SetDestination(point);
        
    }
    public void StopAgent()
    {
        agent.ResetPath();

    }
}
