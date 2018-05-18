using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsHelper {

	public static Vector2 GreatestImpactNormal(Collision2D col, int maxContactPoints){
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

		return contactNormal * greatestCollisionForce;
	}
}
