using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DELP;
using Viv;

[CreateAssetMenu]
public class DELPEntity : ScriptableObject
{
    [SerializeField] private List<string> facts;
    [SerializeField] private List<string> strictRules;
    [SerializeField] private List<string> defeasibleRules;
    private Queue<DELPMessage> preparedData = new Queue<DELPMessage>();

    public void AddFact(string fact)
    {
        if (!facts.Contains(fact))
        {
            facts.Add(fact);
            PrepareAndUpdate();
        }
    }

    public void RemoveFact(string fact)
    {
        if (facts.Contains(fact))
        {
            facts.Remove(fact);
            PrepareAndUpdate();
        }
    }

    public void AddStrictRule(string strictRule)
    {
        if (!strictRules.Contains(strictRule))
        {
            strictRules.Add(strictRule);
            PrepareAndUpdate();
        }
    }

    public void RemoveStrictRule(string strictRule)
    {
        if (strictRules.Contains(strictRule))
        {
            strictRules.Remove(strictRule);
            PrepareAndUpdate();
        }
    }

    public void AddDefeasibleRule(string defeasibleRule)
    {
        if (!defeasibleRules.Contains(defeasibleRule))
        {
            defeasibleRules.Add(defeasibleRule);
            PrepareAndUpdate();
        }
    }

    public void RemoveDefeasibleRule(string defeasibleRule)
    {
        if (defeasibleRules.Contains(defeasibleRule))
        {
            defeasibleRules.Remove(defeasibleRule);
            PrepareAndUpdate();
        }
    }

    public void PrepareEntityData()
    {
        foreach (string fact in this.facts)
        {
            DELPBelief belief = new DELPBelief(fact);
            string data = JsonUtility.ToJson(belief);
            DELPMessage msg = new DELPMessage(0, "delp", data);
            preparedData.Enqueue(msg);
        }

        foreach (string srule in this.strictRules)
        {
            DELPBelief belief = new DELPBelief(srule);
            string data = JsonUtility.ToJson(belief);
            DELPMessage msg = new DELPMessage(1, "delp", data);
            preparedData.Enqueue(msg);
        }

        foreach (string drule in this.defeasibleRules)
        {
            DELPBelief belief = new DELPBelief(drule);
            string data = JsonUtility.ToJson(belief);
            DELPMessage msg = new DELPMessage(2, "delp", data);
            preparedData.Enqueue(msg);
        }
    }

    public void UpdateKnowledgeBase()
    {
        while (preparedData.Count != 0 )
        {
            DELPMessage msg = preparedData.Dequeue();
            TCPTestClient.Instance.SendMessage<DELPMessage>(msg);
        }
    }

    public void PrepareAndUpdate()
    {
        PrepareEntityData();
        UpdateKnowledgeBase();
        Viv.Viv.Instance.EvaluateCurrentSupertask();
    }

    public void QueryKnowledgeBase(string query)
    {
        DELPQuery q = new DELPQuery(query);
        DELPMessage msg = q.PrepareQuery();
        TCPTestClient.Instance.SendMessage<DELPMessage>(msg);
    }

    public List<string> Facts
    {
        get { return facts; }
    }

    public List<string> StrictRules
    {
        get { return strictRules; }
    }

    public List<string> DefeasibleRules
    {
        get { return defeasibleRules; }
    }
}
