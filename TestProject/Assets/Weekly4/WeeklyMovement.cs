using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeklyMovement : MonoBehaviour {
	public float gravity;
	public float moveSpeed;
	public float jumpSpeed;

	Vector3 moveDir = Vector3.zero;
	CharacterController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(controller.isGrounded) {
			moveDir = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
			if(moveDir.magnitude > 1)
				moveDir.Normalize();
			moveDir *= moveSpeed;
			if(Input.GetButtonDown("Jump"))
				moveDir.y = jumpSpeed;
		}
		moveDir.y -= gravity * Time.deltaTime;
		controller.Move(moveDir * Time.deltaTime);
	}
}
