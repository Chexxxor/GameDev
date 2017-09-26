using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Weekly2 : MonoBehaviour {
	AudioSource source;

	KeyCode[] keys = {
		KeyCode.Q,
		KeyCode.A,
		KeyCode.W,
		KeyCode.S,
		KeyCode.E,
		KeyCode.R,
		KeyCode.F,
		KeyCode.T,
		KeyCode.G,
		KeyCode.Y,
		KeyCode.H,
		KeyCode.U,
		KeyCode.I
	};
	
	private void Awake() {
		source = GetComponent<AudioSource>();
		if(!source) {
			Debug.Log("No AudioSource component found!");
		}
	}

	// Update is called once per frame
	void Update () {
		if(source) {
			for(int i = 0; i < keys.Length; i++) {
				if(Input.GetKeyDown(keys[i])) {
					source.pitch = getPitch(i);
					source.Play();
				}
			}
		}
	}

	float getPitch(int steps) {
		return Mathf.Pow(1.05946f, steps);
	}
}
