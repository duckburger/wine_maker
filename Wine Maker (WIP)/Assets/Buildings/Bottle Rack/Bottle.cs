using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bottle : MonoBehaviour
{


	public Sprite lightEmpty;
	public Sprite lightFull;
	public Sprite lightLabeled;

	public Sprite regularEmpty;
	public Sprite regularFull;
	public Sprite regularLabeled;
	public BottleType myType;



	private PlayerMovement player;
	private SpriteRenderer mySprite;
	private Inventory inventory;

	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		inventory = FindObjectOfType<Inventory>();
		
	}

	private void OnEnable()
	{
		mySprite = GetComponent<SpriteRenderer>();
	}

	public enum BottleType
	{
		empty,
		full,
		labeled
	}

	void OnMouseOver()
	{


		switch (myType)
		{
			case BottleType.empty:
				mySprite.sprite = lightEmpty;
				break;
			case BottleType.full:
				mySprite.sprite = lightFull;
				break;
			case BottleType.labeled:
				mySprite.sprite = lightLabeled;
				break;

		}

		if (Input.GetMouseButtonDown(1))
		{

			if (Vector2.Distance(player.transform.position, transform.position) < 0.8f)
			{


				switch (myType)
				{
					case BottleType.empty:
						inventory.AddItem("empty_wine_bottle", 1);
						this.gameObject.SetActive(false);
						break;
					case BottleType.full:
						inventory.AddItem("full_wine_bottle", 1);
						this.gameObject.SetActive(false);
						break;
					case BottleType.labeled:
						inventory.AddItem("full_wine_bottle", 1);
						this.gameObject.SetActive(false);
						break;

				}


			}
		}

	}


	private void OnMouseExit()
	{
		switch (myType)
		{
			case BottleType.empty:
				mySprite.sprite = regularEmpty;
				break;
			case BottleType.full:
				mySprite.sprite = regularFull;
				break;
			case BottleType.labeled:
				mySprite.sprite = regularLabeled;
				break;

		}
	}

	public void ShowBottle()
	{
		switch (myType)
		{
			case BottleType.empty:
				mySprite.sprite = regularEmpty;
				break;
			case BottleType.full:
				mySprite.sprite = regularFull;
				break;
			case BottleType.labeled:
				mySprite.sprite = regularLabeled;
				break;

		}
	}


}

