using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewItemDatabase : MonoBehaviour {

	private List<Item> database = new List<Item>();

	// Use this for initialization
	void Start() {
		database.Add(new Item(1, "Empty Grape Basket", "A simple basket designed specifically for carrying grapes", ItemType.tool, false));
		database.Add(new Item(2, "Full Grape Basket (Unsorted)", "A grape basket full of unsorted grapes", ItemType.tool, false));
		database.Add(new Item(3, "Full Grape Basket (Sorted)", "A grape basket full of sorted grapes", ItemType.tool, false));
		database.Add(new Item(4, "Empty Clay Jar", "A jar you could carry wine juice in", ItemType.tool, false));
		database.Add(new Item(5, "Full Clay Jar", "A jar full of wine", ItemType.tool, true));
		database.Add(new Item(6, "Empty Wine Barrel", "A fine barrel for storing wine. Except it's empty.", ItemType.tool, false));
		database.Add(new Item(7, "Full Wine Barrel", "A barrel full of wine.", ItemType.tool, false));
		database.Add(new Item(8, "Empty Wine Bottle", "An empty bottle that can be used to bottle wine.", ItemType.tool, true));
		database.Add(new Item(9, "Full Wine Bottle", "A bottle full of wine. All that's left is to give this wine a name and label it.", ItemType.consumable, true));

		
	}

	// Update is called once per frame
	void Update() {

	}

	public enum ItemType
	{
		consumable,
		weapon,
		tool
	}


	public class Item
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ItemType Type { get; set; }
		public bool Stackable { get; set; }
		public Texture2D Icon { get; set; }


		public Item()
		{
			ID = -1;
		}

		public Item (int itemID, string itemName, string itemDescription, ItemType itemType, bool isItemStackable)
		{
			this.ID = itemID;
			this.Name = itemName;
			this.Description = itemDescription;
			this.Type = itemType;
			this.Stackable = isItemStackable;
			this.Icon = Resources.Load<Texture2D>("Item Icons/" + itemName);

		}

	}

	

}
