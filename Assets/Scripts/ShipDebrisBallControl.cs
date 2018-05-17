using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This attaches to the ship
/// </summary>
public class ShipDebrisBallControl : MonoBehaviour {

	[SerializeField] DebrisBall debrisBallPrefab;
	[SerializeField] Transform debrisBallInitialPoint;

	DebrisBall spawnedBall;

	// Use this for initialization
	void Start () {
		//note: this will need to spawn for the server as well
		spawnedBall = Instantiate(debrisBallPrefab);
		spawnedBall.transform.position = debrisBallInitialPoint.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	Vector2 BallPosition(){

	}
	*/
}
