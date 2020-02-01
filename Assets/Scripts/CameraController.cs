using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public int distanceZaxis = 1;
    public int distanceYaxis = 1;
    Transform playerTransform;
    private RaycastHit[] hits = null;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + distanceYaxis, playerTransform.position.z + distanceZaxis);
        if (hits != null)
        {
            foreach (RaycastHit hit in hits)
            {
                Renderer colliderRenderer = hit.collider.GetComponent<Renderer>();
                if (colliderRenderer)
                {
                    colliderRenderer.enabled = true;
                }
            }

        }
        Vector3 direction_to_player = playerTransform.position - transform.position;

        hits = Physics.RaycastAll(transform.position, direction_to_player, direction_to_player.magnitude);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Scenario")
            {
                Renderer colliderRenderer = hit.collider.GetComponent<Renderer>();
                if (colliderRenderer)
                {
                    colliderRenderer.enabled = false;
                }
            }

        }
    }
}
