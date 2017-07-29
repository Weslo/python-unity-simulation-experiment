using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerGameManager : NetworkManager {

	public override void OnStartServer() {
		Debug.LogWarning("Server started.");
	}
}
