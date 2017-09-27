using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public enum ITEM { NONE, TREE, STONE };
	Dictionary<ITEM, int> items;

	Canvas inventoryCanvas;
	bool inventoryOpen = false;
	bool inventoryCanvasFound = true;

	Image[] inventorySlots;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		inventoryCanvas = GameObject.FindObjectOfType<Canvas>();
		if(!inventoryCanvas) {
			Debug.Log("No Inventory GUI. Inventory functions disabled.");
			inventoryCanvasFound = false;
		}
		items = new Dictionary<ITEM, int>(8);
		items.Clear();
		if(inventoryCanvasFound) {
			inventoryCanvas.enabled = false;
			findItemSlots();
		}
	}

	private void findItemSlots() {
		GameObject[] objects = GameObject.FindGameObjectsWithTag("ItemImage");
		inventorySlots = new Image[objects.Length];
		for(int i = 0; i < objects.Length; i++){
			inventorySlots[i] = objects[i].GetComponent<Image>();
		}
		/*Transform inventoryPanel = inventoryCanvas.GetComponentInChildren<Transform>();
		if(inventoryPanel != null) {
			Transform[] itemSlots = inventoryPanel.GetComponentsInChildren<Transform>();
			if(itemSlots != null) {
				Debug.Log("Found " + itemSlots.Length + " slots.");
				inventorySlots = new Image[itemSlots.Length];
				for(int i = 0; i < itemSlots.Length; i++) {
					Image[] slotImgs = itemSlots[i].GetComponentsInChildren<Image>(true);
					inventorySlots[i] = slotImgs[1];
				}
			}
		}*/
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
		if(inventoryCanvasFound) {
			openInventory(inventoryOpen);
			Cursor.lockState = inventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
		}
	}

	private void openInventory(bool open) {
		inventoryCanvas.enabled = open;
		if(open) {
			int i = 0;
			foreach(ITEM item in items.Keys) {
				inventorySlots[i].enabled = true;
				inventorySlots[i].sprite = (item == ITEM.STONE ? StoneScript.icon : TreeScript.icon);
				i++;
			}
			for(; i < inventorySlots.Length; i++) {
				inventorySlots[i].sprite = null;
				inventorySlots[i].enabled = false;
			}
		}
	}


}
