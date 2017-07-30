using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace UnityTcpServer {

	// TCP socket server.
	public class TcpServer {

		// Server that listens to clients via sockets.
		private TcpListener server;

		// Default constructor.
		public TcpServer() {
		}

		// Start the server.
		public void Start() {

			// Start up the server.
			server = new TcpListener(IPAddress.Any, 50000);
			server.Start();

			// Start listening loop.
			bool exit = false;
			while(!exit) {
				Console.WriteLine("Waiting for a connection.");

				// Accept a client connection.
				TcpClient client = server.AcceptTcpClient();
				Console.WriteLine("Connected!");

				// Get streams for connected client.
				NetworkStream networkStream = client.GetStream();
				StreamWriter streamWriter = new StreamWriter(networkStream);
				StreamReader streamReader = new StreamReader(networkStream);

				while(client.Connected) {

					// Read data from client.
					if(networkStream.DataAvailable) {
						string data = streamReader.ReadLine();
						Console.WriteLine("Data from client: {0}", data);

						if(data.Equals("ping")) {
							streamWriter.WriteLine("pong");
							streamWriter.Flush();
						}
					}
				}
			}
		}
	}
}
