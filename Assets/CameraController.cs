using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public int distanceZaxis = 1;
    public int distanceYaxis = 1;
    Transform target;
    private RaycastHit[] hits = null;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target);
        transform.position = new Vector3(target.position.x, target.position.y + distanceYaxis, target.position.z + distanceZaxis);
        if (hits != null)
        {
            foreach (RaycastHit hit in hits)
            {
                Renderer r = hit.collider.GetComponent<Renderer>();
                if (r)
                {
                    r.enabled = true;
                }
            }

        }
        hits = Physics.RaycastAll(this.transform.position, (target.transform.position - transform.position), Vector3.Distance(transform.position, target.transform.position));

        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.name != "Player")
            {
                Renderer r = hit.collider.GetComponent<Renderer>();
                if (r)
                {
                    r.enabled = false;
                }
            }

        }
    }
}
