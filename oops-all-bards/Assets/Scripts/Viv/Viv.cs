using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Viv
{
    public class Viv : MonoBehaviour
    {
        [SerializeField] private List<Supertask> supertasks;
        private Supertask currentSupertask;


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
        // Whether or not the assumption holds true for the given owner, predicate, and subject.
        enum Validity { DEFAULT, YES, NO, UNDECIDED };
        [SerializeField] private Validity isValid = Validity.DEFAULT;

        public Assumption()
        {
            this.owner = "default";
            this.predicate = "default";
            this.subject = "default";
        }

        public Assumption(string owner, string predicate, string subject)
        {
            this.owner = owner;
            this.predicate = predicate;
            this.subject = subject;
        }

        // A utility function to transform the given assumption into a string format parsable by a DELP knowledgebase.
        public string ToDelpQuery()
        {
            string toBuild = string.Format("{0}({1}).", predicate, subject);
            return toBuild; 
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
