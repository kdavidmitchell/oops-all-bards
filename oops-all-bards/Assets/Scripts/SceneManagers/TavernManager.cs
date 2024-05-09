using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TavernManager : BaseSceneManager
{
    private static GameObject playerModel;
    private static GameObject quintonModel;

    void Awake()
    {
        EntitySpawner.Instance.SpawnPlayer(playerSpawnPoint, playerCamera);
        base.Init();
    }

    // Update is called once per frame
    void Start()
    {
        AudioManager.Instance.PlayMusicTrack("thehauntedhearth");
        playerModel = GameObject.FindGameObjectWithTag("Player");
        quintonModel = GameObject.Find("Quinton");
        if (GameManager.Instance.tavernVisits == 2)
        {
            //Prevents bad bug when going in the backrooms after the first fight
            Destroy(GameObject.Find("QuintonQuestTrigger")); 
            StartCoroutine(DemoResolution());
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
}
