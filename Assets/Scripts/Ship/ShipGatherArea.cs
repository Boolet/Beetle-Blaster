using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGatherArea : MonoBehaviour {

	[SerializeField] ShipDebrisBallControl shipBallController;

	void OnTriggerEnter2D(Collider2D col){
		Debris debris = col.gameObject.GetComponent<Debris>();
		if (debris != null){
			shipBallController.spawnedBall.CollapseDebrisToBall(debris);
		}
	}
}
