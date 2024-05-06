
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCPTestClient : MonoBehaviour {  	
	#region private members 	
	private TcpClient socketConnection; 	
	private Thread clientReceiveThread; 	
	#endregion

	private static TCPTestClient _instance;
	public static TCPTestClient Instance => TCPTestClient._instance;  	
	
	void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else if (_instance != null)
        {
            Destroy(gameObject);
        }
		ConnectToTcpServer();
		DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization 	
	void Start () {
	}

	// Update is called once per frame	
	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	private void ConnectToTcpServer () { 		
		try {  			
			clientReceiveThread = new Thread (new ThreadStart(ListenForData)); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start();  		
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e); 		
		} 	
	}

	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void ListenForData() { 		
		try { 			
			socketConnection = new TcpClient("localhost", 8000);  			
			Byte[] bytes = new Byte[1024];             
			while (true) { 				
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream()) { 					
					int length; 					
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) 
					{ 						
						var incomingData = new byte[length]; 						
						Array.Copy(bytes, 0, incomingData, 0, length);

						// TODO: ADD MROE DOCS
						string result = System.Text.Encoding.UTF8.GetString(incomingData);
						ABLResponse response = JsonUtility.FromJson<ABLResponse>(result);
						ActionManager.Instance.ParseData(response);
					} 				
				} 			
			}         
		}         
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}

	/// <summary> 	
	/// Send a stringified JSON object to server using socket connection. 	
	/// </summary> 	
	public void SendMessage<T>(T message) {       
		if (socketConnection == null) {       
			Debug.Log("Returning because socket connection is null");      
			return;         
		}  	
		try { 			
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream(); 			
			if (stream.CanWrite) {
				string json = JsonUtility.ToJson(message);                 
				// Convert string message to byte array.                 
				//	The reason for the newline is because of how Java's readLine
				//	works. It needs a newline to consider the stream complete.
				byte[] msgAsByteArray = Encoding.ASCII.GetBytes(json+'\n'); 				
				// Write byte array to socketConnection stream.                 
				stream.Write(msgAsByteArray, 0, msgAsByteArray.Length);                            
			}         
		} 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}

	public ABLMessage CreateABLMessage(BasePlayer ally) 
	{
		// Create the AllyWME object from the BasePlayer object.
		AllyWME allyWME = new AllyWME(ally);
		// Transform to json.
		string data = JsonUtility.ToJson(allyWME);
		Debug.Log(data);
		// Prepare the message for ABL.
		ABLMessage message = new ABLMessage();
		message.code = 1;
		message.msg = "AllyWME";
		message.data = data;
		// Return message object.
		return message;
	}

	public void RefreshWMEs()
	{
		Debug.Log("Refreshing WMEs");
		foreach (BasePlayer partyMember in PartyManager.Instance.currentParty)
		{
			ABLMessage msg = CreateABLMessage(partyMember);
			SendMessage<ABLMessage>(msg);
		}
	} 
}