using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status
{
    // An enumeration of CiF status types
    public enum StatusTypes 
    {
        NONE,
        REQUIRES_ASSISTANCE,
        LEFT_HANGING,
        JOYFUL,
        PISSED,
        UPSET,
        CONFIDENT,
        FOCUSED,
        BORED
    }

    [SerializeField] private StatusTypes type;

    public StatusTypes Type
    {
        get { return this.type; }
        set { this.type = value; }
    }

    public Status(StatusTypes type)
    {
        this.type = type;
    }
}
