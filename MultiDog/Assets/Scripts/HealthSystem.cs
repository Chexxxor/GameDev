using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HealthSystem : NetworkBehaviour {
	BoxCollider body;
	public int maxHealth;
	Text gameOverText;
	HealthBar bar;

	int currentHealth;
	bool useText = true;
	bool useBar = true;

	// Use this for initialization
	void Start () {

		if(!(gameOverText = FindObjectOfType<Text>())) {
			Debug.Log("Couldn't find text object for Game Over text!");
			useText = false;
		}
		if(!(bar = GetComponentInChildren<HealthBar>())) {
			Debug.Log("Found no Healthbar");
			useBar = false;
		}
		currentHealth = maxHealth;
	}

	public bool isDead() {
		return currentHealth <= 0;
	}

	private void OnTriggerEnter(Collider other) {
		if (isServer)
		{
			Projectile projectile = other.GetComponent<Projectile>();
			if (projectile)
			{
				RpcDamage(projectile);
				Destroy(projectile.gameObject);
			}
		}
	}

	[ClientRpc]
	private void RpcDamage(Projectile projectile) {
		currentHealth -= projectile.baseDamage;
		if(useBar) {
			bar.ScaleBar(currentHealth / (float)maxHealth);
		}
		if(currentHealth <= 0) {
			die();
		}
	}

	private void die() {
		if(isLocalPlayer && useText)
			gameOverText.enabled = true;
	}

	public void Revive() {
		CmdReset();
		if(isLocalPlayer && useText)
			gameOverText.enabled = false;
	}

	[Command]
	public void CmdReset() {
		RpcResetHealth();
	}

	[ClientRpc]
	public void RpcResetHealth() {
		currentHealth = maxHealth;
		bar.ScaleBar(1);
	}
}
