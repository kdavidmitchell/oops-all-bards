using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DELP;

public class DELPTest : MonoBehaviour
{
    [SerializeField] private DELPEntity entity;
    private Queue<DELPMessage> preparedData = new Queue<DELPMessage>();
    public bool finished = false;

    void Start()
    {
        PrepareEntityData(entity);
    }

    void Update()
    {
        if (preparedData.Count != 0 )
        {
            DELPMessage msg = preparedData.Dequeue();
            TCPTestClient.Instance.SendMessage<DELPMessage>(msg);
        } else if (preparedData.Count == 0 && finished == false)
        {
            finished = true;
        }
    }

    private void PrepareEntityData(DELPEntity entity)
    {
        foreach (string fact in entity.Facts)
        {
            DELPBelief belief = new DELPBelief(fact);
            string data = JsonUtility.ToJson(belief);
            DELPMessage msg = new DELPMessage(0, "delp", data);
            preparedData.Enqueue(msg);
        }

        foreach (string srule in entity.StrictRules)
        {
            DELPBelief belief = new DELPBelief(srule);
            string data = JsonUtility.ToJson(belief);
            DELPMessage msg = new DELPMessage(1, "delp", data);
            preparedData.Enqueue(msg);
        }

        foreach (string drule in entity.DefeasibleRules)
        {
            DELPBelief belief = new DELPBelief(drule);
            string data = JsonUtility.ToJson(belief);
            DELPMessage msg = new DELPMessage(2, "delp", data);
            preparedData.Enqueue(msg);
        }
    }

    private DELPMessage PrepareQuery()
    {
        DELPQuery query = new DELPQuery("Penguin(opus)");
        DELPMessage msg = query.PrepareQuery();
        return msg;
    }
}
