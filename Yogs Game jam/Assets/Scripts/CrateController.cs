using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    public GameObject coin;
    public GameObject crate;

    AudioSource audioSource;

    bool withinDistance;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (withinDistance && Input.GetKeyDown("e"))
            {
                Debug.Log("test");
                OpenCrate();
            }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            withinDistance = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            withinDistance = false;
        }
    }

    void OpenCrate()
    {
        audioSource.Play();
        coin.SetActive(true);
        crate.SetActive(false);

    }
}
