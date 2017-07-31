using System;
using System.IO;
using System.Net.Sockets;

namespace UnityTcpServer {

	// Wrapper for a TCP client.
	public class Client {

		// Reference to socket.
		private TcpClient socket;

		// Reference to socket network stream.
		private NetworkStream networkStream;

		// Network stream writer.
		private StreamWriter streamWriter;

		// Network stream reader.
		private StreamReader streamReader;

		// Default constructor.
		public Client(TcpClient socket) {
			this.socket = socket;
			networkStream = socket.GetStream();
			streamWriter = new StreamWriter(networkStream);
			streamReader = new StreamReader(networkStream);
		}

		// Return the address of this client.
		public string GetAddress() {
			return socket.Client.RemoteEndPoint.ToString();
		}

		// Check if this client has written any packets to the server.
		public void Tick(float dt) {

			// Check pings.
			if(networkStream.DataAvailable) {
				string data = streamReader.ReadLine().Trim();
				Console.WriteLine("Data from client {0}: {1}", GetAddress(), data);
				if(data.Equals("ping")) {
					streamWriter.WriteLine("pong");
					streamWriter.Flush();
				}
			}
		}
	}
}
