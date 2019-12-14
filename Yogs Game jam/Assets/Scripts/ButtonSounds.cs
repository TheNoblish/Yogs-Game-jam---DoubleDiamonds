using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource buttonAudioSource;
    public AudioClip clickSoundEffect;
    public AudioClip hoverSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickButton()
    {
        buttonAudioSource.PlayOneShot(clickSoundEffect);
    }

    public void HoverButton()
    {
        buttonAudioSource.PlayOneShot(hoverSoundEffect);
    }
}
