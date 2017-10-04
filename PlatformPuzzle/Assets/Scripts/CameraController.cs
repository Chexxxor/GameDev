using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
		if (!player)
		{
			Debug.Log("No player gameobject attached to camera");
		}
    }

    void LateUpdate()
    {
		if (player)
		{
			transform.position = player.transform.position + offset;
		}
    }
}