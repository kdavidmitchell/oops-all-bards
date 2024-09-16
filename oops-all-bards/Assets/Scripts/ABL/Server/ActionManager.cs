using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private static ActionManager _instance;
    public static ActionManager Instance => ActionManager._instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else if (_instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // A function used to parse an ABLResponse for further processing.
    // Codes: 1 -- Combat
    //        2 -- Noncombat
    //        3 -- Dialogue
    public void ParseData(ABLResponse response) 
    {
        if (response.code == 1)
        {
            ManageCombatAction(response);
        } else if (response.code == 2)
        {
            ManageNoncombatAction(response);
        } else
        {
            ManageDialogue(response);
        }
    }

    public void ManageCombatAction(ABLResponse response)
    {
        if (response.msg == "Protect")
        {
            if (DemoManager.Instance.hasBeenProtectedOnce == false)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    DemoManager.Instance.CreateSignpostMessage(DemoManager.help6);
                });
                DemoManager.Instance.hasBeenProtectedOnce = true;
            }
            BasePlayer actingCharacter = PartyManager.Instance.FindPartyMemberById(response.data.actingCharacter);
            ITargetable target = PartyManager.Instance.FindPartyMemberById(response.data.targetCharacter);
            AllyAction action = new AllyAction(actingCharacter, target, AllyAction.ActionTypes.PROTECT);
            // Add this action to the queue with priority.
            CombatManager.Instance.combatQueue.PriorityPush(action);
        }

        if (response.msg == "RequestAssistance")
        {
            if (DemoManager.Instance.hasRequestAidOnce == false)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    DemoManager.Instance.CreateSignpostMessage(DemoManager.help7);
                });
                DemoManager.Instance.hasRequestAidOnce = true;
            }
            BasePlayer actingCharacter = PartyManager.Instance.FindPartyMemberById(response.data.actingCharacter);
            if (DemoManager.Instance.hasAssistedOnce == false) 
            {
                actingCharacter.CiFData.AddStatus(new Status(Status.StatusTypes.REQUIRES_ASSISTANCE));
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    DialogueManager.Instance.TriggerAssistanceQuip(response.data.actingCharacter);
                });
            }
        }
    }

    public void ManageNoncombatAction(ABLResponse response) 
    {
        Debug.Log("Received a noncombat action.");
    }

    public void ManageDialogue(ABLResponse response)
    {
        if (response.msg == "Quip")
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => {
            DialogueManager.Instance.ChooseAppropriateQuip(response.data.actingCharacter, response.data.inCombat);
            });
        }
    } 
}
