using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    GameObject player;
    Rigidbody2D rigidbody2D;

    public float dialogueSpeed = 0.2f;

    Coroutine typingCoroutine = null;

    public static bool isTalking;

    public Text dialogueName;
    public Text dialogueDisplay;
    public string[] dialogueArray;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rigidbody2D = player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //If the currently displayed text is the complete line
        if (dialogueDisplay.text == dialogueArray[index])
        {
            //And the player presses space...
            if (Input.GetKeyDown("space"))
            {
                //Proceed to the next line of dialogue
                nextDialogue();
            }

        //If the currently displayed text is NOT the complete line
        } else
        {
            //And the player presses space...
            if (Input.GetKeyDown("space"))
            {
                //Fill in the rest of the line of dialogue
                skipDialogue();
            }
        }

        if (isTalking)
        {
            player.GetComponent<PlayerController>().enabled = false;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        }

    }

    IEnumerator Type()
    {
        foreach(char letter in dialogueArray[index].ToCharArray())
        {
            dialogueDisplay.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    public void nextDialogue()
    {
        if (index < dialogueArray.Length - 1)
        {
            index++;
            dialogueDisplay.text = "";
            typingCoroutine = StartCoroutine(Type());
        } else
        {
            dialogueDisplay.text = "";
            dialogueName.text = "";
            index = 0;
            isTalking = false;
        }
    }

    public void skipDialogue()
    {
        if (isTalking)
        {
            StopCoroutine(typingCoroutine);
            dialogueDisplay.text = "";
            dialogueDisplay.text = dialogueArray[index];
        }

    }

    public void startDialogue(string name, string[] npcDialogue)
    {
        if (!isTalking)
        {
            isTalking = true;
            index = 0;
            dialogueName.text = name;
            dialogueDisplay.text = "";
            dialogueArray = npcDialogue;
            typingCoroutine = StartCoroutine(Type());
        }

    }
}
