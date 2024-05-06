package abl.wmes;

import wm.WME;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

public class AllyWME extends WME {
	/** Whether or not this WME is on an active behavior tree */
	private boolean onTree;
	/** ID of the ally character */
	private int id;
	/** Whether or not the ally is in combat */
	private boolean inCombat;
	/** The x location of the ally */
	private double locationX;
	/** The y location of the ally */
	private double locationY;
	/** The z location of the ally */
	private double locationZ;
	/** The ally character's health */
	private int health;
	/** The ally character's flourish points */
	private int flourish;
	/** The ally character's shield */
	private int shield;
	/** The ally character's ability IDs in array form */
	private int[] abilityIDs;
	/** The ally character's ability costs in array form */
	private int[] abilityCosts;
	/** The ally character's combat ability types in array form */
	private String[] abilityTypes;
	/** Whether this ally owns the current turn */
	private boolean ownsTurn;
	/** The character IDs of the current party members */
	private int[] partyIDs;
	/** The affinity values this ally has for other party members */
	private int[] partyAffinities;
	/** The status IDs of this ally */
	private int[] statusIDs;
	/** The trait IDs of this ally */
	private int[] traitIDs;
	
	/**
	 * Instantiates a working memory element for tracking an ally.
	 */
	public AllyWME(int id, boolean inCombat, float locationX, float locationY, float locationZ, int health, 
			int flourish, int shield, int[] abilityIDs, int[] abilityCosts, String[] abilityTypes, boolean ownsTurn, 
			int[] partyIDs, int[] partyAffinities, int[] statusIDs, int[] traitIDs) {
		this.onTree = false;
		this.id = id;
		this.inCombat = inCombat;
		this.locationX = locationX;
		this.locationY = locationY;
		this.locationZ = locationZ;
		this.health = health;
		this.flourish = flourish;
		this.shield = shield;
		this.abilityIDs = abilityIDs;
		this.abilityCosts = abilityCosts;
		this.abilityTypes = abilityTypes;
		this.ownsTurn = ownsTurn;
		this.partyIDs = partyIDs;
		this.partyAffinities = partyAffinities;
		this.statusIDs = statusIDs;
		this.traitIDs = traitIDs;
	}
	
	public AllyWME(JSONObject data) {
		// Handle simple data structures.
		this.onTree = false;
		this.id = (int)(long) data.get("id");
		this.inCombat = (boolean) data.get("inCombat");
		this.locationX = (double) data.get("locationX");
		this.locationY = (double) data.get("locationY");
		this.locationZ = (double) data.get("locationZ");
		this.health = (int)(long) data.get("health");
		this.flourish = (int)(long) data.get("flourish");
		this.shield = (int)(long) data.get("shield");
		this.ownsTurn = (boolean) data.get("ownsTurn");
		// Handle arrays.
		JSONArray jsonArray = (JSONArray) data.get("abilityIDs");
		this.abilityIDs = new int[jsonArray.size()];
		for (int i = 0; i < jsonArray.size(); i++) {
		    this.abilityIDs[i] = (int)(long) jsonArray.get(i);
		}
		jsonArray = (JSONArray) data.get("abilityCosts");
		this.abilityCosts = new int[jsonArray.size()];
		for (int i = 0; i < jsonArray.size(); i++) {
		    this.abilityCosts[i] = (int)(long) jsonArray.get(i);
		}
		jsonArray = (JSONArray) data.get("abilityTypes");
		this.abilityTypes = new String[jsonArray.size()];
		for (int i = 0; i < jsonArray.size(); i++) {
		    this.abilityTypes[i] = (String) jsonArray.get(i);
		}
		jsonArray = (JSONArray) data.get("partyIDs");
		this.partyIDs = new int[jsonArray.size()];
		for (int i = 0; i < jsonArray.size(); i++) {
		    this.partyIDs[i] = (int)(long) jsonArray.get(i);
		}
		jsonArray = (JSONArray) data.get("partyAffinities");
		this.partyAffinities = new int[jsonArray.size()];
		for (int i = 0; i < jsonArray.size(); i++) {
		    this.partyAffinities[i] = (int)(long) jsonArray.get(i);
		}
		jsonArray = (JSONArray) data.get("statusIDs");
		this.statusIDs = new int[jsonArray.size()];
		for (int i = 0; i < jsonArray.size(); i++) {
		    this.statusIDs[i] = (int)(long) jsonArray.get(i);
		}
		jsonArray = (JSONArray) data.get("traitIDs");
		this.traitIDs = new int[jsonArray.size()];
		for (int i = 0; i < jsonArray.size(); i++) {
		    this.traitIDs[i] = (int)(long) jsonArray.get(i);
		}
	}
	
	public boolean getOnTree() { return onTree; }
	
	public int getID() { return id; }
	
	public boolean getInCombat() { return inCombat; }
	
	public double getLocationX() { return locationX; }
	
	public double getLocationY() { return locationY; }
	
	public double getLocationZ() { return locationZ; }
	
	public int getHealth() { return health; }
	
	public int getFlourish() { return flourish; }
	
	public int getShield() { return shield; }
	
	public int[] getAbilityIDs() { return abilityIDs; }
	
	public int[] getAbilityCosts() { return abilityCosts; }
	
	public String[] getAbilityTypes() { return abilityTypes; }

	public boolean getOwnsTurn() { return ownsTurn; }
	
	public int[] getPartyIDs() { return partyIDs; }
	
	public int[] getPartyAffinities() { return partyAffinities; }
	
	public int[] getStatusIDs() { return statusIDs; }
	
	public int[] getTraitIDs() { return traitIDs; }
	
	public void setOnTree(boolean value) { this.onTree = value; }
}
