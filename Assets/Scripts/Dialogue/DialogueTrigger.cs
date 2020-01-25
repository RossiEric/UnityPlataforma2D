using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject player;
    public string dialoguePath;
    private bool inTrigger;
    private bool dialogueLoaded;

    // Start is called before the first frame update
    void Start()
    {
        if (dialogueManager == null)
        {
            dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            inTrigger = false;
        }
    }

    private void runDialogue(bool KeyTrigger) 
    {
        if (KeyTrigger)
        {
            if (inTrigger && !dialogueLoaded)
            {
                dialogueLoaded = dialogueManager.LoadDialog(dialoguePath);
            }

            if (dialogueLoaded)
            {
                dialogueLoaded = dialogueManager.PrintLine();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        runDialogue(Input.GetKeyUp(KeyCode.B) || Input.GetButtonDown("Fire2"));
    }
}
