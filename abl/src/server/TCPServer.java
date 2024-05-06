package server;

import java.io.*;
import java.net.*;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

import abl.generated.GameAgent;
import abl.wmes.AllyWME;
import handler.*;
/**
 * This program demonstrates a simple TCP/IP socket server.
 *
 * @author www.codejava.net
 * ADD more docs if need be?
 */


public class TCPServer {

	private static TCPServer server;
	
	public static TCPServer getInstance() {
		return server;
	}
	
	private static Socket socket;
	
	public static GameAgent agent;

	public static DelpHandler delpHandler;
	
	public void startAgent() {
		agent = new GameAgent();
		agent.startBehaving();
	}

	public void startHandler() {
		delpHandler = new DelpHandler();
		delpHandler.init();
	}
	
	void startServer() {
    	new Thread() {
    		public void run() {
    			while (true) {
    				try {
    					startAgent();
    					Thread.sleep(50);
    				}
    				catch (Exception e) {}
    			}
    		}
    	}.start();
		
		int port = 8000;
        try (ServerSocket serverSocket = new ServerSocket(port)) {

            System.out.println("Server is listening on port " + port);
            while (true) {
                socket = serverSocket.accept();
 
                System.out.println("New client connected");
 
                InputStream input = socket.getInputStream();
                BufferedReader in = new BufferedReader(new InputStreamReader(input));
                String line = "";
                while ((line = in.readLine()) != null) {
                	System.out.println("Receiving message...");
                	JSONObject obj = (JSONObject) JSONValue.parse(line);
                	handleIncomingMessage(obj);
                }
 
            }
 
        } catch (IOException ex) {
            System.out.println("Server exception: " + ex.getMessage());
            ex.printStackTrace();
        }
	}
	
    public static void main(String[] args) {
    	server = new TCPServer();
		server.startHandler();
    	server.startServer();
    }
    
    private void handleIncomingMessage(JSONObject jo) {
    	Message toHandle = new Message(jo);

		if (toHandle.msg.equals("delp"))
		{
			toHandle.parseDelpMessage(delpHandler);
			System.out.println("Parsing delp message...");
			return;
		}
		
		if (toHandle.code == 1) {
    		AllyWME wme = (AllyWME) toHandle.parseData();
    		agent.addWME(wme);
    	}
    }
    
    public void sendOutgoingMessage(JSONObject jo) {
    	System.out.println("Sending outgoing message...");
    	OutputStream output = null;
		try {
			output = socket.getOutputStream();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			System.out.println("Error sending message: output stream");
		}
        PrintWriter writer = new PrintWriter(output, true);
        writer.println(jo);
    }
}