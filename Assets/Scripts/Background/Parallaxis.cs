using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parallaxis : MonoBehaviour {

	float maxDepth = 30f;
	Vector2 appearanceOffset;
	Camera mainCam;

	// Use this for initialization
	void Start () {
		mainCam = Camera.main;
		appearanceOffset = transform.position;
	}
		
	void LateUpdate () {
		AdjustPosition();
	}

	float MovementRateAtDepth(float depth){
		depth = Mathf.Clamp(depth, 0f, maxDepth);
		return depth/maxDepth;
	}

	void AdjustPosition(){
		Vector2 cameraOffset = mainCam.transform.position;
		cameraOffset = cameraOffset * MovementRateAtDepth(transform.position.z);
		transform.position = new Vector3(appearanceOffset.x + cameraOffset.x, appearanceOffset.y + cameraOffset.y, transform.position.z);
	}
}
