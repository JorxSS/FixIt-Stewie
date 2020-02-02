using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private GameObject objectInFocus = null;
    public Animator animator;
    PlayerMovement playerMovement;
    InteractibleObject carriedGO = null;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        Physics.IgnoreLayerCollision(4,5);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        animator.SetFloat("speed", movement.magnitude);
        playerMovement.SetMovement(movement);
        if(carriedGO != null)
        {
            animator.SetBool("carryObject", true);
        }
        else
        {
            animator.SetBool("carryObject", false);
        }
        if (Input.GetButtonDown("Interaction"))
        {
            if (objectInFocus != null)
            {
                InteractibleObject interactedObject = objectInFocus.GetComponent<InteractibleObject>();
                if (interactedObject != null)
                {
                    interactedObject.TriggerAction(carriedGO);
                    objectInFocus.GetComponent<InteractibleObject>().SwitchHighlight(false);
                    objectInFocus = null;
                }
                else if(carriedGO != null)
                {
                    carriedGO.Place();
                }
            }
            else if(carriedGO != null)
            {
                carriedGO.Place();
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
        if (!other.CompareTag("InteractiveObject") && !other.CompareTag("Drunk"))
        {
            return;
        }
        if (other.CompareTag("InteractiveObject"))
        {
            if (objectInFocus != null && objectInFocus != other.gameObject)
            {
                return;
            }

            objectInFocus = other.gameObject;
            objectInFocus.GetComponent<InteractibleObject>().SwitchHighlight(true);
        }
        if (other.CompareTag("Drunk"))
        {
            if (Input.GetButtonDown("Interaction"))
                other.GetComponent<DrunkAgent>().LeaveHouse();
        }
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
    }

    public void removeObjectInFocus()
    {
        objectInFocus = null;
    }

    public bool SetCarriedGO(InteractibleObject go)
    {
        if (carriedGO != null && go != null)
            return false;
        carriedGO = go;
        return true;
    }
}
