using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Barrel : MonoBehaviour
{

	public GameObject bg;
	public GameObject fg;

	public Sprite emptyBarrelSprite;
	public Sprite fullBarrelSprite;
	public Sprite bgOfBottle;
	public Sprite agedSprite;

	private PlayerMovement player;
	private NotificationsManager notificationsManager;
	private Inventory inventory;
	[SerializeField] float agingTimer;
	[SerializeField] bool doneAging;
	[SerializeField] bool empty;

	// Use this for initialization
	void Start()
	{
		agingTimer = 25;
		inventory = FindObjectOfType<Inventory>();
		notificationsManager = FindObjectOfType<NotificationsManager>();
		player = FindObjectOfType<PlayerMovement>();
	}


	public void ShowEmptyBarrel()
	{
		doneAging = false;
		fg.GetComponent<Image>().enabled = false;
		bg.GetComponent<Image>().sprite = emptyBarrelSprite;
		empty = true;
		return;
	}


	public void ShowFullBarrel()
	{
		empty = false;
		Image bgImage = bg.GetComponent<Image>();
		Image fgImage = fg.GetComponent<Image>();
		bgImage.sprite = bgOfBottle;
		fgImage.enabled = true;

		float t = 0;

		StartCoroutine(TimerForFermentation(t, fgImage));

		return;



	}

	IEnumerator TimerForFermentation(float timeToIncrement, Image bottleIndicator)
	{
		while (timeToIncrement < 25)
		{

			bottleIndicator.sprite = fullBarrelSprite;
			timeToIncrement += Time.deltaTime;
			bottleIndicator.fillAmount = timeToIncrement / agingTimer;
			yield return null;
		}

		timeToIncrement = 0;
		bottleIndicator.sprite = agedSprite;
		doneAging = true;


	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(1))
		{

			if (Vector2.Distance(player.transform.position, transform.position) < 0.4f)
			{
				if (empty)
				{
					inventory.AddItem("empty_wine_barrel", 1);
					this.gameObject.SetActive(false);
					return;
				}

				if (doneAging)
				{
					inventory.AddItem("aged_wine_barrel", 1);
					this.gameObject.SetActive(false);
					return;
				}
				notificationsManager.StartSpawningText("The wine in this barrel is still fermenting");
				return;
			}
			notificationsManager.StartSpawningText("Too far away");
			return;

		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
	
