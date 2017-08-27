using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeBush : BuildingActions
{

	public Transform[] grapes;

	private GameObject grapesHolder;
	[SerializeField] bool isBeingUsed;
	private GameObject player;
	private Inventory inventory;

	private List<int> deletedBunches = new List<int>();



	// Use this for initialization
	void Start()
	{
		

		player = GameObject.FindGameObjectWithTag("Player");

		grapesHolder = transform.GetChild(1).gameObject;
		print(grapesHolder);

		grapes = grapesHolder.GetComponentsInChildren<Transform>();

		inventory = FindObjectOfType<Inventory>();

	}


	public override void HandleInteractions()
	{
		if (!isBeingUsed && Vector2.Distance(player.transform.position, transform.position) <= 0.5f && inventory.CheckForItemInInventory("empty_grape_basket"))
		{
			isBeingUsed = true;
			StartCoroutine(DeleteGrapesFromTree());

			

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

	}
}
