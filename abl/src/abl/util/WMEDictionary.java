package abl.util;

import java.util.*;
import abl.wmes.AllyWME;

public class WMEDictionary {
	private HashMap<Integer, AllyWME> map;
	
	public WMEDictionary() {
		this.setMap(new HashMap<Integer, AllyWME>());
	}

	public HashMap<Integer, AllyWME> getMap() {
		return map;
	}

	public void setMap(HashMap<Integer, AllyWME> map) {
		this.map = map;
	}
	
	public void addCharacter(int key, AllyWME value) {
		this.map.putIfAbsent(key, value);
		this.map.replace(key, value);
	}
	
	public void deleteCharacter(int key) {
		this.map.remove(key);
	}
	
	public AllyWME getCharacter(int key) {
		return this.map.get(key);
	}
	
	public Integer[] getCharacters() {
		Integer arr[] = new Integer[this.map.size()];
		this.map.keySet().toArray(arr);
		return arr;
	}
	
	public boolean containsKey(int key) {
		return this.map.containsKey(key);
	}
	
	public boolean isEmpty() {
		return this.map.isEmpty();
	}
}
