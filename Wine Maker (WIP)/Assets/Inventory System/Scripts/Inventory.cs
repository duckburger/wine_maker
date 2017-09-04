using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	ItemDatabase itemDatabase;
	public GameObject inventoryPanel; // Reference to the UI element
	GameObject slotPanel; // Reference to the UI element
	public GameObject inventorySlot; // Reference to the prefab of a slot (with a background)
	public GameObject inventoryItem; // Reference to the prefab of an item (an GO with an image on it)
	public GameObject lastAddedItem = null;

	public List<Item> inventoryItems = new List<Item>(); // A lits of all items in inventory
	public List<GameObject> slots = new List<GameObject>(); // A list of all slots in inventory


	private int slotAmount; // How many slots to spawn

	// Use this for initialization
	void Start() {

		itemDatabase = GetComponent<ItemDatabase>();

		slotAmount = 8;

		
		slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;

		for (int i = 0; i < slotAmount; i++)  // Fill the inventory with EMPTY items and fill the slots with slot prefabs. Set slots' parent to the slot panel and assign each slot's ID. Resize each slot to 1 1 1
		{
			inventoryItems.Add(new Item());
			slots.Add(Instantiate(inventorySlot));
			slots[i].transform.SetParent(slotPanel.transform);

			slots[i].GetComponent<Slot>().myID = i;

			slots[i].transform.localScale = new Vector3(1, 1, 1);
		}


		
		AddItem("empty_grape_basket", 1);
		//AddItem("full_grape_basket_s", 1);
		//AddItem("full_grape_basket_s", 1);

	}


	public bool CheckIfInventoryHasEmptySlots()
	{
		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i].transform.childCount == 0)
			{
				return true;
			}
		}
		print("No empty slots in inventory!");
		return false;
	}

	public Item AddItem(string slug, int amountOfItem) // Method that adds items by slug and amount to add
	{
		Item itemToAdd = itemDatabase.GetItem(slug); // Grabbing the item we want to add from the database

		if (amountOfItem >= 20 && CheckIfInventoryHasEmptySlots())
		{
			for (int i = 0; i < inventoryItems.Count; i++) // Go through the items in inventory 
			{
				if (inventoryItems[i].itemSlug == null) // Add this item to an empty slot if the item is not stackable or there isn't an instance of this item in the inventory
				{
					inventoryItems[i] = itemToAdd;
					GameObject invObj = Instantiate(inventoryItem); // Spawning the inventory item prefab to then fill out with the passed item's properties

					invObj.transform.SetParent(slots[i].transform);   // Parent the item prefab to the current slot
					invObj.transform.position = Vector2.zero;  // Center the prefab
					invObj.transform.localScale = new Vector3(1, 1, 1);  // Normalize its size
					invObj.GetComponent<Image>().sprite = itemToAdd.itemIcon;  // Change its image to one of the added item


					invObj.GetComponent<ItemData>().currentSlot = i;   // Set the item's slot to current slot
					invObj.GetComponent<ItemData>().amount = amountOfItem; // Set the amount to the amount we added
					invObj.GetComponent<ItemData>().item = itemToAdd; // Notify the profab that it is the added item
					invObj.GetComponentInChildren<Text>().text = invObj.GetComponent<ItemData>().amount.ToString(); // Write the amount of item in the bottom right corner

					invObj.name = itemToAdd.itemName; // Change the prefab's name to that of the item it will represent

					lastAddedItem = slots[i].gameObject.GetComponentInChildren<ItemData>().gameObject;

					return inventoryItems[i];
				}
			}
		}


		for (int i = 0; i < inventoryItems.Count; i++) // Go through the items in inventory
		{
			if (inventoryItems[i].itemSlug == null) // Add this item to an empty slot if the item is not stackable or there isn't an instance of this item in the inventory
			{
				inventoryItems[i] = itemToAdd;
			
				GameObject invObj = Instantiate(inventoryItem); // Spawning the inventory item prefab to then fill out with the passed item's properties

				invObj.transform.SetParent(slots[i].transform);   // Parent the item prefab to the current slot
				invObj.transform.localPosition = new Vector3(0, 0, 0);  // Center the prefab
				
				//Debug.Log("Added the " + itemToAdd.itemName + " to the inventory");
				invObj.transform.localScale = new Vector3(1, 1, 1);  // Normalize its size
				invObj.GetComponent<Image>().sprite = itemToAdd.itemIcon;  // Change its image to one of the added item


				invObj.GetComponent<ItemData>().currentSlot = i;   // Set the item's slot to current slot
				invObj.GetComponent<ItemData>().amount = amountOfItem; // Set the amount to the amount we added
				invObj.GetComponent<ItemData>().item = itemToAdd; // Notify the profab that it is the added item
				invObj.GetComponentInChildren<Text>().text = invObj.GetComponent<ItemData>().amount.ToString(); // Write the amount of item in the bottom right corner

				invObj.name = itemToAdd.itemName; // Change the prefab's name to that of the item it will represent
				lastAddedItem = slots[i].gameObject.GetComponentInChildren<ItemData>().gameObject;
				return inventoryItems[i];
			}
			else if ((inventoryItems[i].itemSlug == itemToAdd.itemSlug && itemToAdd.itemStackable && amountOfItem >= 20))
			{
				int differenceToTwenty = 20 - slots[i].GetComponentInChildren<ItemData>().amount;
				slots[i].GetComponentInChildren<ItemData>().amount += (differenceToTwenty); // Add the passed amount to the slot with the same item
				slots[i].GetComponentInChildren<Text>().text = slots[i].GetComponentInChildren<ItemData>().amount.ToString(); // Write the new amount of item out
				int remainingAmount = amountOfItem - differenceToTwenty;

				AddRemainingAmountOfItem(itemToAdd, remainingAmount);
				lastAddedItem = slots[i].gameObject.GetComponentInChildren<ItemData>().gameObject;

				return inventoryItems[i];
			}
			else if ((inventoryItems[i].itemSlug == itemToAdd.itemSlug && itemToAdd.itemStackable && (slots[i].transform.GetChild(0).GetComponent<ItemData>().amount + amountOfItem) >= 20f))
			{




				int differenceToTwenty = 20 - slots[i].GetComponentInChildren<ItemData>().amount;
				slots[i].GetComponentInChildren<ItemData>().amount += (differenceToTwenty); // Add the passed amount to the slot with the same item
				slots[i].GetComponentInChildren<Text>().text = slots[i].GetComponentInChildren<ItemData>().amount.ToString(); // Write the new amount of item out
				int remainingAmount = amountOfItem - differenceToTwenty;

				AddRemainingAmountOfItem(itemToAdd, remainingAmount);

				lastAddedItem = slots[i].gameObject.GetComponentInChildren<ItemData>().gameObject;
				return inventoryItems[i];
			}
			else if (inventoryItems[i].itemSlug == itemToAdd.itemSlug && itemToAdd.itemStackable && amountOfItem > 0) // Add the amount of this item to an already existing stack
			{
				inventoryItems[i] = itemToAdd; // Put the item we're adding in the inventory at this ID

				slots[i].GetComponentInChildren<ItemData>().currentSlot = i; // Make sure this item's internal ID matches the slot's ID
				slots[i].GetComponentInChildren<ItemData>().amount += amountOfItem; // Add the passed amount to the slot with the same item
				slots[i].GetComponentInChildren<Text>().text = slots[i].GetComponentInChildren<ItemData>().amount.ToString(); // Write the new amount of item out

				lastAddedItem = slots[i].gameObject.GetComponentInChildren<ItemData>().gameObject;
				return inventoryItems[i];

			}
			


		}
		return null;

	}
	 

	
	


	

	private void AddRemainingAmountOfItem (Item itemToAdd, int remainingAmount)
	{
		for (int i = 0; i < inventoryItems.Count; i++) // Go through the items in inventory
		{



			if (inventoryItems[i].itemSlug == itemToAdd.itemSlug && itemToAdd.itemStackable && slots[i].transform.GetChild(0).GetComponent<ItemData>().amount < 20) // Add the amount of this item to an already existing stack
			{
				inventoryItems[i] = itemToAdd; // Put the item we're adding in the inventory at this ID

				slots[i].GetComponentInChildren<ItemData>().currentSlot = i; // Make sure this item's internal ID matches the slot's ID
				slots[i].GetComponentInChildren<ItemData>().amount += remainingAmount; // Add the passed amount to the slot with the same item
				slots[i].GetComponentInChildren<Text>().text = slots[i].GetComponentInChildren<ItemData>().amount.ToString(); // Write the new amount of item out

				break;

			}
			else if (inventoryItems[i].itemSlug == null) // Add this item to an empty slot if the item is not stackable or there isn't an instance of this item in the inventory
			{
				inventoryItems[i] = itemToAdd;

				GameObject invObj = Instantiate(inventoryItem); // Spawning the inventory item prefab to then fill out with the passed item's properties

				invObj.transform.SetParent(slots[i].transform);   // Parent the item prefab to the current slot
				invObj.transform.position = Vector2.zero;  // Center the prefab
				invObj.transform.localScale = new Vector3(1, 1, 1);  // Normalize its size
				invObj.GetComponent<Image>().sprite = itemToAdd.itemIcon;  // Change its image to one of the added item


				invObj.GetComponent<ItemData>().currentSlot = i;   // Set the item's slot to current slot
				invObj.GetComponent<ItemData>().amount = remainingAmount; // Set the amount to the amount we added
				invObj.GetComponent<ItemData>().item = itemToAdd; // Notify the profab that it is the added item
				invObj.GetComponentInChildren<Text>().text = invObj.GetComponent<ItemData>().amount.ToString(); // Write the amount of item in the bottom right corner

				invObj.name = itemToAdd.itemName; // Change the prefab's name to that of the item it will represent
				break;
			}


		}
	}

	public void RemoveItem(string slug, int amountToRemove)
	{
		Item itemToRemove = itemDatabase.GetItem(slug);
		for (int i = 0; i < inventoryItems.Count; i++)
		{
			
			if (inventoryItems[i].itemSlug == itemToRemove.itemSlug && itemToRemove.itemStackable)
			{
				print("Removing a stackable item");
				if (slots[i].transform.GetChild(0).GetComponent<ItemData>().amount > amountToRemove)
				{
					slots[i].transform.GetChild(0).GetComponent<ItemData>().amount -= amountToRemove;
					slots[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = slots[i].transform.GetChild(0).GetComponent<ItemData>().amount.ToString();
					return;
				}
				else if (slots[i].transform.GetChild(0).GetComponent<ItemData>().amount < amountToRemove)
				{
					print("You don't have enough of this item to complete this action!");
					return;
				}
				else if (slots[i].transform.GetChild(0).GetComponent<ItemData>().amount == amountToRemove)
				{
					inventoryItems[i] = new Item();
					Destroy(slots[i].transform.GetChild(0).gameObject);
					return;
				}
			}
			else if (inventoryItems[i].itemSlug == itemToRemove.itemSlug)
			{
				print("Removed a non-stackable item");
				inventoryItems[i] = new Item();
				Destroy(slots[i].transform.GetChild(0).gameObject);
				
				return;
			}
			
		}
	}



		public bool CheckForItemInInventory(string slug)
		{
			foreach (Item item in inventoryItems)
			{
				if (item.itemSlug == slug)
				{
					return true;
				}

			}
			return false;
		}
	}
