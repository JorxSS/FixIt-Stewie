using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScene : MonoBehaviour
{
    public GameObject scene;
    public GameObject parent;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        scene.SetActive(true);
        parent.SetActive(false);
    }
}
