using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

	public AudioSource fanfare;


	private void OnTriggerEnter(Collider other)
	{
		other.attachedRigidbody.useGravity = false;
		if (fanfare)
		{
			fanfare.Play();
		}

		Player player = other.GetComponent<Player>();
		if (player)
		{
			player.enabled = false;
		}

		WinMovement movement = Camera.main.GetComponent<WinMovement>();
		if (movement)
		{
			movement.enabled = true;
		}
	}
}
