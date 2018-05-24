using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
public class ShipShootDebrisControl : NetworkBehaviour {

	[SerializeField] ShipDebrisBallControl ballController;
	[SerializeField] float chargeRate = 2f;
	[SerializeField] float launchPower = 0.2f;
	[SerializeField][Range(0f,1f)] float powerVariancePercent = 0.10f;
	[SerializeField][Range(0f,90f)] float spread = 5f;

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
		chargeLevel -= projectileCount;

		CmdFire(projectileCount);

	}

	[Command]
	void CmdFire(int projectileCount){
		Debris[] firedProjectiles = ballController.spawnedBall.SpawnDebrisFromBall(projectileCount);
		foreach (Debris d in firedProjectiles){
			float thisSpread = Random.Range(-spread, spread);
			float thisPower = launchPower * Random.Range(-powerVariancePercent, powerVariancePercent) + launchPower;
			Vector2 forceVector = transform.right * thisPower;
			forceVector = Quaternion.AngleAxis(thisSpread, transform.forward) * forceVector;

			d.rigidBody2D.AddForce(forceVector, ForceMode2D.Impulse);
		}
	}
}
