using UnityEngine;

public class TreeScript : Harvestable {
	public static Sprite icon;
	public static string text = "Tree";
	public int minAmount;
	public int maxAmount;

	public override int harvest(Player player) {
		return Random.Range(minAmount, maxAmount);
	}

	public override Inventory.ITEM getItem() {
		return Inventory.ITEM.TREE;
	}
}
