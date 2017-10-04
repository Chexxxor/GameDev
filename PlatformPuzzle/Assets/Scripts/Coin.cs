using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	AudioSource kaPling;
	MeshRenderer mesh;
	Score score;

    private void Start()
    {
        GameObject scoreObject = GameObject.Find("Score");
		score = scoreObject.GetComponent<Score>();
		score.AddTargetsScore(1);
		kaPling = GetComponent<AudioSource>();
		mesh = GetComponent<MeshRenderer>();
	}
	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("Player") && this.enabled)
		{
			kaPling.Play();
			score.Addscore(1);
			mesh.enabled = false;
			Destroy(gameObject, kaPling.clip.length);
			this.enabled = false;
		}
	}
}
