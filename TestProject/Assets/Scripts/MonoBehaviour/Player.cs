using UnityEngine;

public class Player : Character {
	Inventory inventory;

	private void Start() {
		//inventory = gameObject.AddComponent<Inventory>();
		inventory = GetComponent<Inventory>();
		if(!inventory) {
			Debug.Log("Inventory not found, disabling inventory functions");
		}
	}

	public void harvest(Harvestable harvestable) {
		if(inventory)
			inventory.addItem(harvestable.Item, harvestable.Harvest(this));
	}

	public void onDamageDealt(Character target) {

	}
}
