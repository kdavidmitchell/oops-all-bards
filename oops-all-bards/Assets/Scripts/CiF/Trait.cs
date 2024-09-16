using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trait
{
    public enum TraitTypes
    {
        NONE,
        SARDONIC,
        VENGEFUL,
        FORGIVING,
        TRUSTING,
        DISTRUSTING,
        EMOTIONAL,
        STONIC,
        GREEDY,
        GENEROUS,
        CONFORMIST,
        INDIVIDUALIST,
        VIOLENT,
        PACIFIST,
        OUTGOING,
        RESERVED,
        WITTY,
        DULL,
        KIND,
        CRUEL,
        CAREFUL,
        CARELESS,
        HONEST,
        DECEITFUL,
        DILIGENT,
        LAZY,
        ROBUST,
        FRAIL,
        CHATTY,
        QUIET,
        GENUINE
    }

    [SerializeField] private TraitTypes type;

    public TraitTypes Type
    {
        get { return this.type; }
        set { this.type = value; }
    }

    public Trait(TraitTypes type)
    {
        this.type = type;
    }
}
