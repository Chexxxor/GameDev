using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

	public float damage;

	private void OnTriggerStay(Collider other)
	{
		Health health = other.GetComponent<Health>();
		if (health)
		{
			health.Damage(damage * Time.deltaTime);
		}
	}
}
