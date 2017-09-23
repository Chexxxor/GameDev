using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory2 : MonoBehaviour {
	enum ITEM { TREE, STONE };

	Dictionary<ITEM, int> items;

	// Use this for initialization
	void Start () {
		items = new Dictionary<ITEM, int>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void addItem(ITEM item, int amount) {
		if(items.ContainsKey(item))
			items[item] += amount;
		else
			items.Add(item, amount);
	}
}
