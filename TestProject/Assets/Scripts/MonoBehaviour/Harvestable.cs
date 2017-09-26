using UnityEngine;

public abstract class Harvestable : MonoBehaviour {
	public abstract Inventory.ITEM Item { get; }
	public abstract string Name { get; }

	public abstract int Harvest(Player player);
}
