using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ShipDamage : MonoBehaviour {

	[SerializeField] float forceDamageThreshold = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		float impactSpeed = col.relativeVelocity.magnitude;
		float impactMass = col.rigidbody.mass;

		if (impactMass * impactSpeed >= forceDamageThreshold){
			TakeDamage();
		}
	}

	void TakeDamage(){
		//effects of damage
	}
}
