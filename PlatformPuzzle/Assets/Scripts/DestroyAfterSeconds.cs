using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
	public float delay = 2f;

	private void Start()
	{
		Invoke("DestroyMe", delay);
	}

	void DestroyMe()
	{
		Destroy(gameObject);
	}

}
