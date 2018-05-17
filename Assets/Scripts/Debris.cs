using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The top-level class for all debris objects. Will contain references to the debris' other
/// required components to avoid calls to GetComponent.
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DebrisOwnership))]
public class Debris : MonoBehaviour {
	public Collider2D attachedCollider;
	public Rigidbody2D rigidBody2D;
	public DebrisOwnership ownershipScript;
}
