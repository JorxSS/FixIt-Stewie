using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public int distanceZaxis = 1;
    public int distanceYaxis = 1;
    public Transform playerTransform;
    private RaycastHit[] hits = null;
    private int layer_mask;

    void Start()
    {
        layer_mask = LayerMask.GetMask("scenario");
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
        hits = Physics.RaycastAll(transform.position, direction_to_player, direction_to_player.magnitude, layer_mask);

        foreach (RaycastHit hit in hits)
        {
            Renderer colliderRenderer = hit.collider.GetComponent<Renderer>();
            if (colliderRenderer)
            {
                colliderRenderer.enabled = false;
            }
        }
    }
}
