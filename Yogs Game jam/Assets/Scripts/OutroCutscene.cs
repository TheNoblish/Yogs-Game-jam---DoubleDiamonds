using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroCutscene : MonoBehaviour
{

    Animator animator;
    public GameObject player;
    public GameObject npc;
    public GameObject dialogueManager;
    public GameObject package;
    public AudioSource audioSource;
    public AudioSource audioSource2;


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
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Animator>().SetBool("isRunning", false);
        player.GetComponent<Animator>().SetBool("isFacingRight", true);
        player.GetComponent<Animator>().SetBool("hasPackage", true);
        npc.GetComponent<NPCManager>().enabled = true;
        npc.GetComponent<NPCManager>().startDialogue();
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        IEnumerator fadeSound = MusicFadeOut.FadeOut(audioSource, 20f);
        IEnumerator fadeSound2 = MusicFadeOut.FadeOut(audioSource2, 20f);
        StartCoroutine(fadeSound);
        StartCoroutine(fadeSound2);
        yield return new WaitForSeconds(5f);
        animator.SetTrigger("FadeOut");


    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(2);
    }
}
