using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    PlayerMovement movement;

    InteractibleObject carriedGO = null;

    void Start()
    {
        cam = Camera.main;
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //sDebug.Log("We hit.. " + hit.collider.name + " " + hit.point);
                //Move our player to what we hit
                movement.MoveToPoint(hit.point);
                

            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("We hit.. " + hit.collider.name + " " + hit.point);
                //Move our player to what we hit
                InteractibleObject interactedObject = hit.collider.gameObject.GetComponent<InteractibleObject>();
                if (interactedObject != null)
                {
                    interactedObject.TriggerAction();
                }
                else if(carriedGO != null)
                {
                    ContainerScript containerScript = hit.collider.gameObject.GetComponent<ContainerScript>();
                    if(containerScript != null)
                    {
                        containerScript.Throw();
                    }
                }


            }
        }
        movement.MoveWASD();

    }


    public void SetCarriedGO(InteractibleObject go)
    {
        carriedGO = go;
    }
}
