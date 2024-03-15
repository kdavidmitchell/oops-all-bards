using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DELP 
{
    [System.Serializable]
    public class DELPBelief
    {
        [SerializeField] public string belief;

        public DELPBelief(string belief)
        {
            this.belief = belief;
        }
    }

    [System.Serializable]
    public class DELPQuery
    {
        [SerializeField] public string query;

        public DELPMessage PrepareQuery()
        {
            string data = JsonUtility.ToJson(this);
            DELPMessage msg = new DELPMessage(4, "delp", data);
            return msg;
        }

        public DELPQuery(string query)
        {
            this.query = query;
        }
    }

    [System.Serializable]
    public class DELPMessage
    {
        // The code intended to be read by Java server.
        // 0 - fact
        // 1 - strict rule
        // 2 - defeasible rule
        // 3 - query
        public int code;
        // A string message intended to be read by Java server.
        public string msg;
        // The data represented as a string sent to the Java server.
        public string data;

        public DELPMessage(int code, string msg, string data)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }
    }

    [System.Serializable]
    public class DELPResponse
    {
        // The code returned by the Java server.
        public int code;
        // The message returned by the Java server.
        public string msg;
        // The data represented as a string returned by the Java server.
        public DELPAnswer data;
    }

    [System.Serializable]
    public class DELPAnswer
    {
        public string answer;
    }
}
