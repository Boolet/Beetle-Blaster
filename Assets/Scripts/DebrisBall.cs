using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is attached to the debris ball in front of the player's ship
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class DebrisBall : MonoBehaviour {

	[SerializeField] Debris debrisPrefab;
	[SerializeField] float volumePerDebris = 1.5f;
	[SerializeField] float massPerDebris = 0.5f;

	Rigidbody2D body;
	CircleCollider2D attachedCollider;

	int m_debrisCount = 0;
	public int DebrisCount{
		get{ return m_debrisCount; }
		set{
			m_debrisCount = value;
			UpdateBallSize();
			UpdateBallMass();
		}
	}
	float m_debrisRadius = 0f;
	public float DebrisRadius{
		get{ return m_debrisRadius; }
	}


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		attachedCollider = GetComponent<CircleCollider2D>();
	}

	/// <summary>
	/// Adds another piece of debris to the ball
	/// </summary>
	/// <param name="debris">Debris.</param>
	public void CollapseDebrisToBall(Debris debris){
		Destroy(debris.gameObject);
		++DebrisCount;
	}

	/// <summary>
	/// Reduces the size of the debris ball by the specified number (or the number in the debris ball, whichever is less)
	/// and spawns that many pieces of debris randomly within the area of the debris ball. Each of those pieces of debris
	/// will be set to not collide with the debris ball while within the debris ball area.
	/// 
	/// The ship may need to take ownership of them, and will have to fire them off. They will also have the same velocity as the ball.
	/// </summary>
	/// <returns>The debris from ball.</returns>
	/// <param name="count">Count.</param>
	public Debris[] SpawnDebrisFromBall(int count){
		count = Mathf.Min(count, DebrisCount);
		Debris[] output = new Debris[count];

		for (int i = 0; i < count; ++i){
			output[i] = SpawnDebris();
		}

		return output;
	}

	/// <summary>
	/// Spawns one piece of debris in a random point within the debris ball.
	/// 
	/// Will need to update the server.
	/// </summary>
	/// <returns>The debris.</returns>
	Debris SpawnDebris(){
		Debris newDebris = Instantiate(debrisPrefab);
		newDebris.rigidBody2D.velocity = body.velocity;
		newDebris.rigidBody2D.angularVelocity = body.angularVelocity;
		newDebris.transform.position = RandomPointInBall();
		//update server here
		return newDebris;
	}

	//returns a random point within the ball
	Vector2 RandomPointInBall(){
		Vector2 point = Random.insideUnitCircle;
		point *= m_debrisRadius;
		return point;
	}

	/// <summary>
	/// Changes the size of the ball to reflect the amount of debris in it, taking into consideration the volumePerDebris
	/// and the area of a circle.
	/// 
	/// Will need to update the server.
	/// </summary>
	void UpdateBallSize(){
		m_debrisRadius = Mathf.Sqrt(volumePerDebris * DebrisCount / Mathf.PI);
		transform.localScale = Vector3.one * m_debrisRadius;
		//update the server here
	}

	/// <summary>
	/// Just changes the mass to refelect the amount of debris in it, using massPerDebris.
	/// </summary>
	void UpdateBallMass(){
		body.mass = DebrisCount * massPerDebris;
	}
}
