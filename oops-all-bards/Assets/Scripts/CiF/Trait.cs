using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trait
{
    [SerializeField] private string name;
    [SerializeField] private int id;

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public int ID
    {
        get { return this.id; }
        set { this.id = value; }
    }

    public Trait(string name, int id)
    {
        this.name = name;
        this.id = id;
    }
}
