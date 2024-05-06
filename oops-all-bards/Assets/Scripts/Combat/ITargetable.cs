using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An interface that allows classes derived from it to be targetable by player/enemy actions.
public class ITargetable
{
    private string name;
    private int health;
    private int flourish;
    private int shield;
    private int elan;
    private List<CombatStatus> combatStatuses = new List<CombatStatus>();
    private CiFData cifData = new CiFData();
    private Animator anim;
    private CombatAnimationManager animan;

    public virtual string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public virtual int Health
    {
        get { return this.health; }
        set { this.health = value; }
    }

    public virtual int Flourish
    {
        get { return this.flourish; }
        set { this.flourish = value; }
    }

    public virtual int Shield
    {
        get { return this.shield; }
        set { this.shield = value; }
    }

    public virtual int Elan
    {
        get { return this.elan; }
        set { this.elan = value; }
    }

    public virtual List<CombatStatus> CombatStatuses
    {
        get { return this.combatStatuses; }
        set { this.combatStatuses = value; }
    }

    public virtual CiFData CiFData
    {
        get { return this.cifData; }
        set { this.cifData = value; }
    }

    public virtual Animator Anim
    {
        get { return this.anim; }
        set { this.anim = value; }
    }

    public virtual CombatAnimationManager Animan
    {
        get { return this.animan; }
        set { this.animan = value; }
    }

    public virtual void RemoveCombatStatus(CombatStatus.CombatStatusTypes type)
    {
        foreach (CombatStatus cs in this.CombatStatuses.ToArray())
        {
            if (cs.Type == type)
            {
                this.CombatStatuses.Remove(cs);
            }
        }
    }

    public virtual bool HasCombatStatusType(CombatStatus.CombatStatusTypes type)
    {
        foreach (CombatStatus cs in this.CombatStatuses)
        {
            if (cs.Type == type)
            {
                return true;
            }
        }
        return false;
    }
}
