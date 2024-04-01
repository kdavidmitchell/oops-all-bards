using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ABLMessage
{
    // The code intended to be read by Java server.
    public int code;
    // The message intended for the Java server.
    public string msg;
    // The data represented as a string sent to the Java server.
    // This could be for example a JSON representation of an AllyWME.
    public string data;

    public ABLMessage(int code, string msg, string data)
    {
        this.code = code;
        this.msg = msg;
        this.data = data;
    }
}