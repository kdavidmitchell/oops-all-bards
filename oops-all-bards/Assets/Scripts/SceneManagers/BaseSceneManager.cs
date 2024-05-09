using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSceneManager : MonoBehaviour
{
    public GameObject playerSpawnPoint;
    public CameraController playerCamera;

    protected virtual void Init()
    {
        CheckDialogueUI();
        CheckPartyUI();
        AssignScriptsToPlayer();
        TCPTestClient.Instance.RefreshWMEs();
    }

    protected virtual void AssignScriptsToPlayer()
    {
        //It would seem that when a new scene is loaded that these scripts must be made sure to be ON as to avoid bugs with other code
        //Prevents camera floating bug
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true;
    }

    protected virtual void CheckDialogueUI()
    {
        AssignDialogueUIToManager();
        if (DialogueManager.Instance.dialogueUI.activeSelf)
        {
            DialogueManager.Instance.ToggleDialogueUI();
        }
    }

    protected virtual void CheckPartyUI()
    {
        AssignPartyUIToManager();
        if (PartyManager.Instance.partyUI.activeSelf)
        {
            PartyManager.Instance.TogglePartyUI();
        }
    }

    protected virtual void AssignDialogueUIToManager()
    {
        DialogueManager.Instance.dialogueUI = GameObject.Find("DialogueUI");
        DialogueUIData data = DialogueManager.Instance.dialogueUI.GetComponent<DialogueUIData>();
        DialogueManager.Instance.portrait = data.portrait;
        DialogueManager.Instance.speakerName = data.speakerName;
        DialogueManager.Instance.nodeText = data.nodeText;
        DialogueManager.Instance.nodeContentOrganizer = data.nodeContentOrganizer;
    }

    protected virtual void AssignPartyUIToManager()
    {
        PartyManager.Instance.partyUI = GameObject.Find("PartyUI");
    }
}
