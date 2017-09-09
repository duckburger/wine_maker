using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermentationJar : BuildingActions {

	public bool isBeingUsed = false;
	public float qSToRemember;
	public float qSForThisStage;
	public Sprite emptyJarSprite;
	public Sprite fullJarSprite;

	private Inventory inventory;
	private SpriteRenderer mySpriteRenderer;
	private CameraUIManager cameraUIManager;
	
	private NotificationsManager notificationsManager;
	[SerializeField] int timesBeforeReady = 3;
	[SerializeField] int currentIncrement = 0;
	[SerializeField] float t;
	[SerializeField] bool fermentationDone;
	[SerializeField] float fermentatationAlertInterval;
	[SerializeField] bool fermenting;

	// Use this for initialization
	void Start () {

		cameraUIManager = FindObjectOfType<CameraUIManager>();
		inventory = FindObjectOfType<Inventory>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		notificationsManager = FindObjectOfType<NotificationsManager>();
		
	}

	public override void HandleInteractions()
	{
		 if (!isBeingUsed)
		{
			if (inventory.CheckForItemInInventory("full_clay_jar"))
			{
				inventory.RemoveItem("full_clay_jar", 1);
				qSToRemember =  inventory.lastRemovedItem.GetComponent<ItemData>().myBottleInProgress.qualityScore;
				inventory.AddItem("empty_clay_jar", 1);
				mySpriteRenderer.sprite = fullJarSprite;
				isBeingUsed = true;
				
				Destroy(cameraUIManager.currentlyVisibleMenu);
				cameraUIManager.menuSpawned = false;


			}
			notificationsManager.StartSpawningText("Bring a jar full of wine to activate this building");
		}

	}


	


	// Update is called once per frame
	void Update () {

		
	}
}
