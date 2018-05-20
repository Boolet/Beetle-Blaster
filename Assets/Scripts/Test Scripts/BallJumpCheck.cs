using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJumpCheck : MonoBehaviour {

	[SerializeField] float logLength = 0.1f;
	[SerializeField] float minJumpLength = 0.01f;

	Queue<KeyValuePair<float, Vector3>> positionLogQueue = new Queue<KeyValuePair<float, Vector3>>();
	
	// Update is called once per frame
	void Update () {
		positionLogQueue.Enqueue(new KeyValuePair<float, Vector3>(Time.time, transform.position));
		float jump;
		if (HasJumped(transform.position, out jump))
			print("jump detected: Magnitude " + jump);
		RemoveUpToTime(Time.time - logLength);
	}

	void RemoveUpToTime(float time){
		while (positionLogQueue.Peek().Key <= time)
			positionLogQueue.Dequeue();
	}

	bool HasJumped(Vector3 position, out float magnitude){
		KeyValuePair<float, Vector3> peek = positionLogQueue.Peek();
		return (magnitude = Vector3.Distance(position, peek.Value)) >= minJumpLength;
	}
}
