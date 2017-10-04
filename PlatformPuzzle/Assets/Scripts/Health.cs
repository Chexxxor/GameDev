using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour {

	public float health;
	public Text hp;

	private void Start()
	{
		if (!hp)
		{
			print("Text not available");
		}

		else
		{
			hp.text = "" + health;
		}

		
	}

	public void Damage (float damage)
	{
		health -= damage;
		
		if (hp)
		{
			hp.text = "" + health;
		}

		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
	
}
