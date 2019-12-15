using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroCutscene : MonoBehaviour
{

    Animator animator;
    public GameObject player;
    public GameObject npc;
    public GameObject dialogueManager;
    public GameObject package;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartOutro()
    {
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        player.GetComponent<PlayerController>().enabled = false;
        npc.GetComponent<NPCManager>().enabled = true;
        npc.GetComponent<NPCManager>().startDialogue();
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(7.5f);
        player.GetComponent<Animator>().SetBool("isRunning", false);
        player.GetComponent<Animator>().SetBool("isFacingRight", true);
        player.GetComponent<Animator>().SetBool("hasPackage", true);
        animator.SetTrigger("FadeOut");
    }
}
