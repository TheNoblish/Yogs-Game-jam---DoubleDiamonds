using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    public GameObject npc;
    public GameObject dialogueManager;
    public GameObject package;


    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        player.GetComponent<PlayerController>().enabled = false;
        dialogueManager = GameObject.Find("Dialogue Manager");
        player = GameObject.Find("Player");
        npc.GetComponent<NPCManager>().enabled = false;
        animator = GetComponent<Animator>();
        animator.SetTrigger("FadeIn");
        package.GetComponent<AudioSource>().enabled = false;
    }

    public void beginDialogue()
    {
        StartCoroutine(startTalking());
        Debug.Log("start");
    }

    IEnumerator startTalking()
    {
        yield return new WaitForSeconds(1f);
        package.GetComponent<AudioSource>().enabled = true;
        npc.GetComponent<NPCManager>().enabled = true;
        npc.GetComponent<NPCManager>().startDialogue();
        yield return new WaitForSeconds(12f);
        npc.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        player.GetComponent<PlayerController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
