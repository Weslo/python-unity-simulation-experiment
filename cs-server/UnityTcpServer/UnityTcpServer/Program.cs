using System;

namespace UnityTcpServer {
	class MainClass {
		public static void Main(string[] args) {
			Console.WriteLine("Hello World!");
			Server server = new Server();
			server.Start();
		}
	}
}
