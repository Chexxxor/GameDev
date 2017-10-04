using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour {

	public Transform a;
	public Transform b;
	public bool doRepeat = true;
	public float speed;
	public float force;

	bool isReturning = false;
	Vector3 direction = Vector3.zero;
	UnityEngine.Rigidbody rb;
	float thresHold = 1;

	void Start ()
	{
		transform.position = a.position;
		rb = GetComponent<UnityEngine.Rigidbody>();
		if (!rb)
		{
			print("No rigidbody on slider");
		}
	}
	
	
	void FixedUpdate ()
	{
		if((transform.position - (isReturning ? a.position : b.position)).magnitude < thresHold)
		{
			isReturning = !isReturning;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			transform.position = (isReturning ? b.position : a.position);
			if (!doRepeat)
			{
				this.enabled = false;
				return;
			}
		}
		if (isReturning)
		{
			direction = (a.position - transform.position).normalized;
		}
		else
		{
			direction = (b.position - transform.position).normalized;
		}
		if (rb.velocity.magnitude < speed || Vector3.Dot(rb.velocity, direction) < 0)
		{
			rb.AddForce(direction * force);
		}
	}
}
