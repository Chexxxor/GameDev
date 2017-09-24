using UnityEngine;
using UnityEngine.UI;

public class TutorialInventory : MonoBehaviour {

	public Image[] itemImages = new Image[numItemSlots];
	public Item[] items = new Item[numItemSlots];
	public int[] itemAmount = new int[numItemSlots];

	public const int numItemSlots = 8;

	public void AddItem(Item item, int amount) {
		for(int i = 0; i < items.Length; i++) {
			if(items[i] == null) {
				items[i] = item;
				itemImages[i].sprite = item.sprite;
				itemImages[i].enabled = true;
				itemAmount[i] = amount;
				return;
			}
		}
	}

	public void RemoveItem(Item item) {
		for(int i = 0; i < items.Length; i++) {
			if(items[i] == item) {
				items[i] = null;
				itemImages[i].sprite = null;
				itemImages[i].enabled = false;
				itemAmount[i] = 0;
				return;
			}
		}
	}
}
