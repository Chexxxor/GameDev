using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour {

	public List<MonoBehaviour> scripts;
	public float delay = 0f;
	public bool isOneTime = true;

	private bool hasTriggered = false;
	private void Enable()
	{
		foreach (MonoBehaviour script in scripts)
		{
			script.enabled = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag("Player") && !(isOneTime && hasTriggered))
		{
			Invoke("Enable", delay);
			if (isOneTime)
			{
				hasTriggered = true;
			}
			
		}
		
			
	}
}
