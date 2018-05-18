using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ShipDamage : MonoBehaviour {

	[SerializeField] float forceDamageThreshold = 2f;
	[SerializeField] int maxContactPoints = 4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		if (PhysicsHelper.GreatestImpactNormal(col, maxContactPoints).magnitude >= forceDamageThreshold){
			TakeDamage();
		}
	}

	void TakeDamage(){
		//effects of damage
	}
}
