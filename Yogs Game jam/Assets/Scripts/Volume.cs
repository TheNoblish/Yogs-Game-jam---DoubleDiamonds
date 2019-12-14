using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume(float vol)
    {
        audioMixer.SetFloat("MusicVol", vol);
    }

    public void SetEffectsVolume(float vol)
    {
        audioMixer.SetFloat("EffectsVol", vol);
    }
}
