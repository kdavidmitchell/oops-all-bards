using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllyWME
{
    // General statistics
    [SerializeField] public int id;
    [SerializeField] public float locationX;
    [SerializeField] public float locationY;
    [SerializeField] public float locationZ;
    [SerializeField] public bool inCombat;

    // Combat statistics
    [SerializeField] public int health;
    [SerializeField] public int flourish;
    [SerializeField] public int shield;
    [SerializeField] public int elan;
    [SerializeField] public int[] abilityIDs;
    [SerializeField] public int[] abilityCosts;
    [SerializeField] public string[] abilityTypes;
    [SerializeField] public bool ownsTurn;

    // CiF statistics
    [SerializeField] public int[] partyIDs;
    [SerializeField] public int[] partyAffinities;
    [SerializeField] public int[] statusIDs;
    [SerializeField] public int[] traitIDs;

    public AllyWME(BasePlayer player)
    {
        this.id = player.ID;
        // TODO: When we have 3D movement of player/allies, this will become important.
        this.locationX = player.LocationX;
        this.locationY = player.LocationY;
        this.locationZ = player.LocationZ;
        this.inCombat = PartyManager.Instance.inCombat;
        this.health = player.Health;
        this.flourish = player.Flourish;
        this.shield = player.Shield;
        this.elan = player.Elan;
        // Handle ability data
        this.abilityIDs = new int[player.PlayerClass.Abilities.Count];
        this.abilityCosts = new int[player.PlayerClass.Abilities.Count];
        this.abilityTypes = new string[player.PlayerClass.Abilities.Count];
        for (int i = 0; i < player.PlayerClass.Abilities.Count; i++)
        {
            abilityIDs[i] = player.PlayerClass.Abilities[i].ID;
            abilityCosts[i] = player.PlayerClass.Abilities[i].Cost;
            abilityTypes[i] = player.PlayerClass.Abilities[i].CombatType.ToString();
        }
        this.ownsTurn = player.OwnsTurn;
        // Handle CiF data
        this.partyIDs = new int[PartyManager.Instance.currentParty.Count];
        this.partyAffinities = new int[PartyManager.Instance.currentParty.Count];
        this.statusIDs = new int[player.CiFData.Statuses.Count];
        this.traitIDs = new int[player.CiFData.Traits.Count];
        for (int i = 0; i < PartyManager.Instance.currentParty.Count; i++)
        {
            partyIDs[i] = PartyManager.Instance.currentParty[i].ID;
            partyAffinities[i] = PartyManager.Instance.currentParty[i].CiFData.GetAffinityByID(partyIDs[i]);
        }
        for (int i = 0; i < player.CiFData.Statuses.Count; i++)
        {
            statusIDs[i] = (int)player.CiFData.Statuses[i].Type;
        }
        for (int i = 0; i < player.CiFData.Traits.Count; i++)
        {
            traitIDs[i] = (int)player.CiFData.Traits[i].Type;
        }
    }   
}
