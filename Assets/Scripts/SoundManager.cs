using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public AudioSource footstepsAudioSource;
    public AudioSource repairingAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchFootsteps(bool enabled)
    {
        if (enabled)
        {
            footstepsAudioSource.Play();
        }
        else
        {
            footstepsAudioSource.Stop();
        }
    }
    public void SwitchRepairing(bool enabled)
    {
        if (enabled)
        {
            repairingAudioSource.Play();
        }
        else
        {
            repairingAudioSource.Stop();
        }
    }
}
