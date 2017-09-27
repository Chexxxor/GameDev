using UnityEngine;

public class StoneScript : Harvestable {
	public static Sprite icon;
	public int minAmount = 1;
	public int maxAmount = 3;

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

	public override int HarvestSpecific(Player player) {
		return Random.Range(minAmount, maxAmount);
	}
}
