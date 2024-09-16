using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    private int currentQuest = 0;
    public int CurrentQuest => currentQuest;
    private int currentQuestStage = 0;
    public int CurrentQuestStage => currentQuestStage;
    public static QuestManager Instance => QuestManager._instance;
    public JSONReader jsonReader;
    public GameObject questUI;
    public GameObject questPrefab;
    public GameObject questMarkerPrefab;

    // Singleton pattern
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

    void Start()
    {
        RenderQuest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RenderQuest()
    {
        Quest quest = jsonReader.quests.GetQuest(currentQuest);
        GameObject questToRender = Instantiate(questPrefab, Vector3.zero, Quaternion.identity);
        questToRender.transform.SetParent(questUI.transform, false);
        questToRender.transform.Find("QuestName").GetComponent<TMP_Text>().text = quest.Name;
        questToRender.transform.Find("QuestStageText").GetComponent<TMP_Text>().text = quest.Stages[currentQuestStage].DisplayText;
        UpdateQuestMarker(quest, false);
    }

    private void UpdateQuestMarker(Quest quest, bool destroy)
    {
        GameObject model = GameObject.Find($"{quest.Stages[currentQuestStage].NPCTargetName}");
        Transform target = model.transform.Find("CameraTarget");
        if (!destroy)
        {
            GameObject toInstantiate = Instantiate(questMarkerPrefab, target.position, Quaternion.identity);
            toInstantiate.transform.SetParent(target, true);
        } else
        {
            foreach (Transform child in target)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void MarkStageComplete(int stageID)
    {
        ClearQuestUI();
        Quest quest = jsonReader.quests.GetQuest(currentQuest);
        quest.Stages[stageID].Complete = true;
        if (stageID == (quest.Stages.Count - 1))
        {
            MarkQuestComplete();
            return;
        }

        UpdateQuestMarker(quest, true);
        currentQuestStage++;
        RenderQuest();
    }

    public void MarkQuestComplete()
    {
        Quest quest = jsonReader.quests.GetQuest(currentQuest);
        quest.Complete = true;
    }

    private void ClearQuestUI()
    {
        foreach (Transform child in questUI.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

[System.Serializable]
public class Quests
{
    [SerializeField] public List<Quest> quests = new List<Quest>();

    public Quest GetQuest(int id)
    {
        foreach (Quest q in quests)
        {
            if (q.ID == id)
            {
                return q;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Quest
{
    [SerializeField] private string name;
    [SerializeField] private int id;
    [SerializeField] private List<QuestStage> stages;
    [SerializeField] private bool complete;

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public int ID
    {
        get { return this.id; }
        set { this.id = value; }
    }

    public List<QuestStage> Stages
    {
        get { return this.stages; }
        set { this.stages = value; }
    }

    public bool Complete
    {
        get { return this.complete; }
        set { this.complete = value; }
    }
}

[System.Serializable]
public class QuestStage
{
    // [SerializeField] private bool hasNPCTarget;
    [SerializeField] private int id;
    [SerializeField] private string npcTargetName;
    [SerializeField] private bool complete;
    [SerializeField] private string displayText;

    public int ID
    {
        get { return this.id; }
        set { this.id = value; }
    }

    public string NPCTargetName
    {
        get { return this.npcTargetName; }
        set { this.npcTargetName = value; }
    }

    public bool Complete
    {
        get { return this.complete; }
        set { this.complete = value; }
    }

    public string DisplayText
    {
        get { return this.displayText; }
        set { this.displayText = value; }
    }
}
