using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This goes on a piece of debris
/// </summary>
public class DebrisOwnership : MonoBehaviour {

	PlayerDebrisOwner lastOwner = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetDebrisOwner(PlayerDebrisOwner newOwner){
		lastOwner = newOwner;
	}
}
