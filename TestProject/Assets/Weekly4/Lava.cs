using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
	public float damage = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay(Collider other) {
		Health health;
		if(health = other.GetComponent<Health>())
			health.Damage(damage * Time.deltaTime);
	}
}
