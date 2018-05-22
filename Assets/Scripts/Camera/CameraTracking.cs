using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraTracking : MonoBehaviour {

	[SerializeField] Transform cameraTarget;

	float cameraDistance = 10f;
	float speedDistanceFactor = 0.6f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		LockOn();
		//ApproachTarget();
	}

	void LockOn(){
		transform.position = cameraTarget.position - Vector3.forward * cameraDistance;
	}

	void ApproachTarget(){
		Vector2 offset = (Vector2)cameraTarget.position - (Vector2)transform.position;
		float distanceToTarget = offset.magnitude;
		Vector2 movementThisFrame = offset * speedDistanceFactor * Time.deltaTime;
		transform.position = (Vector3)movementThisFrame + transform.position;
	}
}
