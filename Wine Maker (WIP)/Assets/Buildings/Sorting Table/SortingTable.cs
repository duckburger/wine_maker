using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingTable : BuildingActions {

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


	public override void HandleInteractions()
	{
		if (!isBeingUsed && inventory.CheckForItemInInventory("full_grape_basket_u") == true)
		{
			
			sortingMiniGameController.StartTheSortingMiniGame();
			Destroy(cameraUIManager.currentlyVisibleMenu);
			cameraUIManager.menuSpawned = false;
			return;
		}
		notificationsManager.StartSpawningText("You need to bring unsorted grapes to use this table");
		



	}
}
