using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeBush : BuildingActions
{

	public Transform[] grapes;

	private GameObject grapesHolder;
	private bool isBeingUsed;
	private GameObject player;
	private Inventory inventory;

	private List<int> deletedBunches = new List<int>();



	// Use this for initialization
	void Start()
	{
		inventory = FindObjectOfType<Inventory>();

		player = GameObject.FindGameObjectWithTag("Player");

		grapesHolder = transform.GetChild(1).gameObject;
		print(grapesHolder);

		grapes = grapesHolder.GetComponentsInChildren<Transform>();


	}


	public override void HandleInteractions()
	{
		if (!isBeingUsed && Vector2.Distance(player.transform.position, transform.position) <= 0.5f)
		{
			isBeingUsed = true;
			StartCoroutine(DeleteGrapesFromTree());


		}
	}

	IEnumerator DeleteGrapesFromTree()
	{
		for (int i = 0; i<grapes.Length; i++)
		{
			int randomBunchToDelete = Random.Range(1, grapes.Length);

			while (deletedBunches.Contains(randomBunchToDelete))
			{
				randomBunchToDelete = Random.Range(1, grapes.Length);
			}

			deletedBunches.Add(randomBunchToDelete);

			Destroy(grapes[randomBunchToDelete].gameObject);
			grapes[randomBunchToDelete] = null;

			

		}
		

	}

}
