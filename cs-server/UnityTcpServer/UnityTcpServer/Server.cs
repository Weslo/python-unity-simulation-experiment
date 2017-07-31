using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace UnityTcpServer {

	// TCP socket server.
	public class Server {

		private const float TICK_FREQUENCY = 1f;

		// Server that listens to clients via sockets.
		private TcpListener listener;

		// List of connected clients.
		private List<Client> clients = new List<Client> ();

		// Timestamp as of the previous tick.
		private int timestamp;

		// Time since last tick.
		private float timeSinceLastTick;

		// Default constructor.
		public Server() {
			timestamp = Environment.TickCount;
		}

		// Start the server.
		public void Start() {

			// Start up the server.
			listener = new TcpListener(IPAddress.Any, 50000);
			listener.Start();

			// Start listening loop.
			bool exit = false;
			while(!exit) {

				// Get delta time.
				int now = Environment.TickCount;
				int deltaTicks = now - timestamp;
				float dt = deltaTicks / 1000f;
				timeSinceLastTick += dt;

				// Update engine.
				if(timeSinceLastTick >= TICK_FREQUENCY) {
					Tick(timeSinceLastTick);
					timeSinceLastTick -= TICK_FREQUENCY;
				}

				// Update timestamp.
				timestamp = now;
			}
		}

		// Update each frame.
		private void Tick(float dt) {

			// Accept a client connection.
			while(listener.Pending()) {
				Client client = new Client(listener.AcceptTcpClient());
				clients.Add(client);
				Console.WriteLine("New client connected: {0}", client.GetAddress());
			}

			// Update clients.
			foreach(Client client in clients) {
				client.Tick(dt);
			}
		}
	}
}
