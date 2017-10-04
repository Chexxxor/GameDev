using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour {
	public float strength;

	protected Rigidbody rb;
	protected Vector3 moveDirection;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		moveDirection = new Vector3(0, 0, Input.GetAxis("Horizontal"));

		move();
	}

	protected abstract void move();
}





