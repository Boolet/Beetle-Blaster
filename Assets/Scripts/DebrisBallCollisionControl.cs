using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DebrisBall))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class DebrisBallCollisionControl : MonoBehaviour {

	[SerializeField] DebrisBall ball;
	[SerializeField] float velocityThreshold = 2f;

	void OnCollisionEnter2D(Collision2D col){
		if (col.relativeVelocity.magnitude > velocityThreshold){
			return;	//will need to handle breaking off debris here
		}

		Debris otherDebris = col.gameObject.GetComponent<Debris>();
		if (otherDebris != null){
			
			//may want to negate or mitigate velocity gained from absorbed garbage
			//or maybe not!

			ball.CollapseDebrisToBall(otherDebris);
		}
	}


}
