using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This goes on a piece of debris
/// </summary>
public class DebrisOwnership : MonoBehaviour {

    //PlayerDebrisOwner lastOwner = null;
    GameObject lastOwner = null;
    public float TurnInSpeed = 1.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
