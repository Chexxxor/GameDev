using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public enum ITEM { NONE, TREE, STONE };
	Dictionary<ITEM, int> items;

	Canvas inventoryGUI;
	bool inventoryOpen;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		inventoryGUI = GameObject.FindObjectOfType<Canvas>();
		if(!inventoryGUI) {

		}
		items = new Dictionary<ITEM, int>();
		items.Clear();
		inventoryOpen = false;
		inventoryGUI.enabled = false;
	}

	private void Update() {
		if(Input.GetButtonDown("Inventory")) {
			toggleInventory();
		}
	}

	public bool isInventoryOpen() {
		return inventoryOpen;
	}

	public void addItem(ITEM item, int amount) {
		if(items.ContainsKey(item))
			items[item] += amount;
		else
			items.Add(item, amount);
	}

	void toggleInventory() {
		inventoryOpen = !inventoryOpen;
		inventoryGUI.enabled = inventoryOpen;
		Cursor.lockState = inventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
	}
}
