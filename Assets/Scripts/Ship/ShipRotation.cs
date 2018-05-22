using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotation : MonoBehaviour {

    Rigidbody2D body;
    Vector3 playerDir;
    Vector3 mouseDir;
    Vector3 mouseInput;
    bool isTurning;
    // Use this for initialization
    float _angle;
    Vector3 mousePosition;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        GetDirections();
        isTurning = false;
    }
	
	void FixedUpdate ()
    {
        if(!isTurning && !CheckSameDirection())
        {
            AddTorque(Vector2.SignedAngle(mouseDir, playerDir));
            isTurning = true;
        }
        //======================================================================
        //_angle = Vector2.SignedAngle(playerDir, mouseDir);
        //Debug.Log(_angle);
        //Debug.Log("Mouse:" + mouseDir.normalized + "   Player: " + playerDir.normalized);
        //Debug.Log(Vector3.SqrMagnitude(mouseDir.normalized - playerDir.normalized));
        //======================================================================
        GetDirections();

    }
    void AddTorque(float angle)
    {
        Vector3 x = Vector3.Cross(playerDir.normalized, mouseDir.normalized);
        float theta = Mathf.Asin(x.magnitude);
        Vector3 w = x.normalized * theta / Time.fixedDeltaTime;
        float alpha = w.z;
        float T = body.inertia * w.z;
        //T = T/10;
        body.AddTorque(T, ForceMode2D.Impulse);
    }

    void GetDirections()
    {
        mouseInput = Input.mousePosition - body.transform.position;
        mousePosition = Camera.main.ScreenToWorldPoint(mouseInput);
        mouseDir = mousePosition - body.transform.position;
        mouseDir.z = 0;
        playerDir = body.transform.right;
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
