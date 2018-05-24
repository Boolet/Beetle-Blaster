using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// This is attached to the debris ball in front of the player's ship
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class DebrisBall : NetworkBehaviour {

	[SerializeField] Debris debrisPrefab;
	[SerializeField] float volumePerDebris = 1.5f;
	[SerializeField] float massPerDebris = 0.5f;
	[SerializeField] float baseScaleFactor = 0.2f;
	[SerializeField] float spawnedDebrisNoCollideTime = 0.5f;

	public Rigidbody2D body;
	public CircleCollider2D attachedCollider;
	[HideInInspector] public ShipDebrisBallControl controller;

	Dictionary<Collider2D, float> noCollideObjects = new Dictionary<Collider2D, float>();
    //int debrisToAdd = 0;

    string lastOwner = null;
    public GameObject scoreboard;

	int m_debrisCount = 0;
	public int DebrisCount{
		get{ return m_debrisCount; }
		set{
			//if (value == m_debrisCount)
				//return;
			m_debrisCount = value;
			RpcUpdateBallSize();
			RpcUpdateBallMass();
			RpcSendBallUpdatedNotification();
		}
	}
	float m_debrisRadius = 0f;
	public float DebrisRadius{
		get{
			float realCollisionRadius = attachedCollider.radius * transform.localScale.x;
			return realCollisionRadius;
		}
	}

	void Start(){
		DebrisCount = 0;
	}

	/// <summary>
	/// Only using update to count down all of the nocollide timers
	/// </summary>
	void Update(){
		TickDownNoCollide();
	}

	/*
	void FixedUpdate(){
		AddLeftoverDebris();
	}
	*/

	/// <summary>
	/// counts down the timers associated with each of the objects that are not supposed to collide with this object
	/// and, when any of them reach zero, enables collision and removes them from the dictionary
	/// </summary>
	void TickDownNoCollide(){

		List<Collider2D> keyList = new List<Collider2D>();
		foreach(Collider2D key in noCollideObjects.Keys){
			keyList.Add(key);
		}

		foreach (Collider2D key in keyList){
			noCollideObjects[key] -= Time.deltaTime;
			if (noCollideObjects[key] <= 0){
				Physics2D.IgnoreCollision(key, attachedCollider, false);
				noCollideObjects.Remove(key);
			}
		}

	}

	/*
	//adds unadded debris to the ball
	void AddLeftoverDebris(){
		DebrisCount += debrisToAdd;
		debrisToAdd = 0;
	}
	*/

	/// <summary>
	/// Adds another piece of debris to the ball
	/// </summary>
	/// <param name="debris">Debris.</param>
	[Server]
	public void CollapseDebrisToBall(Debris debris){
		NetworkServer.UnSpawn(debris.gameObject);
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
		if (count == 0)
			return output;
		DebrisCount -= count;

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
	[Server]
	Debris SpawnDebris(){
		Debris newDebris = Instantiate(debrisPrefab);
		newDebris.rigidBody2D.velocity = body.velocity;
		newDebris.rigidBody2D.angularVelocity = body.angularVelocity;
		newDebris.transform.position = RandomPointInBall();

		Physics2D.IgnoreCollision(attachedCollider, newDebris.attachedCollider);
		noCollideObjects.Add(newDebris.attachedCollider, spawnedDebrisNoCollideTime);
		NetworkServer.Spawn(newDebris.gameObject);

		return newDebris;
	}

	//returns a random point within the ball
	Vector2 RandomPointInBall(){
		Vector2 point = Random.insideUnitCircle;
		point *= DebrisRadius;
		point += (Vector2)transform.position;
		return point;
	}

	/// <summary>
	/// Changes the size of the ball to reflect the amount of debris in it, taking into consideration the volumePerDebris
	/// and the area of a circle.
	/// 
	/// Will need to update the server.
	/// </summary>
	//[ClientRpc]
	void RpcUpdateBallSize(){
		m_debrisRadius = Mathf.Sqrt(volumePerDebris * DebrisCount / Mathf.PI) * baseScaleFactor;
		transform.localScale = Vector3.one * m_debrisRadius;
		//update the server here
	}

	/// <summary>
	/// Just changes the mass to refelect the amount of debris in it, using massPerDebris.
	/// </summary>
	//[ClientRpc]
	void RpcUpdateBallMass(){
		body.mass = DebrisCount * massPerDebris;
	}

	//inform the controller that the ball's size has changed
	//[ClientRpc]
	void RpcSendBallUpdatedNotification(){
		if(controller != null)
			controller.BallSizeChanged();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            //test line only
            lastOwner = "Player1";
            if (lastOwner != null)
            {

                scoreboard = GameObject.Find("Canvas/ScoreBoard");
                scoreboard.GetComponent<ScoreBoard>().updateScore(lastOwner, DebrisCount);
                DebrisCount = 0;
                //Destroy(this.gameObject);
            }
        }
    }
}
