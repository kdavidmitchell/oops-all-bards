using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DELPMessage : MonoBehaviour
{
    // The code intended to be read by Java server.
    // 0 - fact
    // 1 - strict rule
    // 2 - defeasible rule
    // 3 - query
    public int code;
    // The data represented as a string sent to the Java server.
    public string data;

    public DELPMessage(int code, string data)
    {
        this.code = code;
        this.data = data;
    }
}