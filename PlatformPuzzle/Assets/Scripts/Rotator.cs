using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	Vector3 rotate;

	private void Start()
	{
		rotate = new Vector3(Random.Range(-45, 45), Random.Range(-30, 30), Random.Range(-45, 45));
	}
	void Update ()
    {
        transform.Rotate(rotate * Time.deltaTime);
	}
}
