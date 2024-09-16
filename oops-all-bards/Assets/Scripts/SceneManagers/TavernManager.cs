using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TavernManager : MonoBehaviour
{
    public GameObject playerSpawnPoint;
    public CameraController playerCamera;
    private static GameObject playerModel;
    private static GameObject quintonModel;

    void Awake()
    {
        SpawnPlayer();
        CheckDialogueUI();
        CheckPartyUI();
        TCPTestClient.Instance.RefreshWMEs();
    }

    // Update is called once per frame
    void Start()
    {

        AudioManager.Instance.PlayMusicTrack("thehauntedhearth");
        //It would seem that when a new scene is loaded that these scripts must be made sure to be ON as to avoid bugs with other code
        //Prevents camera floating bug
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true;

        playerModel = GameObject.FindGameObjectWithTag("Player");
        quintonModel = GameObject.Find("Quinton");
        if (GameManager.Instance.tavernVisits == 2)
        {
            //Prevents bad bug when going in the backrooms after the first fight
            Destroy(GameObject.Find("QuintonQuestTrigger")); 
            StartCoroutine(DemoResolution());
        }

    }


    void SpawnPlayer()
    {

        // Instantiate chosen player model at spawn point.
        BasePlayer playerData = DataManager.Instance.PlayerData;
        GameObject toInstantiate = Instantiate(playerData.Model, playerSpawnPoint.transform.position, Quaternion.identity);
        // Set up rigidbody and collider for physics.
        Rigidbody rb = toInstantiate.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        // Set up player controller script and camera follow.
        PlayerController playerController = toInstantiate.AddComponent<PlayerController>();
        playerCamera.Player = toInstantiate;
        // Set up player animator controller.
        Animator a = toInstantiate.GetComponent<Animator>();
        a.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Controllers/Player");
        // Add player tag.
        toInstantiate.tag = "Player";
        Cursor.lockState = CursorLockMode.Locked; //testing

    }

    void CheckDialogueUI()
    {
        AssignDialogueUIToManager();
        if (DialogueManager.Instance.dialogueUI.activeSelf)
        {
            DialogueManager.Instance.ToggleDialogueUI();
        }
    }

    void CheckPartyUI()
    {
        AssignPartyUIToManager();
        if (PartyManager.Instance.partyUI.activeSelf)
        {
            PartyManager.Instance.TogglePartyUI();
        }
    }

    IEnumerator DemoResolution()
    {
        NPCMovement quintonAgent = quintonModel.gameObject.GetComponent<NPCMovement>();
        Animator a = quintonModel.gameObject.GetComponent<Animator>();
        bool didNotAssist = PartyManager.Instance.FindPartyMemberById(1).CiFData.HasStatusType(Status.StatusTypes.REQUIRES_ASSISTANCE);
        if (didNotAssist) 
        {
            DemoManager.Instance.CreateSignpostMessage(DemoManager.help8); 
            a.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"Controllers/InjuredNPC"); 
        } else
        {
            a.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"Controllers/HappyNPC");
        }
        quintonAgent.SendQuintonToPlayer();
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(quintonAgent.HasStopped);
        
        if (didNotAssist)
        {
            PartyManager.Instance.FindPartyMemberById(1).CiFData.AddStatus(new Status(Status.StatusTypes.LEFT_HANGING));
            DialogueManager.Instance.StartDialogue(3);
        } else
        {
            DialogueManager.Instance.StartDialogue(4);
        }
    }

    public static GameObject PlayerModel
    {
        get { return playerModel; }
    }

    public static GameObject QuintonModel
    {
        get { return quintonModel; }
    }

    public void AssignDialogueUIToManager()
    {
        DialogueManager.Instance.dialogueUI = GameObject.Find("DialogueUI");
        DialogueUIData data = DialogueManager.Instance.dialogueUI.GetComponent<DialogueUIData>();
        DialogueManager.Instance.portrait = data.portrait;
        DialogueManager.Instance.speakerName = data.speakerName;
        DialogueManager.Instance.nodeText = data.nodeText;
        DialogueManager.Instance.nodeContentOrganizer = data.nodeContentOrganizer;
       
    }

    public void AssignPartyUIToManager()
    {
        PartyManager.Instance.partyUI = GameObject.Find("PartyUI");
    }

     
}
