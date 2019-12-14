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

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (isFriendly && distance < 4)
        {
            if (!hasSpoken)
            {
                hasSpoken = true;
                dialogueManager.GetComponent<DialogueSystem>().startDialogue(name, dialogueArray);
            }
        } else if (distance > 4)
        {
            hasSpoken = false;
        }
    }
}
