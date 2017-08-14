using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public int slotsX;
	public int slotsY;

	public List<Item> slots = new List<Item>();
	public List<Item> itemsInInvetory = new List<Item>();


	public bool openInventory;
	public GUISkin invSkin;

	private ItemDatabase itemDatabase;
	private bool showToolTip;
	private string toolTip;

	private bool draggingItem;
	private Item draggedItem;
	private int draggedIndex;

	// Use this for initialization
	void Start () {

		itemDatabase = FindObjectOfType<ItemDatabase>();
		
		for (int i = 0; i < (slotsX * slotsY); i++)
		{
			slots.Add(new Item());
			itemsInInvetory.Add(new Item());
		}


	}

	

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.I)){

			openInventory = !openInventory;
		}
		
	}

	private void OnGUI()
	{

		toolTip = ""; // Clears the tooltip text, whjich later affects whether the tooltip window is showing

		GUI.skin = invSkin;
		if (openInventory)
		{
			DrawInventory();

			if (showToolTip)
			{

				GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y - 200, 200, 200), toolTip, invSkin.GetStyle("Tooltips"));
			}
		}

		if (draggingItem)
		{
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
		}



	}

	void DrawInventory()
	{
		Event e = Event.current; //Remember the currenty event for ease of use later

		int i = 0; //Remembering the current slot
		for (int y = 0; y < slotsY; y++) //Loop throught each row
		{
			for (int x = 0; x < slotsX; x++) //Loop throught each column
			{
				Rect currentSlot = new Rect((x * 90) + Screen.width / 3, (y * 90) + Screen.height - 160, 80, 80); //Remembering the current slot we're drawing for use below
				GUI.Box(currentSlot, "", invSkin.GetStyle("Slots")); // Draw a box with coordinates of the current slot, and with a skin that we pre-make in the inspector by creating a "Skin" object

				slots[i] = itemsInInvetory[i]; //Set slots equal to inventory so we can see the items in the inventory displayed.

				if (slots[i].itemName != null) //If there is something in the current slot....
				{
					GUI.DrawTexture(currentSlot, slots[i].itemIcon); // ....draw the icon of whatever it is in the center
					if (currentSlot.Contains(Event.current.mousePosition)) //If we're mousing over this slot - draw a tooltip with the data from the item in this slot (pulled from "i")
					{
						toolTip = CreateToolTip(slots[i]); // Method listed later
						showToolTip = true;  //Activates the tooltip (see OnGUI)

						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem) //If we're pressing the left mouse button and dragging the mouse, and we're not dragging something else at the time....
						{
							draggingItem = true; 
							draggedIndex = i; 
							draggedItem = slots[i]; // Remember what item we're dragging (see condition below for "dropping)
							itemsInInvetory[i] = new Item(); // Make the item's slot empty

						} 
						
						if (e.type == EventType.mouseUp && draggingItem) //If we drop the item we were dragging...
						{
							itemsInInvetory[draggedIndex] = itemsInInvetory[i]; //
							itemsInInvetory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				} else  //If there is nothing in the slot...
				{
					if (currentSlot.Contains(Event.current.mousePosition))
					{
						if (e.type == EventType.mouseUp && draggingItem) //If we drop the item we were dragging...
						{
							itemsInInvetory[i] = draggedItem; // Set the inventory slot we're mousing over to the item we're dragging
							draggingItem = false; // Stop dragging the item
							draggedItem = null; // Null our the dragged item
						}
						
					}
					
				}

				if (toolTip == "")
				{
					showToolTip = false;
				}

				i++;
			}
		}
	}

	 string CreateToolTip (Item item) // Take in an item and write in the tooltip (with color tags)
	{
		
		toolTip = "<color=#2de263>" + item.itemName + "</color>\n\n" + item.itemDescription;
		return toolTip;
	}


	public bool AddItemToInventory (int ID) // Add item to inventory by ID
	{
		for (int i = 0; i < itemsInInvetory.Count; i++)
		{
			if (itemsInInvetory[i].itemName == null)
			{
				for (int j = 0; j < itemDatabase.items.Count; j++)
				{
					if (itemDatabase.items[j].itemID == ID)
					{
						itemsInInvetory[i] = itemDatabase.items[j];
						return true;
					}
				}
				
			}
		}

		Debug.Log("Inventory full!");
		return false;
	}

	void RemoveItemFromInventory (int ID) // Remove item to inventory by ID
	{
		for (int i = 0; i < itemsInInvetory.Count; i++)
		{
			if (itemsInInvetory[i].itemID == ID)
			{
				print("Removed " + itemsInInvetory[i].itemName);
				itemsInInvetory[i] = new Item();
				break;
			}
		
		}
	}

	bool CheckInventoryForItem (int ID)
	{
		for (int i = 0; i < itemsInInvetory.Count; i++)
		{
			if (itemsInInvetory[i].itemID == ID)
			{
				
				return true;
			}
			
		}
		return false;
	}
}
