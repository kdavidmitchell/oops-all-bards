using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Viv;

public class CustomDictionary : MonoBehaviour
{
    [SerializeField] private List<SupertaskBinding> bindings = new List<SupertaskBinding>();
    private Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
