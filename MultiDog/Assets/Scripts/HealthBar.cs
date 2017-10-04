using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthBar : MonoBehaviour {
	float maxScale;

	private void Start() {
		maxScale = transform.localScale.x;
	}

	public void ScaleBar(float factor) {
		if(factor < 0)
			factor = 0;
		transform.localScale = new Vector3(maxScale * factor, 1, 1);
	}
}
