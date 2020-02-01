using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dissolver : MonoBehaviour
{
    [Header("Configuration")]
    public MeshRenderer text_renderer;
    [Range(0.0f, 1.1f)]
    public float initialThreshold;

    void Start()
    {
        text_renderer.material.SetFloat("_Threshold", initialThreshold);
    }

    public void SetThreshold(float percentage)
    {
        text_renderer.material.SetFloat("_Threshold", percentage * 1.1f);
    }
}