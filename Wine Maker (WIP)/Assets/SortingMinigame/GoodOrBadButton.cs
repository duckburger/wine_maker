using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoodOrBadButton : MonoBehaviour, IPointerDownHandler {

	public GrapeButton.ButtonValue valueOfThis;
	public int buttonID;
	public int changeOfQSValue;

	private SortingMinigame sortingMiniGameController;
	




	// Use this for initialization
	void Start()
	{
		sortingMiniGameController = FindObjectOfType<SortingMinigame>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (valueOfThis == GrapeButton.ButtonValue.Good)
		{
			
			Destroy(gameObject);
		} else if (valueOfThis == GrapeButton.ButtonValue.Bad) {

			Destroy(gameObject);
		}
	}

	
	
	// Update is called once per frame
	void Update () {
		
	}
}
