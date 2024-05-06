using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : MonoBehaviour, IInteractable
{
    private bool triggering;
    
    // Assigned in editor.
    public int dialogueID;
    public string exhaustedDialogueResponse;
    // Quest link: [0] = quest ID
    //             [1] = quest stage ID
    public int[] questLink = new int[2]; 

    // Assigned DialogueTrigger starts dialogue from manager if the dialogue
    // has not already been exhausted, or falls back to exhausted dialogue response if it has.
    public void Execute()
    {
        Debug.Log("Executing dialogue.");
        // TODO: Have characters in dialogue actually look towards the player when they speak.
        // transform.LookAt(Camera.main.transform);
        Dialogue toStart = DialogueManager.Instance.jsonReader.dialogues.GetDialogue(dialogueID);
        if (!toStart.Exhausted)
        {
            DialogueManager.Instance.StartDialogue(dialogueID);
            if (DialogueManager.Instance.portrait.sprite == null)
            {
                Debug.Log("Generating Portrait");
                DialogueManager.Instance.dialogueModel(gameObject);
            }
            QuestManager.Instance.MarkStageComplete(questLink[1]);
            
        } else
        {
            DialogueManager.Instance.SpawnTextBubble(gameObject, exhaustedDialogueResponse);
        }  
    }

    void Update()
    {
        if (triggering)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Execute();

                //Sarah's edit added to prevent triggering dialogue twice error
                triggering = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggering = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggering = false;
        }
    }
}
