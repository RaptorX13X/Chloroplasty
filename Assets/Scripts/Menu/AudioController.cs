using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public static AudioController instance;

    private void Awake()
    {
        instance = this;    
    }

    public void PlayOneShot(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
    }
}
