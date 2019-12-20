using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public bool isFriendly = false;

    public string name;

    bool hasSpoken;

    public int spriteNumber;

    public GameObject dialogueManager;

    GameObject player;

    public string[] dialogueArray;

    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        dialogueManager = GameObject.Find("Dialogue Manager");
        animator = GetComponent<Animator>();
    }

    public void startDialogue()
    {
        if (isFriendly)
        {
            dialogueManager.GetComponent<DialogueSystem>().startDialogue(name, dialogueArray, spriteNumber);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("triggerSound", true);
            animator.SetBool("untriggerSound", false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("untriggerSound", true);
            animator.SetBool("triggerSound", false);
        }
    }

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

    // Update is called once per frame
    void Update()
    {

    }
}
