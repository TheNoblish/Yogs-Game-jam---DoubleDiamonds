using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public bool isFriendly = false;

    public string name;

    bool hasSpoken;

    GameObject dialogueManager;

    GameObject player;

    public string[] dialogueArray;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        dialogueManager = GameObject.Find("Dialogue Manager");
    }

    public void startDialogue()
    {
        if (isFriendly)
        {
            dialogueManager.GetComponent<DialogueSystem>().startDialogue(name, dialogueArray);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
