using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class FermentationJar : BuildingActions, IDropHandler {

	public bool isBeingUsed = false;

	public Sprite emptyJarSprite;
	public Sprite fullJarSprite;

	private Inventory inventory;
	private SpriteRenderer mySpriteRenderer;
	private CameraUIManager cameraUIManager;
	
	private NotificationsManager notificationsManager;
	[SerializeField] int currentIncrement = 0;
	[SerializeField] float t;
	[SerializeField] bool fermentationDone;
	[SerializeField] bool fermenting;
	[SerializeField] GameObject progressBar;
	[SerializeField] bool gotPoints;
	private PlayerMovement player;

	// Use this for initialization
	void Start () {

		cameraUIManager = FindObjectOfType<CameraUIManager>();
		inventory = FindObjectOfType<Inventory>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		notificationsManager = FindObjectOfType<NotificationsManager>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		
	}

	private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(1))
		{

		}
	}

	public override void HandleInteractions()
	{
	
		if (Vector3.Distance(player.transform.position, this.transform.position) < 0.4f)
		{
			if (fermentationDone && inventory.CheckForItemInInventory("empty_wine_barrel"))
			{
				mySpriteRenderer.sprite = emptyJarSprite;
				isBeingUsed = false;
				inventory.RemoveItem("empty_wine_barrel", 1);
				inventory.AddItem("full_wine_barrel", 1);
				fermentationDone = false;
				return;
			}


			if (!fermentationDone && fermenting)
			{
				notificationsManager.StartSpawningText("The wine is still fermenting!");
				return;
			}

			if (fermentationDone && !inventory.CheckForItemInInventory("empty_wine_barrel"))
			{
				notificationsManager.StartSpawningText("You need an empty wine barrel to store the fermented wine");
				return;
			}
			return;

		}
		notificationsManager.StartSpawningText("Too far away!");




	}

	IEnumerator Ferment(float timer)
	{
		var timerMem = timer;


		while (timer > 0.0001f)
		{
			timer -= Time.deltaTime;
			progressBar.GetComponentInChildren<Image>().fillAmount = timer /  timerMem;
			yield return null;
		}
		fermentationDone = true;
		fermenting = false;
		

	}



	// Update is called once per frame
	void Update () {

		
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (Vector3.Distance(player.transform.position, this.transform.position) < 0.4f)
		{
			if (eventData.pointerDrag.GetComponent<ItemData>().item.itemSlug == "full_clay_jar")
			{

				if (!isBeingUsed)
				{
					if (inventory.CheckForItemInInventory("full_clay_jar"))
					{
						inventory.RemoveItem("full_clay_jar", 1);
						inventory.AddItem("empty_clay_jar", 1);
						mySpriteRenderer.sprite = fullJarSprite;
						isBeingUsed = true;
						fermenting = true;

						StartCoroutine(Ferment(UnityEngine.Random.Range(6, 12)));

						Destroy(cameraUIManager.currentlyVisibleMenu);
						cameraUIManager.menuSpawned = false;
						return;

					}
					notificationsManager.StartSpawningText("Bring a jar full of wine to activate this building");
					return;
				}



			}
		}
		notificationsManager.StartSpawningText("Too far away!");


	}
}
