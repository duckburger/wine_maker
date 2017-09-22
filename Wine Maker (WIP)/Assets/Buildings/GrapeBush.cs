using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeBush : BuildingActions
{

	public Transform[] grapes;
	public float timeToRegenerate;
	public float myTimer;
	public GameObject grapeBunch;

	private NotificationsManager notificationsManager;
	private CameraUIManager cameraUIManager;
	private GameObject grapesHolder;
	[SerializeField] bool isBeingUsed;
	private PlayerMovement player;
	private Inventory inventory;
	[SerializeField] bool isPickedClean;
	private List<Vector2> bunchPositions = new List<Vector2>();



	// Use this for initialization
	void Start()
	{
		notificationsManager = FindObjectOfType<NotificationsManager>();

		cameraUIManager = FindObjectOfType<CameraUIManager>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

		grapesHolder = transform.GetChild(1).gameObject;
		

		grapes = grapesHolder.GetComponentsInChildren<Transform>();
		print(grapes.Length);


		foreach (Transform grapeBunchTransform in grapes)
		{
			bunchPositions.Add(grapeBunchTransform.position);
		}
		inventory = FindObjectOfType<Inventory>();

	}

	public override void HandleInteractions()
	{
		if (!isBeingUsed && Vector2.Distance(player.transform.position, transform.position) <= 0.5f && !isPickedClean)
		{

			if (inventory.CheckForItemInInventory("empty_grape_basket"))
			{
				player.isUsingSomething = true;

				isBeingUsed = true;
				StartCoroutine(DeleteGrapesFromTree());
				Destroy(cameraUIManager.currentlyVisibleMenu);
				cameraUIManager.menuSpawned = false;
				return;
			}
			notificationsManager.StartSpawningText("You need an empty bucket to pick grapes");

		}


	}

	private void Update()
	{
		 if (isPickedClean)
		{
			myTimer -= Time.deltaTime;

			if (myTimer <= 0)
			{
				myTimer = timeToRegenerate;
				StartCoroutine(Regrow());
				
			}
		}
	}



	IEnumerator Regrow()
	{
		if (grapes[4] == null)
		{
			foreach (Vector2 placeToSpawn in bunchPositions)
			{
				Instantiate(grapeBunch, placeToSpawn, Quaternion.identity, grapesHolder.transform);
				grapes = grapesHolder.GetComponentsInChildren<Transform>();

				yield return new WaitForSeconds(1.5f);
			}
			
			isPickedClean = false;
			yield break;
		}
		Debug.Log("The grapes haven't been picked");
		
	}

	

	IEnumerator DeleteGrapesFromTree()
	{
		for (int i = 1; i<grapes.Length; i++)
		{
			Destroy(grapes[i].gameObject);
			yield return new WaitForSeconds(1);

			

		}

		SpawnGrapeBasket();
	}

	private void SpawnGrapeBasket()
	{

		inventory.RemoveItem("empty_grape_basket", 1);
		inventory.AddItem("full_grape_basket_u", 1);


		isPickedClean = true;
		myTimer = timeToRegenerate;
		isBeingUsed = false;
		player.isUsingSomething = false;
	}
}
