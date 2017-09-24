using UnityEngine;

public abstract class Harvestable : MonoBehaviour {
	public abstract Inventory.ITEM getItem();
	public abstract int harvest(Player player);
}
