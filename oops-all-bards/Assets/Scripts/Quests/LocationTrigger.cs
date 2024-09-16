using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour, IInteractable
{
    public int questStage;
    public int dialogueToTrigger;
    private bool triggering;
    public bool hasExecuted = false;

    public void Execute()
    {
        DialogueManager.Instance.StartDialogue(dialogueToTrigger);
        QuestManager.Instance.MarkStageComplete(questStage);
    }

    void Update()
    {
        //gameObject.Find<DialogueManager>().test
        if (triggering && !hasExecuted)
        {
            Execute();
            hasExecuted = true;
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
