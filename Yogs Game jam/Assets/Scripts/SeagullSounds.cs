using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip audioClip = GetRandomClip();
        audioSource.PlayOneShot(audioClip);
    }

    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
