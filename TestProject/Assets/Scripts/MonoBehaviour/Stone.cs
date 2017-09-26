using UnityEngine;

public class Stone : Harvestable {
	public static Sprite icon;
	public int minAmount;
	public int maxAmount;

	public override Inventory.ITEM Item {
		get {
			return Inventory.ITEM.STONE;
		}
	}

	public override string Name {
		get {
			return "Stone";
		}
	}

	public override int Harvest(Player player) {
		return Random.Range(minAmount, maxAmount);
	}
}
