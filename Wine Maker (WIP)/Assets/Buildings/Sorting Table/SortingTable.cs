using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SortingTable : BuildingActions, IDropHandler {

	public bool isBeingUsed;


	private PlayerMovement player;
	private SortingMinigame sortingMiniGameController;
	private CameraUIManager cameraUIManager;
	private Inventory inventory;
	private NotificationsManager notificationsManager;

	private void Start()
	{
		cameraUIManager = FindObjectOfType<CameraUIManager>();
		player = FindObjectOfType<PlayerMovement>();
		sortingMiniGameController = FindObjectOfType<SortingMinigame>();
		inventory = FindObjectOfType<Inventory>();
		notificationsManager = FindObjectOfType<NotificationsManager>(); 
	}



	
	public void OnDrop(PointerEventData eventData)
	{


			if (Vector3.Distance(player.transform.position, this.transform.position) < 0.4f)
			{

			if (eventData.pointerDrag.GetComponent<ItemData>().item.itemSlug == "empty_grape_basket_u")		{

				if (!isBeingUsed && inventory.CheckForItemInInventory("full_grape_basket_u") == true)
				{

					sortingMiniGameController.StartTheSortingMiniGame();
					Destroy(cameraUIManager.currentlyVisibleMenu);
					cameraUIManager.menuSpawned = false;
					return;
				}
			}
			notificationsManager.StartSpawningText("You need to bring unsorted grapes to use this table");
			return;


			}
		notificationsManager.StartSpawningText("Too far away!");
		return;
	}
	 

}
