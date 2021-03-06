﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipRotation : NetworkBehaviour {

	float torqueDamping = 0.001f;
    Rigidbody2D body;
    Vector2 playerDir;
    Vector2 mouseDir;
    bool isTurning;
    // Use this for initialization
    float _angle;
    Vector3 mousePosition;

    void Start ()
    {
		if (!isLocalPlayer)
			return;
        body = GetComponent<Rigidbody2D>();
        GetDirections();
        isTurning = false;
    }
	
	void FixedUpdate ()
    {
		if (!isLocalPlayer)
			return;
		//print(mouseDir);

		RotationSystem();

		/*
        if(!isTurning && !CheckSameDirection())
        {
            AddTorque();
            isTurning = true;
        }
        //======================================================================
        //_angle = Vector2.SignedAngle(playerDir, mouseDir);
        //Debug.Log(_angle);
        //Debug.Log("Mouse:" + mouseDir.normalized + "   Player: " + playerDir.normalized);
        //Debug.Log(Vector3.SqrMagnitude(mouseDir.normalized - playerDir.normalized));
        //======================================================================
        */
        //GetDirections();
        

    }

	void RotationSystem(){
		GetDirections();
		AddTorque();
		CriticalApproach();

	}


	void AddTorque()
    {
        Vector3 x = Vector3.Cross(playerDir.normalized, mouseDir.normalized);
		float T = -x.magnitude * Time.fixedDeltaTime * Mathf.Sign(x.z);
		body.AddTorque(T, ForceMode2D.Impulse);
		//CmdAddTorque(T, ForceMode2D.Impulse);
    }

	[Command]
	void CmdAddTorque(float torque, ForceMode2D mode){
		//body.AddTorque(torque, mode);
		RpcAddTorque(torque, mode);
	}

	[ClientRpc]
	void RpcAddTorque(float torque, ForceMode2D mode){
		body.AddTorque(torque, mode);
	}

	void CriticalApproach(){
		float dampingConstant = 2 * Mathf.Sqrt(body.inertia / 90);
		float torque = -body.angularVelocity * dampingConstant * Time.fixedDeltaTime;
		body.AddTorque(torque, ForceMode2D.Force);
		//CmdAddTorque(torque, ForceMode2D.Force);

		// f/m = w^2, y = c/2m : sqrt(f/m) = y = c/2m ; sqrt(f/m)/2m = c
	}


    void GetDirections()
    {
        //mouseInput = Input.mousePosition - body.transform.position;
		mouseDir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseDir = mouseDir - (Vector2)body.transform.position;
        playerDir = -body.transform.right;
        if(isTurning && CheckSameDirection())
        {
            body.freezeRotation = true;
            isTurning = false;
            body.freezeRotation = false;
        }
    }

    bool CheckSameDirection()
    {
        if (Vector3.SqrMagnitude(mouseDir.normalized - playerDir.normalized) < 0.01)
        {
            return true;
        }
        //Debug.Log("Diffs");
        return false;
    }

}
