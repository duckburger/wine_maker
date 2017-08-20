using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();
	public static ItemDatabase Instance { get; set; }

	// Use this for initialization
	void Start () {

		if (Instance != null && Instance != this)
		{
			// Make sure that there is only one instance of this database (singleton pattern)

			Destroy(gameObject);
		} else
		{
			Instance = this;
			BuildDatabase();
		}


	}

	private void BuildDatabase() // read all the text from items.json, and pul it out as a list ( variables are determined by the Item constructor: see json comment on top of it)
	{
		items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/items").ToString());
		
	}

	public Item GetItem (string requestedSlug) // Chech whether an item exists in the database
	{
		foreach (Item item in items)
		{
			if (item.itemSlug == requestedSlug)
			{
				return item;
			}
		}
		Debug.Log("No item with the slug " + requestedSlug + " found in the inventory");
		return null;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
