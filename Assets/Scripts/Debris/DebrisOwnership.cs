using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This goes on a piece of debris
/// </summary>
public class DebrisOwnership : MonoBehaviour {

    //PlayerDebrisOwner lastOwner = null;
    string lastOwner = null;
    public GameObject scoreboard;
    public float TurnInSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        //Testing purposes only, remove this line later
        SetDebrisOwner("Player1");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetDebrisOwner(string newOwner){
		lastOwner = newOwner;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            if(lastOwner != null)
            {
                if(this.GetComponent<Rigidbody2D>().velocity.magnitude < TurnInSpeed)
                {
                    scoreboard = GameObject.Find("Canvas/ScoreBoard");
                    scoreboard.GetComponent<ScoreBoard>().updateScore(lastOwner, 1);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
