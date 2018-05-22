using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This goes on a piece of debris
/// </summary>
public class DebrisOwnership : MonoBehaviour {

    //PlayerDebrisOwner lastOwner = null;
<<<<<<< HEAD
    GameObject lastOwner = null;
    public float TurnInSpeed = 1.0f;

    // Use this for initialization
    void Start () {
=======
    string lastOwner = null;
    public GameObject scoreboard;
    public float TurnInSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        //Testing purposes only, remove this line later
        SetDebrisOwner("Player1");
>>>>>>> c10830ee2be85343df725c100b14284f8afc9c20
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

<<<<<<< HEAD
	//public void SetDebrisOwner(PlayerDebrisOwner newOwner){
	//	lastOwner = newOwner;
	//}

    public void SetDebrisOwner(GameObject newOwner)
    {
        lastOwner = newOwner;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Collision with Turn in Portal
        if (collision.gameObject.tag == "Goal")
        {
            SetDebrisOwner(this.gameObject);
            if(lastOwner != null)
            {
                if (this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > TurnInSpeed)
                {
=======
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
>>>>>>> c10830ee2be85343df725c100b14284f8afc9c20
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
