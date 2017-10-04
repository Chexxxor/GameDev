using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HealthSystem : NetworkBehaviour {
	//Character character;
	//BoxCollider head;
	BoxCollider body;
	public int maxHealth;
	Text gameOverText;
	HealthBar bar;
	//short playerId;

	int currentHealth;
	//bool hasBody = true;
	//bool isCharacter = true;
	bool useText = true;
	bool useBar = true;

	// Use this for initialization
	void Start () {
		/*if(!(body = GetComponent<BoxCollider>())) {
			Debug.Log("No BoxCollider for the body attached. Health system won't work");
			hasBody = false;
			this.enabled = false;
		}*/
		/*if(!(character = GetComponent<Character>())) {
			Debug.Log("Owner is not a character.");
			isCharacter = false;
		}*/
		if(!(gameOverText = FindObjectOfType<Text>())) {
			Debug.Log("Couldn't find text object for Game Over text!");
			useText = false;
		}
		if(!(bar = GetComponentInChildren<HealthBar>())) {
			Debug.Log("Found no Healthbar");
			useBar = false;
		}
		// TODO: Get nullreference check
		/*if(isLocalPlayer) {
			CmdSetID(GetComponent<NetworkIdentity>().playerControllerId);
		}*/
		currentHealth = maxHealth;
	}

	/*[Command]
	public void CmdSetID(short id) {
		playerId = id;
	}*/

	public bool isDead() {
		return currentHealth <= 0;
	}

	private void OnTriggerEnter(Collider other) {
		Projectile projectile = other.GetComponent<Projectile>();
		if(projectile) {
			//if(projectile.owner != playerId) {
				onDamage(projectile);
				//short source = projectile.owner;
				/*if(isCharacter) {
					source.onDamageDealt(character);
				}*/
				Destroy(projectile.gameObject);
			//}
		}
	}

	private void onDamage(Projectile projectile) {
		currentHealth -= projectile.baseDamage;
		if(useBar) {
			bar.ScaleBar(currentHealth / (float)maxHealth);
		}
		//string name = projectile.owner ? projectile.owner.name : "unknown";
		if(currentHealth <= 0) {
			//Debug.Log("Killed by " + name + ".");
			die();
		} //else
			//Debug.Log("Damaged by " + name + ".");
	}

	private void die() {
		if(isLocalPlayer && useText)
			gameOverText.enabled = true;
	}

	public void Revive() {
		/*currentHealth = maxHealth;
		bar.ScaleBar(1);*/
		//CmdResetHealth();
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
