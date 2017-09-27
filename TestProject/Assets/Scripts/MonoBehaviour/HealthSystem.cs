using UnityEngine;

public class HealthSystem : MonoBehaviour {
	Character character;
	//BoxCollider head;
	BoxCollider body;
	public int maxHealth;

	int currentHealth;
	bool hasBody = true;
	bool isCharacter = true;

	// Use this for initialization
	void Start () {
		if(!(body = GetComponent<BoxCollider>())) {
			Debug.Log("No BoxCollider for the body attached. Health system won't work");
			hasBody = false;
			this.enabled = false;
		}
		if(!(character = GetComponent<Character>())) {
			Debug.Log("Owner is not a character.");
			isCharacter = false;
		}
	}

	private void OnTriggerEnter(Collider other) {
		Projectile projectile = other.GetComponent<Projectile>();
		if(projectile) {
			Player source = projectile.owner;
			if(isCharacter && source) {
				source.onDamageDealt(character);
			}
		}
	}

	private void onDamage(Projectile projectile) {
		currentHealth -= projectile.baseDamage;

		string name = projectile.owner ? projectile.owner.name : "unknown";
		if(currentHealth <= 0)
			Debug.Log("Killed by " + name + ".");
		else
			Debug.Log("Damaged by " + name + ".");
	}
}
