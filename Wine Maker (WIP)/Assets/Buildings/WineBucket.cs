using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineBucket : BuildingActions, IBottleproducer {


	public bool isEmpty;

	[SerializeField] Transform stompArea;
	[SerializeField] ParticleSystem effector;
	[SerializeField] float stompTimer;
	[SerializeField] bool isBeingUsed = false;
	[SerializeField] Sprite emptyStateImage;
	[SerializeField] Sprite fullStateImage;
	[SerializeField] float qSToRemember;


	private float stompTimerMemory;
	private GameObject player;
	private Vector2 playerLastPos;
	private InventoryManager inventoryManager;
	private Inventory inventory;
	private SpriteRenderer mySpriteRender;
	private StompingMinigame stompingMinigameController;
	



	// Use this for initialization
	void Start () {
		stompingMinigameController = FindObjectOfType<StompingMinigame>();
		inventory = FindObjectOfType<Inventory>();
		stompTimerMemory = stompTimer;
		player = GameObject.FindGameObjectWithTag("Player");
		inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
		isEmpty = true;
		mySpriteRender = GetComponent<SpriteRenderer>();

	}



	public override void HandleInteractions ()
	{
		if (Vector2.Distance(player.transform.position, transform.position) <= 0.5f)
		{

			if (!isBeingUsed && isEmpty)
			{
				if (inventory.CheckForItemInInventory("full_grape_basket_s"))
				{
					/*for (int j = 0; j < inventory.inventoryItems.Count; j++) {
						if (inventory.inventoryItems[j].itemSlug == "full_grape_basket_s")
						{
							stompingMinigameController.qsForThisStage = inventory.slots[j].GetComponentInChildren<ItemData>().myBottleInProgress.qualityScore;
							break;
						}
					}*/
					mySpriteRender.sprite = fullStateImage;
					inventory.RemoveItem("full_grape_basket_s", 1);

					qSToRemember = inventory.lastAddedItem.GetComponent<ItemData>().myBottleInProgress.qualityScore;

					inventory.AddItem("empty_grape_basket", 1);

					inventory.lastAddedItem.GetComponent<ItemData>().myBottleInProgress.qualityScore = qSToRemember;

					isEmpty = false;
					return;
				}
			}

			if (!isBeingUsed && !isEmpty)
			{
				stompingMinigameController.StartTheStompingMiniGame();
				stompTimer = stompTimerMemory;
				isBeingUsed = true;
				playerLastPos = player.transform.position;
				player.transform.position = stompArea.transform.position;
				print("Moved the player to " + stompArea);
				player.GetComponent<PlayerMovement>().isUsingSomething = true;
				player.GetComponent<PlayerMovement>().myAnimator.SetTrigger("stompingTrigger");
				effector.Play();
				

			}
		}

		Debug.Log("Not close enough to interact with this building!");
		
	}

	

		
		
	

	public void ProduceBottles(int producedBottles)
	{
		inventoryManager.CurrentBottles += producedBottles;

	}

	// Update is called once per frame
	void Update () {

		stompTimer -= Time.deltaTime;

		if (stompTimer <= 0 && isBeingUsed)
		{
			isBeingUsed = false;
			player.GetComponent<PlayerMovement>().myAnimator.SetTrigger("playerIdle");
			player.GetComponent<PlayerMovement>().isUsingSomething = false;

			effector.Stop();
			stompTimer = stompTimerMemory;
			mySpriteRender.sprite = emptyStateImage;
			isEmpty = true;
			Item itemToAdd = inventory.AddItem("full_clay_jar", 1);

			inventory.lastAddedItem.GetComponent<ItemData>().myBottleInProgress.qualityScore = qSToRemember + stompingMinigameController.qsForThisStage;

			stompingMinigameController.StopTheStompingMiniGame();
			player.transform.position = playerLastPos;

			
		}


	}
}
