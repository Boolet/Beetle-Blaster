using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRelativeJoint : MonoBehaviour {

	RelativeJoint2D joint;
	[SerializeField] float growthRate = 1f;

	// Use this for initialization
	void Start () {
		joint = GetComponent<RelativeJoint2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.D))
			GrowJoint(growthRate * Time.fixedDeltaTime);
		if (Input.GetKey(KeyCode.A))
			GrowJoint(-growthRate * Time.fixedDeltaTime);
	}

	void GrowJoint(float amount){
		joint.linearOffset -= Vector2.right * amount;
	}
}
