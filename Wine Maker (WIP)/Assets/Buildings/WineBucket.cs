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



	private float stompTimerMemory;
	private GameObject player;
	private Vector2 playerLastPos;
	private InventoryManager inventoryManager;
	private Inventory inventory;
	private SpriteRenderer mySpriteRender;
	



	// Use this for initialization
	void Start () {
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
					mySpriteRender.sprite = fullStateImage;
					inventory.RemoveItem("full_grape_basket_s", 1);
					inventory.AddItem("empty_grape_basket", 1);
					isEmpty = false;
					return;
				}
			}

			if (!isBeingUsed && !isEmpty)
			{
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
			inventory.AddItem("full_clay_jar", 1);
		

			player.transform.position = playerLastPos;
		}


	}
}
