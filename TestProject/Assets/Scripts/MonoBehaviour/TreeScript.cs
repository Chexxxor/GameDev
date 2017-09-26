using UnityEngine;

public class TreeScript : Harvestable {
	private static readonly Sprite icon;
	public static string text = "Tree";
	public static int minAmount;
	public static int maxAmount;

	public override int Harvest(Player player) {
		return Random.Range(minAmount, maxAmount);
	}

	public override Inventory.ITEM Item {
		get{
			return Inventory.ITEM.TREE;
		}
	}

	public override string Name {
		get {
			return text;
		}
	}
}
