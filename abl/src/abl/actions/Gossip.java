package abl.actions;

import org.json.simple.JSONObject;
import org.json.simple.JSONArray;

import server.Message;
import server.TCPServer;
import abl.util.*;
import java.lang.reflect.*;

// An acting character speaks a string of context-specific dialogue.
// Args [0] -- int actingCharacter (id field in wme)
//		[1] -- bool inCombat
public class Gossip extends BaseAction {

	@Override
	public void execute(Object[] args) {
		System.out.println("Gossip initiator: " + args[0]);
		// Codes: 1 -- combat action
		//		  2 -- noncombat action
		//        3 -- dialogue
		int code = 3;
		// Msg field must match name of java class in String format
		String msg = "Gossip";
		JSONObject data = new JSONObject();
		ArrayHolder ah = (ArrayHolder)args[1];
		JSONArray jsonArray = new JSONArray();
		for (int value : ah.getArray()) {
            jsonArray.add(value);
        }
		data.put("initiatingCharacter", args[0]);
		data.put("receivingCharacter", jsonArray);
		
		Message toSend = new Message(code, msg, data);
		JSONObject jo = toSend.toJSON();
		TCPServer.getInstance().sendOutgoingMessage(jo);
	}
}
