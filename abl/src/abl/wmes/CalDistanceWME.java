package abl.wmes;

import wm.WME;
import java.lang.Math;
import java.util.*;
import abl.util.*;
import abl.wmes.*;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

public class CalDistanceWME extends WME {
	/**
	 * Instantiates a working memory element for tracking an ally.
	 */
	public CalDistanceWME() {}
	
	public boolean isNPCAround(ArrayHolder aroundChars, AllyWME curWME, WMEDictionary dict) {
		System.out.println("IN DistanceWME Ally ID:"+curWME.getID());
		Integer curCharacters[] = dict.getCharacters();
		int charAround[] = new int[15];
		int cnt = 0;
		double THRESHOLD = 10.0;
		boolean isAround = false;
		for(int c = 0; c < curCharacters.length; c++){
			System.out.println("curCharcter:"+curCharacters[c]);
			if (curCharacters[c] < 0 || curCharacters[c] == curWME.getID()) continue;
	    	AllyWME agentWME = dict.getCharacter(curCharacters[c]);
	    	double dist = Math.sqrt(
	    		(agentWME.getLocationX() - curWME.getLocationX()) * (agentWME.getLocationX() - curWME.getLocationX()) 
	    		+ (agentWME.getLocationY() - curWME.getLocationY()) * (agentWME.getLocationY() - curWME.getLocationY())
	    		+ (agentWME.getLocationZ() - curWME.getLocationZ() ) * (agentWME.getLocationZ()  - curWME.getLocationZ() ));
	    	
	    	System.out.println("dist difference:"+dist);
 			if(dist < THRESHOLD){
 				isAround = true;
 				charAround[cnt] = curCharacters[c];
 				cnt ++;
 			}	
		}
		aroundChars.setArray(charAround);
		return isAround;
	}
	
}
