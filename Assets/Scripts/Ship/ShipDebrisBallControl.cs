using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This attaches to the ship
/// </summary>
public class ShipDebrisBallControl : MonoBehaviour {

	[SerializeField] DebrisBall debrisBallPrefab;
	[SerializeField] Transform debrisBallNearEdgeLimit;	//this is how close the edge of the ball is supposed to be to the ship
	[SerializeField] float distanceTolerance = 0.1f;

	[HideInInspector] public DebrisBall spawnedBall;
	SliderJoint2D controlJoint;	//will use a SliderJoint2D to keep the trash ball in front of the ship
	float ballNearEdgeDistance;

	// Use this for initialization
	void Start () {
		ballNearEdgeDistance = Vector2.Distance(transform.position, debrisBallNearEdgeLimit.position);

		//note: this will need to spawn for the server as well
		spawnedBall = Instantiate(debrisBallPrefab);
		spawnedBall.transform.position = debrisBallNearEdgeLimit.position;
		spawnedBall.controller = this;

		SetupJoint();	//the joint will need to be reflected on the server too
	}

	void SetupJoint(){
		controlJoint = gameObject.AddComponent<SliderJoint2D>();
		controlJoint.anchor = Vector2.zero;
		controlJoint.autoConfigureAngle = false;
		controlJoint.angle = 0;
		controlJoint.autoConfigureConnectedAnchor = false;
		controlJoint.useLimits = true;
		controlJoint.connectedBody = spawnedBall.body;
		controlJoint.connectedAnchor = Vector2.zero;
		UpdateBallJointParameters();
	}

	/// <summary>
	/// Called by the ball. This awkward flow is added to reduce how often the ball's position needs to be updated
	/// </summary>
	public void BallSizeChanged(){
		UpdateBallJointParameters();
	}

	/// <summary>
	/// Changes the limits for the joint's max and min distance to resposition the ball
	/// 
	/// Will need to update the server
	/// </summary>
	void UpdateBallJointParameters(){
		controlJoint.limits = MinMaxBallDistance();
	}
		
	/// <summary>
	/// Returns a translationlimits object that tries to keep the ball from ever being closer than debrisBallNearEdgeLimit distance
	/// </summary>
	/// <returns>The max ball distance.</returns>
	JointTranslationLimits2D MinMaxBallDistance(){
		JointTranslationLimits2D output = new JointTranslationLimits2D();
		output.min = ballNearEdgeDistance + spawnedBall.DebrisRadius;
		output.max = output.min + distanceTolerance;
		return output;
	}

}
