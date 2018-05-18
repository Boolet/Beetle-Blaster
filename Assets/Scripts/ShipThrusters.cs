using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipThrusters : MonoBehaviour {

	[SerializeField] float power = 1f;

	Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// Call from a FixedUpdate control, passing in the input direction. The input will be normalized.
	/// 
	/// Will need to update the server
	/// </summary>
	/// <param name="direction">Direction.</param>
	public void ThrustInDirection(Vector2 direction){
		body.AddForce(direction.normalized * Time.fixedDeltaTime * power);
	}
}
