using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DebugChuckDebris : MonoBehaviour {

	[SerializeField] float impulse = 1f;

	Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
			body.AddForce(Vector2.right * impulse, ForceMode2D.Impulse);
	}
}
