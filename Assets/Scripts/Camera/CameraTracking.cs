using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraTracking : MonoBehaviour {

	[SerializeField] Transform cameraTarget;

	float cameraDistance = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		LockOn();
	}

	void LockOn(){
		transform.position = cameraTarget.position - Vector3.forward * cameraDistance;
	}
}
