using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public int speed = 10;
    private int realSpeed;
    Rigidbody myRigidBody;
    Vector3 lastMovement;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        lastMovement = Vector3.zero;
        myRigidBody = GetComponent<Rigidbody>();
        realSpeed = speed;
    }

    private void FixedUpdate()
    {
        if (movement.magnitude > 0)
        {
            myRigidBody.transform.position += movement * realSpeed * Time.fixedDeltaTime;
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

    public void disableMovement()
    {
        realSpeed = 0;
    }

    public void enableMovement()
    {
        realSpeed = speed;
    }
}
