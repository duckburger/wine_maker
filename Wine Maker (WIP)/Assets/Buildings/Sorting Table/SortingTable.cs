using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingTable : BuildingActions {

	[SerializeField] bool isBeingUsed;


	private PlayerMovement player;
	private SortingMinigame sortingMiniGameController;

	private void Start()
	{
		player = FindObjectOfType<PlayerMovement>();
		sortingMiniGameController = FindObjectOfType<SortingMinigame>();
	}


	public override void HandleInteractions()
	{
		if (!isBeingUsed)
		{
			isBeingUsed = !isBeingUsed;
			sortingMiniGameController.StartTheSortingMiniGame();
		}
	}
}
