using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FermentationJar : BuildingActions {

	public bool isBeingUsed = false;

	public Sprite emptyJarSprite;
	public Sprite fullJarSprite;

	private Inventory inventory;
	private SpriteRenderer mySpriteRenderer;
	private CameraUIManager cameraUIManager;
	
	private NotificationsManager notificationsManager;
	[SerializeField] int stirsBeforeFermented = 3;
	[SerializeField] int currentIncrement = 0;
	[SerializeField] float t;
	[SerializeField] bool fermentationDone;
	[SerializeField] bool fermenting;
	[SerializeField] GameObject progressBar;
	[SerializeField] bool gotPoints;

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
				inventory.AddItem("empty_clay_jar", 1);
				mySpriteRenderer.sprite = fullJarSprite;
				isBeingUsed = true;
				fermenting = true;

				StopCoroutine(FermentationTimer(Random.Range(5, 7)));
				StartCoroutine(FermentationTimer(Random.Range(5, 7)));


				Destroy(cameraUIManager.currentlyVisibleMenu);
				cameraUIManager.menuSpawned = false;
				return;

			}
			notificationsManager.StartSpawningText("Bring a jar full of wine to activate this building");
		}


		 if (fermenting && !gotPoints && isBeingUsed)
		{
			
			gotPoints = true;
			notificationsManager.StartSpawningText("Jar stirred, quality of the final product increased");
			return;
		}

		 if (fermenting && gotPoints && isBeingUsed)
			{
			notificationsManager.StartSpawningText("The jar doesn't need stirring for now");

		}



		if (fermentationDone)
		{
			if (inventory.CheckForItemInInventory("empty_wine_barrel"))
			{
				inventory.RemoveItem("empty_wine_barrel", 1);
				inventory.AddItem("full_wine_barrel", 1);
				
				isBeingUsed = false;
				fermentationDone = false;
				return;
			}

			notificationsManager.StartSpawningText("Bring an empty wine barrel to store the wine!");
		}



	}


	IEnumerator FermentationTimer(float timer)
	{

		float time = timer;
		while (timer > 0)
		{
			
			fermenting = true;
			timer -= Time.deltaTime;
			progressBar.GetComponent<Image>().fillAmount = timer / time;
			
	
			
			yield return null;
		}
		notificationsManager.StartSpawningText("Don't forget to stir the fermentation jar");
		currentIncrement++;
		

	}




	// Update is called once per frame
	void Update () {

		
	}
}
