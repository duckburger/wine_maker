using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {

		items.Add(new Item(1, "Empty Grape Basket", "A simple basket designed specifically for carrying grapes", Item.ItemType.tool, false));
		items.Add(new Item(2, "Full Grape Basket (Unsorted)", "A grape basket full of unsorted grapes", Item.ItemType.tool, false));
		items.Add(new Item(3, "Full Grape Basket (Sorted)", "A grape basket full of sorted grapes", Item.ItemType.tool, false));
		items.Add(new Item(4, "Empty Clay Jar", "A jar you could carry wine juice in", Item.ItemType.tool, false));
		items.Add(new Item(5, "Full Clay Jar", "A jar full of wine", Item.ItemType.tool, true));
		items.Add(new Item(6, "Empty Wine Barrel", "A fine barrel for storing wine. Except it's empty.", Item.ItemType.tool, false));
		items.Add(new Item(7, "Full Wine Barrel", "A barrel full of wine.", Item.ItemType.tool, false));
		items.Add(new Item(8, "Empty Wine Bottle", "An empty bottle that can be used to bottle wine.", Item.ItemType.tool, true));
		items.Add(new Item(9, "Full Wine Bottle", "A bottle full of wine. All that's left is to give this wine a name and label it.", Item.ItemType.consumable, true));




	}

	// Update is called once per frame
	void Update () {
		
	}
}
