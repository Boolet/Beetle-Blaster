using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DebrisBall))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class DebrisBallCollisionControl : MonoBehaviour {

	[SerializeField] DebrisBall ball;
	[SerializeField] float forceThreshold = 0.5f;
	[SerializeField] float forcePerDebris = 0.7f;
	[SerializeField] float knockOffImpulseFactor = 0.2f;
	[SerializeField] int maxContactPoints = 4;

	void OnCollisionEnter2D(Collision2D col){
		//this doesn't handle the cases of the other body being kinematic, does it?
		float greatestCollisionForce = 0f;
		Vector2 contactNormal = Vector2.zero;

		ContactPoint2D[] contacts = new ContactPoint2D[maxContactPoints];
		col.GetContacts(contacts);
		foreach (ContactPoint2D cp in contacts){
			if (cp.normalImpulse > greatestCollisionForce){
				greatestCollisionForce = cp.normalImpulse;
				contactNormal = cp.normal;
			}
		}

		if (greatestCollisionForce > forceThreshold){
			BreakOffBall(greatestCollisionForce, contactNormal);
			return;
		} else{
			AddToBall(col.gameObject);
		}
	}

	/// <summary>
	/// causes debris to break off the ball when the ball is hit hard enough.
	/// 
	/// It should really produce a random amount of debris instead, taking into account the impact force
	/// </summary>
	/// <param name="impactMagnitude">Impact magnitude.</param>
	/// <param name="collisionNormal">Collision normal.</param>
	void BreakOffBall(float impactMagnitude, Vector2 collisionNormal){
		int damage = Mathf.FloorToInt(impactMagnitude / forcePerDebris);
		Debris[] spawnedDebris = ball.SpawnDebrisFromBall(damage);
		foreach (Debris d in spawnedDebris){
			d.rigidBody2D.AddForce(-collisionNormal * knockOffImpulseFactor, ForceMode2D.Impulse);
		}
	}

	void AddToBall(GameObject obj){
		Debris otherDebris = obj.GetComponent<Debris>();
		if (otherDebris != null){

			//may want to negate or mitigate velocity gained from absorbed garbage
			//or maybe not!

			ball.CollapseDebrisToBall(otherDebris);
		}
	}

}
