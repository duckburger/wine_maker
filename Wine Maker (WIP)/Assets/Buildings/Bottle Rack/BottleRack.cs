using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BottleRack : BuildingActions, IDropHandler
{


	private Inventory inventory;

	public Transform bottleSlots;
	public List<GameObject> listOfBottleSlots = new List<GameObject>();

	private PlayerMovement player;
	private NotificationsManager notificationsManager;


	// Use this for initialization
	void Start()
	{
		inventory = FindObjectOfType<Inventory>();
		notificationsManager = FindObjectOfType<NotificationsManager>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

		for (int i = 0; i < 21; i++)
		{
			listOfBottleSlots.Add(bottleSlots.GetChild(i).gameObject);
		}
	}


	public override void HandleInteractions()
	{
		Debug.Log("Used the barrel rack");
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnDrop(PointerEventData eventData)
	{

		if (Vector3.Distance(player.transform.position, this.transform.position) < 0.4f)
		{
			if (eventData.pointerDrag.GetComponent<ItemData>().item.itemSlug != null)
			{
				if (eventData.pointerDrag.GetComponent<ItemData>().item.itemSlug == "empty_wine_bottle")
				{


					inventory.RemoveItem("empty_wine_bottle", 1);
					foreach (GameObject slot in listOfBottleSlots)
					{
						if (!slot.transform.GetChild(0).gameObject.activeSelf)
						{
							slot.transform.GetChild(0).gameObject.SetActive(true);
							slot.transform.GetChild(0).GetComponent<Bottle>().myType = Bottle.BottleType.empty;
							slot.transform.GetChild(0).GetComponent<Bottle>().ShowBottle();

							return;
						}


					}
					notificationsManager.StartSpawningText("This bottle rack is full");
					return;
				}

				if (eventData.pointerDrag.GetComponent<ItemData>().item.itemSlug == "full_wine_bottle")
				{
					inventory.RemoveItem("full_wine_bottle", 1);

					foreach (GameObject slot in listOfBottleSlots)
					{
						if (!slot.transform.GetChild(0).gameObject.activeSelf)
						{
							slot.transform.GetChild(0).gameObject.SetActive(true);
							slot.transform.GetChild(0).GetComponent<Bottle>().myType = Bottle.BottleType.full;
							slot.transform.GetChild(0).GetComponent<Bottle>().ShowBottle();

							return;

						}


					}
					notificationsManager.StartSpawningText("This bottle rack is full");
					return;
				}
			}
		}
		notificationsManager.StartSpawningText("Too far away!");




	}


}
