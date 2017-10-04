using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMovement : MonoBehaviour {

	public Transform spaceBall;
	public float spaceRotate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.RotateAround(spaceBall.position, new Vector3(0, 0, 1), spaceRotate * Time.deltaTime);
	}
}
