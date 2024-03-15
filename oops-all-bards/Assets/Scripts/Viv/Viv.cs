using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DELP;

namespace Viv
{
    public class Viv : MonoBehaviour
    {
        // A reference to the bindings between supertasks, behaviors, and assumptions.
        [SerializeField] private CustomDictionary bindings;
        // A list of managed characters, using integer IDs.
        [SerializeField] private List<int> characters;
        // The current supertask that Viv is managing, usually passed to it by CiF.
        [SerializeField] private Supertask currentSupertask;
        // Whether or not Viv should use simulated CiF input.
        [SerializeField] private bool simulateCif = true;

        void Start()
        {
            if (simulateCif)
            {
                SimulateCiFStart();
            }
        }

        void Update()
        {
            
        }

        // A testing function that emulates CiF assigning a supertask for a given character.
        void SimulateCiFStart()
        {
            TestQuintonsRevenge();
        }

        // A testing function that simulates the "Quinton's Revenge" scenario.
        void TestQuintonsRevenge()
        {
            Supertask testST = new Supertask("SabotagePlayer", 1, 0, bindings);
            DebugSupertask(testST);
        }

        private void DebugSupertask(Supertask st)
        {
            Debug.Log(st.Name);
            foreach (Behavior b in st.Behaviors)
            {
                Debug.Log(b.Name);
                foreach (Assumption a in b.Assumptions)
                {
                    Debug.Log(a.ToString());
                }
            }
        }
    }

    [System.Serializable]
    public class Supertask
    {
        // The name of the given supertask.
        [SerializeField] private string name;
        // The list of behaviors associated with the supertask.
        [SerializeField] private List<Behavior> behaviors = new List<Behavior>();
        // The integer ID of the character engaged in the supertask.
        [SerializeField] private int actingCharacter;
        // The integer ID of the character targeted by the supertask.
        [SerializeField] private int targetCharacter;

        public Supertask(string name, int actingCharacter, int targetCharacter, CustomDictionary bindings)
        {
            this.name = name;
            this.actingCharacter = actingCharacter;
            this.targetCharacter = targetCharacter;
            this.behaviors = this.FormBehaviors(name, bindings);
        }

        // A utility function that assigns the list of behaviors associated with the supertask.
        private List<Behavior> FormBehaviors(string name, CustomDictionary bindings)
        {
            List<Behavior> behaviors = new List<Behavior>();
            List<string> behaviorNames = bindings.SupertaskDict[name];

            foreach (string bname in behaviorNames)
            {
                Behavior toAdd = new Behavior(bname, actingCharacter, targetCharacter, bindings);
                behaviors.Add(toAdd);
            }
            return behaviors;
        }

        public string Name
        {
            get { return this.name; }
        }

        public List<Behavior> Behaviors
        {
            get { return this.behaviors; }
        }
    }

    [System.Serializable]
    public class Behavior
    {
        // The name of the behavior (should match the name of the corresponding ABL behavior).
        [SerializeField] private string name;
        // A list of the assumptions on which each behavior is built.
        [SerializeField] private List<Assumption> assumptions = new List<Assumption>();

        public Behavior(string name, int actingCharacter, int targetCharacter, CustomDictionary bindings)
        {
            this.name = name;
            this.assumptions = this.FormAssumptions(name, actingCharacter, targetCharacter, bindings);
        }

        private List<Assumption> FormAssumptions(string name, int actingCharacter, int targetCharacter, CustomDictionary bindings)
        {
            // TODO: Change this to get string from targetCharacter ID. For now, only target is Player.
            string targetCharacterName = "Player";

            List<Assumption> assumptions = new List<Assumption>();
            List<string> assumptionNames = bindings.BehaviorDict[name];

            foreach (string predicate in assumptionNames)
            {
                Assumption toAdd = new Assumption(actingCharacter, predicate, targetCharacterName);
                assumptions.Add(toAdd);
            }
            return assumptions;
        }

        public string Name
        {
            get { return this.name; }
        }

        public List<Assumption> Assumptions
        {
            get { return this.assumptions; }
        }
    }

    [System.Serializable]
    public class Assumption
    {
        // An int ID representing the character making this assumption, so that the correct knowledgebase can be queried.
        [SerializeField] private int actingCharacter;
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
            this.actingCharacter = 0;
            this.predicate = "default";
            this.subject = "default";
            this.isValid = Validity.DEFAULT;
        }

        public Assumption(int actingCharacter, string predicate, string subject)
        {
            this.actingCharacter = actingCharacter;
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
    public class SupertaskBindings 
    {
        public string key;
        public List<string> val;
    }

    [System.Serializable]
    // A class that represents a binding between the name of an ABL behavior, and a list of assumptions.
    public class BehaviorBindings 
    {
        public string key;
        public List<string> val;
    }
}
