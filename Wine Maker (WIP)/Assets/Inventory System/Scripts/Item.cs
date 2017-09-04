using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item  {

	public int itemID;
	public string itemSlug;
	public string itemName;
	public string itemDescription;
	public bool itemStackable;
	public Sprite itemIcon;
	



	public enum ItemType {
		consumable,
		weapon,
		tool
	}


	[Newtonsoft.Json.JsonConstructor]				// This has to be above the constructor of the item we are tryong to build from a JSON array
	public Item (int ID, string slug, string name, string description, bool stackable)
	{
		itemID = ID;
		itemSlug = slug;
		itemName = name;
		itemDescription = description;
		itemStackable = stackable;
		itemIcon = Resources.Load<Sprite>("Item Icons/" + name);
		
	}

	public Item ()
	{
	
	}
	
	
}
