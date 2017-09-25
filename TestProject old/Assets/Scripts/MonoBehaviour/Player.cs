using UnityEngine;

public class Player : MonoBehaviour {
	public Inventory inventory = new Inventory();

	private void Start() {
		inventory = GetComponent<Inventory>();
		if(!inventory) {
			Debug.Log("Inventory not found, disabling inventory functions");
		}
	}

	public void harvest(Harvestable harvestable) {
		if(inventory)
			inventory.addItem(harvestable.getItem(), harvestable.harvest(this));
	}
}
