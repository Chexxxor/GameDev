using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour {
	public Sprite tree;
	public Sprite stone;

	// Use this for initialization
	void Start () {
		TreeScript.icon = tree;
		StoneScript.icon = stone;
	}
}
