using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    public float detectionDistance;
    public float spaceDetectionDistance;
    PlayerMovement movement;
    public GameObject raypoint;

    void Start()
    {
        cam = Camera.main;
        movement = GetComponent<PlayerMovement>();
        Physics.IgnoreLayerCollision(4,5);
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
                //Debug.Log("We hit.. " + hit.collider.name + " " + hit.point);
                //Move our player to what we hit
                movement.MoveToPoint(hit.point);


            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("We hit.. " + hit.collider.name + " " + hit.point);
                //Move our player to what we hit
                // transform.rotation = Quaternion.LookRotation(hit.point - transform.position);
               
                if(hit.collider.name != "Player")
                {
                    movement.StopAgent();
                    Quaternion q = Quaternion.FromToRotation(transform.up, hit.normal);
                    transform.rotation = q * transform.rotation;
                    Vector3 pos = ProjectPointOnPlane(transform.up, transform.position, hit.point);
                    transform.LookAt(pos, transform.up);
                }

                
                InteractibleObject interactedObject = hit.collider.gameObject.GetComponent<InteractibleObject>();
                if (interactedObject != null)
                {
                    float dist = Vector3.Distance(hit.transform.position, transform.position);
                    Debug.Log("Detected object is :" + dist);
                    if (dist <= detectionDistance)
                        interactedObject.TriggerAction();
                }


            }
        }
        if (Input.GetButtonDown("Interaction"))
        {
            Debug.Log("Pressing interaction button");
            Ray action = new Ray(raypoint.transform.position, raypoint.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(action, out hit))
            {
                InteractibleObject interactedObject = hit.collider.gameObject.GetComponent<InteractibleObject>();
                if (interactedObject != null)
                {
                    float dist = Vector3.Distance(hit.transform.position, transform.position);
                    Debug.Log("Detected object is :" + dist);
                    if (dist <= spaceDetectionDistance)
                    {
                        Debug.Log("WE ARE HITTING :" + dist);
                        interactedObject.TriggerAction();
                    }

                }
            }
        }
        movement.MoveWASD();
    }
    Vector3 ProjectPointOnPlane(Vector3 planeNormal , Vector3 planePoint, Vector3 point) {
     planeNormal.Normalize();
     float distance = -Vector3.Dot(planeNormal.normalized, (point - planePoint));
     return point + planeNormal* distance;
    }
}
