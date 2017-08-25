using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineBucket : BuildingActions, IBottleproducer {


	[SerializeField] Transform stompArea;
	[SerializeField] ParticleSystem effector;
	[SerializeField] float stompTimer;
	[SerializeField] int wineBottlesProduced;
	[SerializeField] bool isBeingUsed = false;


	private float stompTimerMemory;
	private GameObject player;
	private Vector2 playerLastPos;
	private InventoryManager inventoryManager;
	



	// Use this for initialization
	void Start () {
		stompTimerMemory = stompTimer;
		player = GameObject.FindGameObjectWithTag("Player");
		inventoryManager = GameObject.FindObjectOfType<InventoryManager>();


	}



	public override void HandleInteractions ()
	{
		if (!isBeingUsed && Vector2.Distance(player.transform.position, transform.position) <= 0.5f)
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

			ProduceBottles(wineBottlesProduced);

			player.transform.position = playerLastPos;
		}


	}
}
