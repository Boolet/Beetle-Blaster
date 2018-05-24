using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

/// <summary>
/// This attaches to the ship
/// </summary>
public class ShipDebrisBallControl : NetworkBehaviour {

	[SerializeField] DebrisBall debrisBallPrefab;
	[SerializeField] Transform debrisBallNearEdgeLimit;	//this is how close the edge of the ball is supposed to be to the ship
	[SerializeField] ShipGatherArea gatherArea;
	//[SerializeField] float distanceTolerance = 0.1f;
	[SerializeField] float distanceAdjustRate = 0.3f;

	[HideInInspector] public DebrisBall spawnedBall;
	HingeJoint2D controlJoint;
	float ballNearEdgeDistance;
	float targetDistance;
	float currentDistance;

	// Use this for initialization
	void Start () {
		ballNearEdgeDistance = Vector2.Distance(transform.position, debrisBallNearEdgeLimit.position);
		currentDistance = targetDistance = ballNearEdgeDistance;

		//note: this will need to spawn for the server as well
		spawnedBall = Instantiate(debrisBallPrefab);
		spawnedBall.transform.position = debrisBallNearEdgeLimit.position;
		spawnedBall.controller = this;

		NetworkServer.Spawn(spawnedBall.gameObject);


		SetupJoint();	//the joint will need to be reflected on the server too
	}

	void FixedUpdate(){
		currentDistance = Mathf.MoveTowards(currentDistance, targetDistance, Time.fixedDeltaTime * distanceAdjustRate);
		UpdateBallJointParameters();
	}

	void SetupJoint(){
		controlJoint = gameObject.AddComponent<HingeJoint2D>();
		controlJoint.autoConfigureConnectedAnchor = false;
		controlJoint.connectedBody = spawnedBall.body;
		controlJoint.anchor = Vector2.right * currentDistance;
		controlJoint.connectedAnchor = Vector2.zero;
	}

	/// <summary>
	/// Called by the ball. This awkward flow is added to reduce how often the ball's position needs to be updated
	/// </summary>
	public void BallSizeChanged(){
		targetDistance = ballNearEdgeDistance + spawnedBall.DebrisRadius;
		gatherArea.enabled = spawnedBall.DebrisCount == 0;
	}

	/// <summary>
	/// Changes the limits for the joint's max and min distance to resposition the ball
	/// 
	/// Will need to update the server
	/// </summary>
	void UpdateBallJointParameters(){
		controlJoint.anchor = new Vector2(currentDistance, 0);
	}

}
