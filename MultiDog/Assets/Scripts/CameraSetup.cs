using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraSetup : NetworkBehaviour {
	public Vector3 relativePos;
	public Vector3 relativeRot;

	Camera localCamera;

	// Use this for initialization
	void Start () {
		if(!isLocalPlayer)
			return;
		if(localCamera = FindObjectOfType<Camera>())
			localCamera.transform.SetParent(transform);
		else {
			// If no camera
			Debug.Log("No camera found");
		}
		localCamera.transform.localPosition = relativePos;
		Quaternion quaternion = new Quaternion();
		quaternion.eulerAngles = relativeRot;
		localCamera.transform.localRotation = quaternion;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnDestroy() {
	}
}
