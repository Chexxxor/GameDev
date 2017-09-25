using UnityEngine;

public class Stone : Harvestable {
	public static Sprite icon;
	public int minAmount;
	public int maxAmount;

	public override int harvest(Player player) {
		return Random.Range(minAmount, maxAmount);
	}

	public override Inventory.ITEM getItem() {
		return Inventory.ITEM.STONE;
	}
}
