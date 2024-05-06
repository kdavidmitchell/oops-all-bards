using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAbility
{
    [SerializeField] private string name;
	[SerializeField] private int id;
	[SerializeField] private string description;
	[SerializeField] private AbilityTypes type;
	[SerializeField] private CombatAbilityTypes combatType;
	[SerializeField] private int damage;
	[SerializeField] private int cost;
	[SerializeField] private int currentRank;
	[SerializeField] private int maxRank;
	[Tooltip("Name of animator clip acting character of ability should play")]
	[SerializeField] private string abilityAnimationClip;
	[Tooltip("Name of animator clip target character of ability should play")]
	[SerializeField] private string abilityReactionAnimationClip;
	[Tooltip("Array of particle prefabs to be spawned and played during animation")]
	[SerializeField] private string[] abilityAnimationParticles;
	[Tooltip("Array of sound effects to be played during animation")]
	[SerializeField] private string[] abilitySFX;

	public enum AbilityTypes
	{
		COMBAT,
		PASSIVE
	}

	public enum CombatAbilityTypes
	{
		NONE,
		ATTACK,
		DEFEND,
		HEAL,
		SUPPORT
	}

	public BaseAbility(string name, int id, string desc, AbilityTypes type, CombatAbilityTypes combatType, int damage, int cost, int currentRank, int maxRank)
	{
		this.name = name;
		this.id = id;
		this.description = desc;
		this.type = type;
		this.combatType = combatType;
		this.damage = damage;
		this.cost = cost;
		this.currentRank = currentRank;
		this.maxRank = maxRank;
	}

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

	public string Description
	{
		get { return this.description; }
		set { this.description = value; }
	}

	public AbilityTypes Type
	{
		get { return this.type; }
		set { this.type = value; }
	}

	public CombatAbilityTypes CombatType
	{
		get { return this.combatType; }
		set { this.combatType = value; }
	}

	public int Damage
	{
		get { return this.damage; }
		set { this.damage = value; }
	}

	public int Cost
	{
		get { return this.cost; }
		set { this.cost = value; }
	}

	public int CurrentRank
	{
		get { return this.currentRank; }
		set { this.currentRank = value; }
	}

	public int MaxRank
	{
		get { return this.maxRank; }
		set { this.maxRank = value; }
	}

	public string AbilityAnimationClip
    {
		get { return this.abilityAnimationClip; }
		set { this.abilityAnimationClip = value; }
    }

	public string AbilityReactionAnimationClip
	{
		get { return this.abilityReactionAnimationClip; }
		set { this.abilityReactionAnimationClip = value; }
	}

	public string[] AbilityAnimationParticles
    {
		get { return this.abilityAnimationParticles; }
        set { this.abilityAnimationParticles = value; }
    }
	
	public string[] AbilitySFX
    {
		get { return this.abilitySFX; }
		set { this.abilitySFX = value; }
    }
}
