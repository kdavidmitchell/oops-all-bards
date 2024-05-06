using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{

    private static DialogueManager _instance;
    public static DialogueManager Instance => DialogueManager._instance;
    private int nodeIndex = 0;
    private int dialogueIndex = 0;
    public JSONReader jsonReader;
    public GameObject dialogueUI;
    public GameObject nodeContentOrganizer;
    public TMP_Text nodeText;
    public Image portrait;
    public TMP_Text speakerName;
    public GameObject nodeResponsePrefab;
    public GameObject textBubblePrefab;
    public GameObject questUI;
    public GameObject NPCModelSpawn;

    // Singleton pattern
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Debug.Log(gameObject.name);//TEST

        //jsonReader.quips.DebugQuips();
    }

    public void ToggleDialogueUI()
    {
        dialogueUI.SetActive(!dialogueUI.activeSelf);
        dialogueUI.GetComponent<Animator>().Play("dialogueBox");
    }

    public void StartDialogue(int dialogueID)
    {
        //Unlocks cursor when dialogue is active
        Cursor.lockState = CursorLockMode.Confined;
        //turn QuestUI off when dialogue is active
        questUI = GameObject.Find("QuestUI");
        questUI.SetActive(false);
        nodeIndex = 0;
        Debug.Log("Starting dialogue.");
        GameManager.Instance.TogglePlayerControls();
        Debug.Log("toggle pause in dialogue");
        dialogueIndex = dialogueID;
        Dialogue dialogue = jsonReader.dialogues.GetDialogue(dialogueID);
        RenderDialogueUI(dialogue);
    }

    private void RenderDialogueUI(Dialogue dialogue)
    {
        speakerName.text = dialogue.SpeakerName;
        portrait.sprite = Resources.Load<Sprite>($"Portraits/{dialogue.SpeakerName}");
        
        // if (portrait.sprite == null)
        // {
        //     dialogueModel();
        // }
        
        DialogueNode currentNode = dialogue.DialogueNodes[nodeIndex];
        RenderCurrentNode(currentNode);
        ToggleDialogueUI();
    }
    
    public void dialogueModel(GameObject character)
    {
        GameObject NPCModelSpawn = null;
        GameObject target = character.transform.Find("CameraTarget").gameObject;

        //repurpose this so that it's not actually using the player and instead just the model
        NPCModelSpawn = Instantiate(target, portrait.transform.position, Quaternion.Euler(new Vector3(0f, 180f, 0f)));
        NPCModelSpawn.name = "ccClone";
        NPCModelSpawn.AddComponent<RotateObj>();
        NPCModelSpawn.transform.SetParent(GameObject.Find("Canvas").transform.Find("Panel").transform.Find("DialogueUI").transform.Find("PortraitPanel").transform.Find("Portrait"));

        if (GameObject.Find("Canvas").transform.Find("Panel").transform.Find("DialogueUI").transform.Find("PortraitPanel").transform.Find("Portrait") == null)
        {
            Debug.Log("no luck");
        }

        foreach (Transform child in NPCModelSpawn.transform.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = 5; //layer 5 literally just means it's in the ui layer
        }

        RenderModel(NPCModelSpawn);
    }

    public void RenderModel(GameObject model)
    {
        if (transform.childCount > 0)
        {
            GameObject currentChild = transform.GetChild(0).gameObject;
            Destroy(currentChild);
        }
    }
    /////
    ///

    private void RenderCurrentNode(DialogueNode node)
    {
        ClearNodeResponses();
        nodeText.text = node.NodeText;
        for (int i = 0; i < node.NodeResponses.Count; i++)
        {
            NodeResponse response = node.NodeResponses[i];
            GameObject toInstantiate = Instantiate(nodeResponsePrefab, nodeContentOrganizer.transform.position, Quaternion.identity);
            toInstantiate.GetComponentInChildren<TMP_Text>().text = response.NodeResponseText;
            toInstantiate.transform.SetParent(nodeContentOrganizer.transform, false); //test

            toInstantiate.AddComponent<DialogueHighlight>(); //Used in order to change text color when highlighting over response
            toInstantiate.GetComponent<Button>().onClick.AddListener(delegate { NextNode(response.NextNode); });
            if (response.Then != null)
            {
                Debug.Log($"{response.Then}");
                toInstantiate.GetComponent<Button>().onClick.AddListener(delegate { Invoke(response.Then, 0); });
            }


        }
    }

    private void ClearNodeResponses()
    {
        foreach (Transform child in nodeContentOrganizer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void NextNode(int index)
    {
        if (index != -1 && index != -2 && index != -999)
        {
            nodeIndex = index;
            DialogueNode currentNode = jsonReader.dialogues.GetDialogue(dialogueIndex).DialogueNodes[nodeIndex];
            RenderCurrentNode(currentNode);
        }
        else if (index == -1)
        {
            CloseDialogue();
        }
        else if (index == -2)
        {
            CloseDialogue();
            DemoManager.Instance.RecruitQuinton();
            GameObject.Find("Quinton").GetComponent<NPCMovement>().SendQuintonToBackroom();
        }
        else
        {
            BasePlayer quinton = jsonReader.allies.GetBasePlayerByID(1);
            
            CloseDialogue();
            DemoManager.Instance.LoadScene("GigDemo");

        }
    }

    private void CloseDialogue()
    {
        //locks cursor while inactive
        Cursor.lockState = CursorLockMode.Locked;
        //turn QuestUI back on when dialogue is closed
        questUI.SetActive(true);
        Dialogue dialogue = jsonReader.dialogues.GetDialogue(dialogueIndex);
        dialogue.Exhausted = true;
        Animator anim = dialogueUI.GetComponent<Animator>();
        dialogueUI.GetComponent<Animator>().Play("dialogueBoxClose");
        //Invoke("ToggleDialogueUI", anim.GetCurrentAnimatorClipInfo(0).Length);
        GameManager.Instance.TogglePlayerControls();

    }

    // Spawn a text bubble prefab above the given character's gameobject with the given text.
    public void SpawnTextBubble(GameObject character, string text)
    {
        GameObject target = character.transform.Find("CameraTarget").gameObject;
        GameObject toInstantiate = Instantiate(textBubblePrefab, (target.transform.position + new Vector3(0, 0.5f, 0)), Quaternion.identity, target.transform);
        toInstantiate.GetComponentInChildren<TMP_Text>().text = text;
    }

    public void QuintonDialogueTrigger()
    {
        DemoManager.Instance.CreateSignpostMessage(DemoManager.help4);
        DemoManager.Instance.CreateSignpostMessage(DemoManager.help3);
    }

    public void TriggerAssistanceQuip(int actingCharacter)
    {
        if (!PartyManager.Instance.inCombat)
        {
            return;
        }

        Quip assistanceQuip = jsonReader.quips.quips[4];
        GameObject model = CombatManager.Instance.GetModelByID(actingCharacter);
        SpawnTextBubble(model, assistanceQuip.Text);
    }

    public void FinalHelpTrigger()
    {
        DemoManager.Instance.CreateSignpostMessage(DemoManager.help9);
        DemoManager.Instance.completedDemo = true;
    }

    public void ChooseAppropriateQuip(int actingCharacter, bool inCombat)
    {
        Quip chosenQuip;
        List<Quip> relevantQuips = new List<Quip>();
        GameObject model;
        BasePlayer ac = PartyManager.Instance.FindPartyMemberById(actingCharacter);

        List<Quip> specificQuips = new List<Quip>();
        List<Quip> genericQuips = new List<Quip>();
        IEnumerable<Quip> result;

        if (inCombat && (inCombat == PartyManager.Instance.inCombat))
        {
            Debug.Log("We're in combat.");
            foreach (CombatStatus cs in ac.CombatStatuses)
            {
                result = specificQuips.Concat(jsonReader.quips.GetQuipsByCombatStatus(cs.Type));
                foreach (Quip q in result)
                {
                    relevantQuips.Add(q);
                }
            }
            if (relevantQuips.Count == 0)
            {
                result = genericQuips.Concat(jsonReader.quips.GetGenericCombatQuips());
                foreach (Quip q in result)
                {
                    relevantQuips.Add(q);
                }
            }
            model = CombatManager.Instance.GetModelByID(actingCharacter);
        }
        else if (!inCombat && (inCombat == PartyManager.Instance.inCombat))
        {
            Debug.Log("We're not in combat.");
            foreach (Status s in ac.CiFData.Statuses)
            {
                result = specificQuips.Concat(jsonReader.quips.GetQuipsByCiFStatus(s.Type));
                foreach (Quip q in result)
                {
                    relevantQuips.Add(q);
                }
            }
            if (relevantQuips.Count == 0)
            {
                result = genericQuips.Concat(jsonReader.quips.GetGenericNoncombatQuips());
                foreach (Quip q in result)
                {
                    relevantQuips.Add(q);
                }
            }
            model = PartyManager.Instance.GetModelByID(actingCharacter);
        }
        else
        {
            return;
        }

        Debug.Log(relevantQuips.Count);
        chosenQuip = relevantQuips[Random.Range(0, relevantQuips.Count - 1)];
        SpawnTextBubble(model, chosenQuip.Text);
    }


    
}