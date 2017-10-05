using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraSetup : NetworkBehaviour {
	public Vector3 relativePos;
	public Vector3 relativeRot;

	Camera sceneCamera;

	// Use this for initialization
	void Start () {
		if(!isLocalPlayer)
			return;
		sceneCamera = Camera.main;
		if (!sceneCamera)
		{
			Debug.Log("No camera in scene.");
			return;
		}
		Camera localCamera = Instantiate(sceneCamera);
		localCamera.transform.SetParent(transform);
		localCamera.transform.localPosition = relativePos;
		Quaternion quaternion = new Quaternion();
		quaternion.eulerAngles = relativeRot;
		localCamera.transform.localRotation = quaternion;

		sceneCamera.gameObject.SetActive(false);
		//localCamera.enabled = true;
	}

	private void OnDestroy()
	{
		sceneCamera.gameObject.SetActive(true);
	}
}
