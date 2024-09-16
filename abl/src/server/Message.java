package server;

import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import wm.WME;
import abl.wmes.AllyWME;
import abl.wmes.VivWME;
import handler.*;

public class Message {
	public int code;
	public String msg;
	public JSONObject data;

	public Message(int code, String msg, JSONObject data) {
		this.code = code;
		this.msg = msg;
		this.data = data;
	}
	
	public Message(JSONObject jo) {
		this.code = (int)(long) jo.get("code");
		this.msg = (String) jo.get("msg");
		JSONParser parser = new JSONParser();
		try {
			System.out.println("Trying to parse data.");
			this.data = (JSONObject) parser.parse((String) jo.get("data"));
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			System.out.println("Error parsing data field.");
		}
	}
	
	public JSONObject toJSON() {
		JSONObject jo = new JSONObject();
		jo.put("code", code);
		jo.put("msg", msg);
		jo.put("data", data);
		return jo;
	}
	
	WME parseData() {
		// 1 -- AllyWME
		if (this.code == 1) {
			AllyWME wme = new AllyWME(this.data);
			return wme;
		}

		// 2 -- VivWME
		if (this.code == 2) {
			VivWME wme = new VivWME(this.data);
			return wme;
		}
		return null;
	}

	void parseDelpMessage(DelpHandler handler) {
		if (this.code == 4) {
			String query = (String) this.data.get("query");
			JSONObject data = new JSONObject();
			data.put("answer", handler.query(query));
			Message toSend = new Message(4, query, data);
			JSONObject jo = toSend.toJSON();
			TCPServer.getInstance().sendOutgoingMessage(jo);
		} else {
			String belief = (String) this.data.get("belief");
			handler.addBelief(belief);
			System.out.println("KB updated: " + belief);
		}
	}
}
