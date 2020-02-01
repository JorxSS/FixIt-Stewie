using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    Camera mainCamera;

    [Header("Raycast Detection")]
    public float detectionDistance;
    public float spaceDetectionDistance;
    public GameObject raypoint;
    private GameObject objectInFocus = null;
    
    PlayerMovement playerMovement;
    InteractibleObject carriedGO = null;

    void Start()
    {
        mainCamera = Camera.main;
        playerMovement = GetComponent<PlayerMovement>();
        Physics.IgnoreLayerCollision(4,5);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hey dude I'm trying to move to this point" + Input.mousePosition);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hitted = Physics.Raycast(ray, out hit);
            Debug.Log("I hitted something?" + hitted);
            if (hitted)
            {
                //Debug.Log("We hit.. " + hit.collider.name + " " + hit.point);
                //Move our player to what we hit
                Debug.Log("Hey dude I'm trying to move to this point");
                playerMovement.MoveToPoint(hit.point);
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("We hit.. " + hit.collider.name + " " + hit.point);
                //Move our player to what we hit
                // transform.rotation = Quaternion.LookRotation(hit.point - transform.position);
               
                if(hit.collider.name != "Player")
                {
                    playerMovement.StopAgent();
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
                else if(carriedGO != null)
                {
                    ContainerScript containerScript = hit.collider.gameObject.GetComponent<ContainerScript>();
                    if(containerScript != null)
                    {
                        containerScript.Throw(carriedGO);
                    }
                }


            }
        }
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); 
        playerMovement.SetMovement(movement);

        if (Input.GetButtonDown("Interaction"))
        {
            Debug.Log("Pressing interaction button");
            if (objectInFocus != null)
            {
                InteractibleObject interactedObject = objectInFocus.GetComponent<InteractibleObject>();
                if (interactedObject != null)
                {
                    interactedObject.TriggerAction();
                    objectInFocus = null;
                }
            }
        }
    }
    Vector3 ProjectPointOnPlane(Vector3 planeNormal , Vector3 planePoint, Vector3 point)
    {
        planeNormal.Normalize();
        float distance = -Vector3.Dot(planeNormal.normalized, (point - planePoint));
        return point + planeNormal* distance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("InteractiveObject"))
        {
            return;
        }

        if (objectInFocus != null && objectInFocus != other.gameObject)
        {
            return;
        }

        objectInFocus = other.gameObject;
        objectInFocus.GetComponent<InteractibleObject>().SwitchHighlight(true);

        Debug.Log("Detected object is :" + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("InteractiveObject"))
        {
            return;
        }

        if (objectInFocus == other.gameObject)
        {
            objectInFocus.GetComponent<InteractibleObject>().SwitchHighlight(false);
            objectInFocus = null;
        }

        if (Input.GetKeyDown(KeyCode.P) && carriedGO != null)
        {
            carriedGO.Place();
        }
    }

    public void SetCarriedGO(InteractibleObject go)
    {
        carriedGO = go;
    }
}
