using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BarrelRack : BuildingActions, IDropHandler, IPointerEnterHandler, IPointerExitHandler {


	private Inventory inventory;

	public Transform barrelSlots;
	public Sprite emptyBarrelImage;
	public List<GameObject> listOfBarrelSlots = new List<GameObject>();

	private NotificationsManager notificationsManager;


	// Use this for initialization
	void Start () {
		inventory = FindObjectOfType<Inventory>();
		notificationsManager = FindObjectOfType<NotificationsManager>();

		for (int i = 0; i < 7; i++)
		{
			listOfBarrelSlots.Add(barrelSlots.GetChild(i).gameObject);
		}
	}


	public override void HandleInteractions()
	{
		Debug.Log("Used the barrel rack");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag.GetComponent<ItemData>().item.itemSlug != null)
		{
			if (eventData.pointerDrag.GetComponent<ItemData>().item.itemSlug == "empty_wine_barrel")
			{


				inventory.RemoveItem("empty_wine_barrel", 1);
				foreach (GameObject slot in listOfBarrelSlots)
				{
					if (!slot.transform.GetChild(0).gameObject.activeSelf)
					{
						slot.transform.GetChild(0).gameObject.SetActive(true);

						slot.transform.GetChild(0).GetComponent<Barrel>().ShowEmptyBarrel();

						break;
					}
					notificationsManager.StartSpawningText("This barrel rack is full");
				}
			}

			if (eventData.pointerDrag.GetComponent<ItemData>().item.itemSlug == "full_wine_barrel")
			{
				inventory.RemoveItem("full_wine_barrel", 1);

				foreach (GameObject slot in listOfBarrelSlots)
				{
					if (!slot.transform.GetChild(0).gameObject.activeSelf)
					{
						slot.transform.GetChild(0).gameObject.SetActive(true);

						slot.transform.GetChild(0).GetComponent<Barrel>().ShowFullBarrel();

						break;
					}
					notificationsManager.StartSpawningText("This barrel rack is full");

				}
			}
		}
		

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Mouse entered");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Mouse exited");
	}
}
