﻿using UnityEngine;

public class Projectile : MonoBehaviour {
	public Player owner;
	public float lifespan = 10;

	private float age;

	// Use this for initialization
	void Start () {
		// TODO: Make a child class for other collider types
		CapsuleCollider collider;
		if(collider = GetComponentInChildren<CapsuleCollider>())
			transform.Translate(0, 0, collider.height);
		else
			Debug.Log("Projectile has no collider, consider adding one.");
	}

	// Update is called at fixed times
	private void FixedUpdate() {
		age += Time.fixedDeltaTime;
		if(age > lifespan)
			Destroy(gameObject);
	}

	private void OnCollisionEnter(Collision collision) {
		Harvestable harvestable = collision.gameObject.GetComponent<Harvestable>();
		if(harvestable && owner) {
			owner.harvest(harvestable);
			Destroy(gameObject);
		}
	}
}
