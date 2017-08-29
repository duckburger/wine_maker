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


	private CameraUIManager cameraUIManager;
	private GameObject grapesHolder;
	[SerializeField] bool isBeingUsed;
	private PlayerMovement player;
	private Inventory inventory;
	[SerializeField] bool isPickedClean;
	private List<int> deletedBunches = new List<int>();
	private List<Vector2> bunchPositions = new List<Vector2>();



	// Use this for initialization
	void Start()
	{

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
		if (grapes[5] == null)
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

	public override void HandleInteractions()
	{
		if (!isBeingUsed && Vector2.Distance(player.transform.position, transform.position) <= 0.5f && inventory.CheckForItemInInventory("empty_grape_basket") && !isPickedClean)
		{
			player.isUsingSomething = true;

			isBeingUsed = true;
			StartCoroutine(DeleteGrapesFromTree());
			Destroy(cameraUIManager.currentlyVisibleMenu);
			cameraUIManager.menuSpawned = false;



		}

		
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
