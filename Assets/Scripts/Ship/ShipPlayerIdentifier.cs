using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipPlayerIdentifier : NetworkBehaviour {

	public static ShipPlayerIdentifier playerShip = null;

	void Start(){
		if(isLocalPlayer)
			playerShip = this;
	}
}
