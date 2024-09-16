using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasePlayer : ITargetable
{
    [SerializeField] private string name;
    [SerializeField] private int id;
    [SerializeField] private int health;
    [SerializeField] private int flourish;
    [SerializeField] private int shield;
    [SerializeField] private int elan;
    [SerializeField] private BaseClass playerClass;
    [SerializeField] private int fame;
	[SerializeField] private int gold;
    [SerializeField] private List<BaseItem> equipment = new List<BaseItem>();
	[SerializeField] private List<BaseItem> inventory = new List<BaseItem>();
    [SerializeField] private CiFData cifData;
    [SerializeField] private bool ownsTurn;
    [SerializeField] private List<CombatStatus> combatStatuses = new List<CombatStatus>();
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject battleModel;
    [SerializeField] private float locationX;
    [SerializeField] private float locationY;
    [SerializeField] private float locationZ;

    public BasePlayer()
    {
        this.name = "Default";
        this.id = 0;
        this.health = 0;
        this.flourish = 0;
        this.shield = 0;
        this.elan = 0;
        this.playerClass = null;
        this.fame = 0;
        this.gold = 0;
        this.equipment = new List<BaseItem>();
        this.inventory = new List<BaseItem>();
        this.cifData = new CiFData();
        this.ownsTurn = false;
        this.combatStatuses = new List<CombatStatus>();
        this.model = null;
        this.battleModel = null;
        this.locationX = 0.0f;
        this.locationY = 0.0f;
        this.locationZ = 0.0f;
    }

    public BasePlayer(string name, int id, int health, int flourish, int shield, int elan, BaseClass playerClass, int fame, int gold, List<BaseItem> equipment, List<BaseItem> inventory, GameObject model, float locationX, float locationY, float locationZ)
    {
        this.name = name;
        this.id = id;
        this.health = health;
        this.flourish = flourish;
        this.shield = shield;
        this.elan = elan;
        this.playerClass = playerClass;
        this.fame = fame;
        this.gold = gold;
        this.equipment = equipment;
        this.inventory = inventory;
        this.cifData = new CiFData();
        this.ownsTurn = false;
        this.model = model;
        this.locationX = locationX;
        this.locationY = locationY;
        this.locationZ = locationZ;
    }

    public override string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public int ID
    {
        get { return this.id; }
        set { this.id = value; }
    }

    public override int Health
    {
        get { return this.health; }
        set { this.health = value; }
    }

    public override int Flourish
    {
        get { return this.flourish; }
        set { this.flourish = value; }
    }

    public override int Shield
    {
        get { return this.shield; }
        set { this.shield = value; }
    }

    public override int Elan
    {
        get { return this.elan; }
        set { this.elan = value; }
    }
    public float LocationX
    {
        get { return this.locationX; }
        set { this.locationX = value; }
    }

    public float LocationY
    {
        get { return this.locationY; }
        set { this.locationY = value; }
    }
    public float LocationZ
    {
        get { return this.locationZ; }
        set { this.locationZ = value; }
    }

    public BaseClass PlayerClass
    {
        get { return this.playerClass; }
        set { this.playerClass = value; }
    }

    public int Fame
    {
        get { return this.fame; }
        set { this.fame = value; }
    }

    public int Gold
    {
        get { return this.gold; }
        set { this.gold = value; }
    }

    public List<BaseItem> Equipment
    {
        get { return this.equipment; }
        set { this.equipment = value; }
    }

    public List<BaseItem> Inventory
    {
        get { return this.inventory; }
        set { this.inventory = value; }
    }

    public override CiFData CiFData
    {
        get { return this.cifData; }
        set { this.cifData = value; }
    }

    public bool OwnsTurn
    {
        get { return this.ownsTurn; }
        set { this.ownsTurn = value; }
    }

    public override List<CombatStatus> CombatStatuses
    {
        get { return this.combatStatuses; }
        set { this.combatStatuses = value; }
    }

    public GameObject Model
    {
        get { return this.model; }
        set { this.model = value; }
    }

    public GameObject BattleModel
    {
        get { return this.battleModel; }
        set { this.battleModel = value; }
    }

    public override void RemoveCombatStatus(CombatStatus.CombatStatusTypes type)
    {
        foreach (CombatStatus cs in this.CombatStatuses.ToArray())
        {
            if (cs.Type == type)
            {
                this.CombatStatuses.Remove(cs);
            }
        }
    }

    public override bool HasCombatStatusType(CombatStatus.CombatStatusTypes type)
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

[System.Serializable]
public class Allies
{
    [SerializeField] public List<BasePlayer> allies = new List<BasePlayer>();

    public BasePlayer GetBasePlayerByID(int id)
    {
        foreach (BasePlayer ally in allies)
        {
            if (ally.ID == id)
            {
                return ally;
            }
        }
        return null;
    }
}
