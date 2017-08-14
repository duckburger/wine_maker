using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item  {

	public int itemID;
	public string itemName;
	public string itemDescription;
	public ItemType itemType;
	public bool itemStackable;
	public Texture2D itemIcon;



	public enum ItemType {
		consumable,
		weapon,
		tool
	}



	public Item (int ID, string name, string description, ItemType type, bool stackable)
	{
		itemID = ID;
		itemName = name;
		itemDescription = description;
		itemType = type;
		itemStackable = stackable;
		itemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
	}

	public Item ()
	{
	
	}
	
	
}
