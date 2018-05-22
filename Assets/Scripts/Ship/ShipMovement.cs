using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    public ShipThrusters shipThrusters;
    // Use this for initialization
    Rigidbody2D body;
    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(Input.GetKey(KeyCode.W))
        {
            shipThrusters.ThrustInDirection(Vector2.up);
        }
        if (Input.GetKey(KeyCode.A))
        {
            shipThrusters.ThrustInDirection(Vector2.left);
        }
        if (Input.GetKey(KeyCode.S))
        {
            shipThrusters.ThrustInDirection(Vector2.down);
        }
        if (Input.GetKey(KeyCode.D))
        {
            shipThrusters.ThrustInDirection(Vector2.right);
        }
    }
}
