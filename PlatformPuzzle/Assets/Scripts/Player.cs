using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float strength = 5f;
	public float maxSpeed = 5f;
	public float jumpSpeed = 5f;
	public float airControl = 5f;

    private UnityEngine.Rigidbody rb;
	bool isGrounded = false;

    void Start()
    {
		rb = GetComponent<UnityEngine.Rigidbody>();
    }

    void FixedUpdate()
    {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);

		if (isGrounded)
		{
			if (rb.velocity.magnitude < maxSpeed)
			{
				rb.AddForce(movement * strength);
			}
		}
		else
		{
			if (rb.velocity.magnitude < maxSpeed)
			{
				rb.AddForce(movement * airControl);
			}
		}
	}

	private void Update()
	{
		if (isGrounded)
		{
			if (Input.GetButtonDown("Jump"))
				rb.velocity = new Vector3(rb.velocity.x, jumpSpeed + rb.velocity.y, rb.velocity.z);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		isGrounded = true;
	}

	private void OnTriggerExit(Collider other)
	{
		isGrounded = false;
	}
}

