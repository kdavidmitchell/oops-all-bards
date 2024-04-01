package abl.wmes;

import java.util.*;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import wm.WME;

public class VivWME extends WME {
    /** Whether or not this WME is on an active behavior tree */
	private boolean onTree;
    /** ID of the acting character */
	private int id;
    /** An array of strings representing behaviors to be spawned */
    private String[] toSpawn;
    /** An array of strings representing behaviors to be stopped */
    private String[] toStop;

    public VivWME(JSONObject data) {
        this.onTree = false;
        this.id = (int)(long) data.get("id");
        JSONArray jsonArray = (JSONArray) data.get("toSpawn");
        this.toSpawn = new String[jsonArray.size()];
		for (int i = 0; i < jsonArray.size(); i++) {
		    this.toSpawn[i] = (String) jsonArray.get(i);
		}
        jsonArray = (JSONArray) data.get("toStop");
        this.toStop = new String[jsonArray.size()];
		for (int i = 0; i < jsonArray.size(); i++) {
		    this.toStop[i] = (String) jsonArray.get(i);
		}
    }

    public String toString() {
        StringBuilder builder = new StringBuilder();
        builder.append("VivWME: \n")
            .append("ID: " + this.id + "\n")
            .append("ToSpawn: " + Arrays.toString(this.toSpawn) + "\n")
            .append("ToStop: " + Arrays.toString(this.toStop));
        String result = builder.toString();
        return result;
    }

    public boolean getOnTree() { return onTree; }
	
	public int getID() { return id; }

    public String[] getToSpawn() { return toSpawn; }

    public String[] getToStop() { return toStop; }
}
