using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Viv;

public class CustomDictionary : MonoBehaviour
{
    [SerializeField] private List<SupertaskBindings> supertaskBindings = new List<SupertaskBindings>();
    [SerializeField] private List<BehaviorBindings> behaviorBindings = new List<BehaviorBindings>();
    private Dictionary<string, List<string>> supertaskDict = new Dictionary<string, List<string>>();
    private Dictionary<string, List<string>> behaviorDict = new Dictionary<string, List<string>>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var kvp in supertaskBindings) 
        {
            supertaskDict[kvp.key] = kvp.val;
        }

        foreach (var kvp in behaviorBindings)
        {
            behaviorDict[kvp.key] = kvp.val;
        }

        Debug.Log(supertaskDict["SabotagePlayer"]);
    }

    public Dictionary<string, List<string>> SupertaskDict
    {
        get { return supertaskDict; }
    }

    public Dictionary<string, List<string>> BehaviorDict
    {
        get { return behaviorDict; }
    }
}
