using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DebugRotate : MonoBehaviour {

	[SerializeField] float degreesPerSecond = 30f;

	Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		if (Input.GetKey(KeyCode.W)){
			body.AddTorque(degreesPerSecond * Time.fixedDeltaTime);
		}
	}
}
