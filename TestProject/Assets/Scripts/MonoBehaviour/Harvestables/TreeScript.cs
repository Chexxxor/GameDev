using UnityEngine;

public class TreeScript : Harvestable {
	public static Sprite icon;
	public int minAmount = 1;
	public int maxAmount = 3;

	public override int HarvestSpecific(Player player) {
		return Random.Range(minAmount, maxAmount);
	}

	public override Inventory.ITEM Item {
		get{
			return Inventory.ITEM.TREE;
		}
	}

	public override string Name {
		get {
			return "Tree";
		}
	}
}
