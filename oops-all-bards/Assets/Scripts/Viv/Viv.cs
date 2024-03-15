using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DELP;

namespace Viv
{
    public class Viv : MonoBehaviour
    {
        [SerializeField] private List<Supertask> supertasks;
        private Supertask currentSupertask;
        [SerializeField] private DELPTest test;
        private bool b = false;

        void Start()
        {
            
        }

        void Update()
        {
            if (test.finished && !b)
            {
                Assumption a = new Assumption("test", "Penguin", "opus");
                a.Validate();
                b = true;
            }
        }
    }

    [System.Serializable]
    public class Supertask
    {
        [SerializeField] private string name;
        [SerializeField] private List<Behavior> behaviors;
    }

    [System.Serializable]
    public class Behavior
    {
        [SerializeField] private string name;
        [SerializeField] private List<Assumption> assumptions;
    }

    [System.Serializable]
    public class Assumption
    {
        // A string representing the name of the character making this assumption, so that the correct knowledgebase can be queried.
        [SerializeField] private string owner;
        // The predicate used in the assumption, i.e. "StrongerThan".
        [SerializeField] private string predicate;
        // The subject on which the predicate is meant to be compared, usually the name of another character.
        [SerializeField] private string subject;
        // A temporary DELPResponse to store a returned message from the server.
        [SerializeField] private DELPResponse tmpResponse;
        // Whether or not the assumption holds true for the given owner, predicate, and subject.
        enum Validity { DEFAULT, YES, NO, UNDECIDED };
        [SerializeField] private Validity isValid;

        public Assumption()
        {
            this.owner = "default";
            this.predicate = "default";
            this.subject = "default";
            this.isValid = Validity.DEFAULT;
        }

        public Assumption(string owner, string predicate, string subject)
        {
            this.owner = owner;
            this.predicate = predicate;
            this.subject = subject;
            this.isValid = Validity.DEFAULT;
        }

        // A utility function to transform the given assumption into a string format parsable by a DELP knowledgebase.
        public override string ToString()
        {
            string toBuild = string.Format("{0}({1})", predicate, subject);
            return toBuild; 
        }

        // A utility function to query a DELP knowledgebase with the given assumption and validate it.
        public void Validate()
        {
            EventManager.Instance.SubscribeToEvent(EventType.DelpResponse, AssignDELPResponse);

            DELPQuery query = new DELPQuery(this.ToString());
            DELPMessage msg = query.PrepareQuery();
            TCPTestClient.Instance.SendMessage<DELPMessage>(msg);
        }

        private void AssignDELPResponse()
        {
            Debug.Log("Received DELP response; assigning to assumption.");
            this.tmpResponse = (DELPResponse)EventManager.Instance.EventData;
            
            if (this.tmpResponse.msg == this.ToString())
            {
                if (this.tmpResponse.data.answer.Contains("YES"))
                {
                    this.isValid = Validity.YES;
                } else if (this.tmpResponse.data.answer.Contains("NO"))
                {
                    this.isValid = Validity.NO;
                } else if (this.tmpResponse.data.answer.Contains("UNDECIDED"))
                {
                    this.isValid = Validity.UNDECIDED;
                } else
                {
                    this.isValid = Validity.DEFAULT;
                }
                Debug.Log("Assumption validated: " + this.isValid.ToString());
            } else
            {
                Debug.Log("Assumption could not be validated; mismatching query.");
            }

            EventManager.Instance.UnsubscribeToEvent(EventType.DelpResponse, null);
        }
    }

    [System.Serializable]
    // A class that represents a binding between the name of a given supertask, and a list of names of the ABL behaviors associated with that supertask.
    public class SupertaskBinding 
    {
        public string key;
        public List<string> val;
    }

    [System.Serializable]
    // A class that represents a binding between the name of an ABL behavior, and a list of assumptions.
    public class BehaviorBinding 
    {
        public string key;
        public List<string> val;
    }
}
