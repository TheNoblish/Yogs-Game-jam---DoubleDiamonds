using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] stepClips;

    [SerializeField]
    private AudioClip[] meowClips;

    private AudioSource audioSource;
    private AudioSource meowAudioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        meowAudioSource = GetComponentInChildren<AudioSource>();
    }

    private void Step()
    {
        AudioClip audioClip = GetRandomClip();
        audioSource.PlayOneShot(audioClip);
    }

    private void Meow()
    {
        AudioClip audioClip = GetRandomMeow();
        meowAudioSource.PlayOneShot(audioClip);
    }

    private AudioClip GetRandomClip()
    {
        return stepClips[UnityEngine.Random.Range(0, stepClips.Length)];
    }

    private AudioClip GetRandomMeow()
    {
        return meowClips[UnityEngine.Random.Range(0, meowClips.Length)];
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
