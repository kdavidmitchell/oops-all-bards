using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DELPEntity : ScriptableObject
{
    [SerializeField] private List<string> facts;
    [SerializeField] private List<string> strictRules;
    [SerializeField] private List<string> defeasibleRules;

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
