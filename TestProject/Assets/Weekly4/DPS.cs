using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPS : MonoBehaviour {
	public float damage;

	Health health;

	private void Start() {
		if(!(health = GetComponent<Health>())) {
			Debug.Log("No health component found, disabling damage!");
			this.enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		health.Damage(damage * Time.deltaTime);
	}
}
