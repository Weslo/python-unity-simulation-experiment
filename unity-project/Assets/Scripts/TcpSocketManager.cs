using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

// Manages a TCP socket connection.
public class TcpSocketManager : MonoBehaviour {

	// Connection flag.
	public bool Connected {
		get;
		set;
	}

	// Client connection.
	private TcpClient socket;

	// Network stream to the socket.
	private NetworkStream networkStream;

	// Network stream writer.
	private StreamWriter streamWriter;

	// Network stream reader.
	private StreamReader streamReader;

	// Self-initialize this component.
	void Awake() {
		Connected = false;
	}

	// Startup this component.
	void Start() {
		Connect("localhost", 50000);
	}

	// Destroy this component.
	void OnDestroy() {
		Disconnect();
	}

	// Connect to the specified address and port.
	public void Connect(string address, int port) {

		// Don't connect if there is already a connection.
		if(!AssertConnected(false, "Socket is already connected.")) {
			return;
		}

		// Connect to socket and cache streams.
		try {
			socket = new TcpClient(address, port);
			networkStream = socket.GetStream();
			streamWriter = new StreamWriter(networkStream);
			streamReader = new StreamReader(networkStream);
		}
		catch(Exception e) {
			Debug.LogErrorFormat("Error connecting to {0}:{1} : {2}", address, port, e.Message);
		}
	}

	// End the current connection.
	public void Disconnect() {

		// Check if we're already disconnected.
		if(!AssertConnected(true, "Socket is already disconnected.")) {
			return;
		}

		// End connection and close streams.
		streamWriter.Close();
		streamReader.Close();
		socket.Close();
		Connected = false;
	}

	// Write data to the socket.
	public void Write(string data) {

		// Cannot write without an active connection.
		if(!AssertConnected(true, "Cannot write to disconnected socket.")) {
			return;
		}

		// Write data to network stream.
		streamWriter.Write(data);
		streamWriter.Flush();
	}

	// Read data from the socket.
	public void Read() {

		// Cannot read without an active connection.
		if(!AssertConnected(true, "Cannot read from disconnected socket.")) {
			return;
		}

		// Ensure net stream has available data before reading.
		if(networkStream.DataAvailable) {
			string data = streamReader.ReadLine();
			Debug.LogFormat("Data from server: {0}", data);
		}
	}

	// Assert that the connection flag must be in the specified state, or print an error.
	private bool AssertConnected(bool connected, string error) {

		// Enforce connected state.
		if(connected != Connected) {

			// Print error.
			Debug.LogError(error);

			// Return failure.
			return false;
		}

		// Matching connection state.
		return true;
	}
}
