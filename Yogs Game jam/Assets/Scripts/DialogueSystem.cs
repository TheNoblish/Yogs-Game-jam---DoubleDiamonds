using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueBox;
    Animator dialogueAnimator;

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
        dialogueAnimator = dialogueBox.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the currently displayed text is the complete line
        if (dialogueDisplay.text == dialogueArray[index])
        {
           //Proceed to the next line of dialogue
           nextDialogue();
        }
          
    }

    IEnumerator Type()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (char letter in dialogueArray[index].ToCharArray())
        {
            dialogueDisplay.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    IEnumerator Fade()
    {
        dialogueAnimator.SetBool("FadeInDialogue", false);
        dialogueAnimator.SetBool("FadeOutDialogue", true);
        yield return new WaitForSeconds(0.5f);
        isTalking = false;
        index = 0;
        dialogueDisplay.text = "";
        dialogueName.text = "";
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
            StartCoroutine(Fade());
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
            dialogueName.text = name;
            dialogueAnimator.SetBool("FadeInDialogue", true);
            dialogueAnimator.SetBool("FadeOutDialogue", false);
            isTalking = true;
            index = 0;
            dialogueDisplay.text = "";
            dialogueArray = npcDialogue;
            typingCoroutine = StartCoroutine(Type());
        }

    }
}
