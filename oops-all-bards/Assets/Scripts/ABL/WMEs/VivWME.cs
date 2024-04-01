using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VivWME
{
    [SerializeField] private int id;
    [SerializeField] private string[] toSpawn;
    [SerializeField] private string[] toStop;

    public VivWME(int id)
    {
        this.id = id;
        this.toSpawn = new string[0];
        this.toStop = new string[0];
    }

    public ABLMessage ToABLMessage()
    {
        int code = 2;
        string msg = "VivWME";
        string data = JsonUtility.ToJson(this);
        ABLMessage message = new ABLMessage(code, msg, data);
        return message;
    }

    public string[] ToSpawn
    {
        get { return this.toSpawn; }
        set { this.toSpawn = value; }
    }

    public string[] ToStop
    {
        get { return this.toStop; }
        set { this.toStop = value; }
    }
}