using System;

namespace UnityTcpServer {
	class MainClass {
		public static void Main(string[] args) {
			Console.WriteLine("Hello World!");
			TcpServer server = new TcpServer();
			server.Start();
		}
	}
}
