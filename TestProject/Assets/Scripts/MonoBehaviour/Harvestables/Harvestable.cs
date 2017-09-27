using UnityEngine;

public abstract class Harvestable : MonoBehaviour {
	public abstract Inventory.ITEM Item { get; }
	public abstract string Name { get; }

	public int Harvest(Player player) {
		Debug.Log(Name + " harvested by " + player.characterName);
		Destroy(gameObject);
		return HarvestSpecific(player);
	}

	public abstract int HarvestSpecific(Player player);
}
