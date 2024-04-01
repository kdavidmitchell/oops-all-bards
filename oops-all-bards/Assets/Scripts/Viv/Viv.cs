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
            testST.DispatchBehaviors();
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
        // A boolean indicating whether or not the supertask is currently being carried out.
        [SerializeField] private bool inProgress = false;
        // The evaluation of the supertask in terms of its behaviors, containing the TFUs associated with each behavior.
        [SerializeField] private TFU[] evaluation;

        public Supertask(string name, int actingCharacter, int targetCharacter, CustomDictionary bindings)
        {
            this.name = name;
            this.actingCharacter = actingCharacter;
            this.targetCharacter = targetCharacter;
            this.behaviors = this.FormBehaviors(name, bindings);
        }

        private VivWME ToVivWME()
        {
            VivWME wme = new VivWME(actingCharacter);
            return wme;
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

        // A function that dispatches all behaviors belonging to the supertask to the ABL agent.
        public void DispatchBehaviors()
        {
            VivWME wme = this.ToVivWME();
            string[] behaviors = new string[this.behaviors.Count];
            for (int i = 0; i < this.behaviors.Count; i++)
            {
                behaviors[i] = this.behaviors[i].Name;
            }
            wme.ToSpawn = behaviors;
            ABLMessage msg = wme.ToABLMessage();
            TCPTestClient.Instance.SendMessage<ABLMessage>(msg);

            this.inProgress = true;
        }

        // A function that evaluates the given supertask with respect to its component behaviors.
        private void Evaluate()
        {
            if (this.evaluation == null)
            {
                // Create a new evaluation matrix, m x n, where m = the number of the behaviors belonging to the supertask, and n = 3 (storing truths, falsities, and uncertainties of the behavior).
                this.evaluation = new TFU[behaviors.Count];
            }

            // Evaluate each behavior.
            for (int i = 0; i < behaviors.Count; i++)
            {
                Behavior currentBehavior = behaviors[i];
                currentBehavior.Evaluate();
                evaluation[i] = ScoreBehavior(currentBehavior);
            }
        }

        // A utility function used to count the number of truths (YES), falsities (NO), and uncertainties (UNDECIDED) belonging to each behavior.
        private TFU ScoreBehavior(Behavior behavior)
        {
            TFU score = new TFU();
            string[] eval = behavior.Evaluation;

            for (int i = 0; i < eval.Length; i++)
            {
                string current = eval[i];
                if (current == "YES")
                {
                    score.Truths += 1;
                } else if (current == "NO")
                {
                    score.Falsities += 1;
                } else
                {
                    score.Uncertainties += 1;
                }
            }

            return score;
        }

        public string Name
        {
            get { return this.name; }
        }

        public List<Behavior> Behaviors
        {
            get { return this.behaviors; }
        }

        public bool InProgress
        {
            get { return this.inProgress; }
            set { this.inProgress = value; }
        }

        public TFU[] Evaluation
        {
            get { return this.evaluation; }
        }
    }

    [System.Serializable]
    public class TFU
    {
        [SerializeField] private int truths;
        [SerializeField] private int falsities;
        [SerializeField] private int uncertainties;

        public TFU()
        {
            this.truths = 0;
            this.falsities = 0;
            this.uncertainties = 0;
        } 

        public TFU(int truths, int falsities, int uncertainties)
        {
            this.truths = truths;
            this.falsities = falsities;
            this.uncertainties = uncertainties;
        }

        public int[] ToArray()
        {
            int[] array = { this.truths, this.falsities, this.uncertainties };
            return array;
        }

        public int Truths
        {
            get { return this.truths; }
            set { this.truths = value; }
        }

        public int Falsities
        {
            get { return this.falsities; }
            set { this.falsities = value; }
        }

        public int Uncertainties
        {
            get { return this.uncertainties; }
            set { this.uncertainties = value; }
        }
    }

    [System.Serializable]
    public class Behavior
    {
        // The name of the behavior (should match the name of the corresponding ABL behavior).
        [SerializeField] private string name;
        // The integer ID of the acting character.
        [SerializeField] private int actingCharacter;
        // A list of the assumptions on which each behavior is built.
        [SerializeField] private List<Assumption> assumptions = new List<Assumption>();
        // The evaluation of the behavior in terms of its assumptions, represented as an array of strings, i.e. ["YES","NO","UNDECIDED"].
        [SerializeField] private string[] evaluation;


        public Behavior(string name, int actingCharacter, int targetCharacter, CustomDictionary bindings)
        {
            this.name = name;
            this.actingCharacter = actingCharacter;
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

        // A function that evaluates the given behavior by validating/failing to validate the assumptions on which it is built.
        public void Evaluate()
        {
            if (this.evaluation == null)
            {
                this.evaluation = new string[assumptions.Count];
            }

            for (int i = 0; i < assumptions.Count; i++)
            {
                assumptions[i].Validate();
                evaluation[i] = assumptions[i].IsValid.ToString();
            }
        }

        public string Name
        {
            get { return this.name; }
        }

        public List<Assumption> Assumptions
        {
            get { return this.assumptions; }
        }

        public string[] Evaluation
        {
            get { return this.evaluation; }
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
        public enum Validity { DEFAULT, YES, NO, UNDECIDED };
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

        public Validity IsValid
        {
            get { return this.isValid; }
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
