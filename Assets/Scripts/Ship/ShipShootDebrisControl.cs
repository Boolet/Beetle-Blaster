using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the player to shoot off debris from their ball as projectiles.
/// 
/// Do we want the player to have to charge up their shots? Or just shoot?
/// And at what fire rate? Does charging increase the projectile speed or
/// the number of projectiles? WHO KNOWS!?
/// 
/// And is there recoil?
/// </summary>
[RequireComponent(typeof(ShipDebrisBallControl))]
public class ShipShootDebrisControl : MonoBehaviour {

	[SerializeField] ShipDebrisBallControl ballController;
	[SerializeField] float chargeRate = 2f;
	[SerializeField] float launchPower = 0.5f;
	[SerializeField] float powerVariance = 0.2f;
	[SerializeField] float spread = 0.05f;

	bool charging = false;
	float chargeLevel = 0f;

	// Update is called once per frame
	void Update () {
		chargeLevel += Time.deltaTime * chargeRate;
		if (!charging)
			chargeLevel = Mathf.Clamp(chargeLevel, 0f, 1f);
	}

	public void StartCharge(){
		charging = true;
	}

	public void Fire(){
		charging = false;
		int projectileCount = Mathf.FloorToInt(chargeLevel);

		Debris[] firedProjectiles = ballController.spawnedBall.SpawnDebrisFromBall(projectileCount);
		foreach (Debris d in firedProjectiles){
			float thisSpread = Random.Range(-spread, spread);
			float thisPower = Random.Range(-powerVariance, powerVariance) + launchPower;
			Vector2 forceVector = transform.right * launchPower + transform.up * thisSpread;
			forceVector = forceVector.normalized * thisPower;

			d.rigidBody2D.AddForce(forceVector, ForceMode2D.Impulse);
		}

		chargeLevel -= projectileCount;

	}
}
